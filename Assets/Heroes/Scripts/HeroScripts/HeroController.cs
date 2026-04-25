using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class HeroController : MonoBehaviour
{
    public static List<HeroController> AllHeroes = new List<HeroController>();

    [SerializeField] private Camera MainCamera;
    [SerializeField] private HeroConfig Hero_Config;
    [SerializeField] private GameObject Marker;

    private Ray Ray;
    private RaycastHit _hit;

    private NavMeshAgent _agent;

    public HeroAttributes Hero_Attributes;

    private HeroMovement _heroMovement;
    private HeroAttack _heroAttack;
    private HeroSkills _heroSkills;
    private AnimationController _animationController;
    
    private HeroInventory _heroInventory;
    public HeroInventory HeroInventory => _heroInventory;

    public GameObject CurrentTarget;

    private Coroutine HealthAndManaGain;

    private Coroutine Respawn;

    public bool isDead = false;

    private void Awake()
    {
        Hero_Attributes = new HeroAttributes(Hero_Config);

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.speed = Hero_Attributes.CurrentMoveSpeed;

        _heroMovement = GetComponent<HeroMovement>();
        _heroAttack = GetComponent<HeroAttack>();
        _heroSkills = GetComponent<HeroSkills>();
        _heroInventory = GetComponent<HeroInventory>();
        _animationController = GetComponent<AnimationController>();

        _heroMovement.Init(this);
        _heroAttack.Init(this);
        _heroSkills.Init(this);
        _heroInventory.Init(this);
        _animationController.Init(this);
    }

    private void Start()
    {
        //Update UI
        HeroEventsArgs HeroArgs = new HeroEventsArgs(_heroSkills.GetAllSkills(), Hero_Attributes, _heroInventory,
            _heroSkills.GetAllSkillsLevels());
        HeroEvents.OnHeroSelectHandler?.Invoke(HeroArgs);

        HealthAndManaGain = StartCoroutine(GainAttributesLoop());
    }

    private void Update()
    {
        Ray = MainCamera.ScreenPointToRay(InputReader.Instance.MousePosition);

        if (!isDead)
        {
            HandleMovementAndTargeting();

            HandleActiveSkills();
            HandlePassiveSkills();
        }

        if (Hero_Attributes.CurrentHealth <= 0 && !isDead)
        {
            Respawn = StartCoroutine(RespawnHero());
        }

        //Test delete later :
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            ChangeHealth(true, 20);
        }
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            ChangeHealth(false, 20);
        }

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            ChagneMana(false, 20);
        }
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            ChagneMana(true, 20);
        }

        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            GainXp(50f);
            Debug.Log($"{_heroSkills.GetSkillLevel(0)},{_heroSkills.GetSkillLevel(1)},{_heroSkills.GetSkillLevel(2)},{_heroSkills.GetSkillLevel(3)}");
        }

        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            _heroInventory.SetGold(true, 100);
        }
    }

    private void HandleMovementAndTargeting()
    {
        if (InputReader.Instance.MouseRightClick)
        {
            if (Physics.Raycast(Ray, out _hit, 100f))
            {
                if (_hit.collider.gameObject.CompareTag("Environment"))
                {
                    Debug.Log("Hit Environment");
                }
                else if (_hit.collider.gameObject.CompareTag("Floor"))
                {
                    CurrentTarget = null;

                    _agent.speed = Hero_Attributes.CurrentMoveSpeed;

                    _heroMovement.Move(_hit.point);

                    Instantiate(Marker, _hit.point, Marker.transform.rotation);
                }
                else if (_hit.collider.gameObject.CompareTag("Enemy"))
                {
                    _agent.speed = Hero_Attributes.CurrentMoveSpeed;

                    CurrentTarget = _hit.collider.gameObject;
                }
                else if (_hit.collider.gameObject.CompareTag("Item"))
                {
                    CurrentTarget = null;

                    _agent.speed = Hero_Attributes.CurrentMoveSpeed;

                    _heroMovement.Move(_hit.point);
                }
            }
        }

        if (CurrentTarget != null)
        {
            bool isInRange = _heroMovement.IsInRange(CurrentTarget.transform.position, Hero_Attributes.CurrentAttackRange);

            _heroAttack.GetRangeToAttack(isInRange, CurrentTarget);
        }
    }
   
    private void HandleActiveSkills()
    {
        if (InputReader.Instance.QButton)
        {
            if (_heroSkills.GetSkillLevel(0) == 0)
            {
                Debug.Log("Skill not studied");
                return;
            }
            if (_heroSkills.GetSkillTypeById(0) == SkillType.Active)
            {
                _heroSkills.ExecuteSkillById(0, this);
            }
        }
        if (InputReader.Instance.WButton)
        {
            if (_heroSkills.GetSkillLevel(1) == 0)
            {
                Debug.Log("Skill not studied");
                return;
            }
            if (_heroSkills.GetSkillTypeById(1) == SkillType.Active)
            {
                _heroSkills.ExecuteSkillById(1, this);
            }
        }
        if (InputReader.Instance.EButton)
        {
            if (_heroSkills.GetSkillLevel(2) == 0)
            {
                Debug.Log("Skill not studied");
                return;
            }
            if (_heroSkills.GetSkillTypeById(2) == SkillType.Active)
            {
                _heroSkills.ExecuteSkillById(2, this);
            }
        }
        if (InputReader.Instance.RButton)
        {
            if (_heroSkills.GetSkillLevel(3) == 0)
            {
                Debug.Log("Skill not studied");
                return;
            }
            if (_heroSkills.GetSkillTypeById(3) == SkillType.Active)
            {
                _heroSkills.ExecuteSkillById(3, this);
            }
        }
    }

    private void HandlePassiveSkills()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_heroSkills.GetSkillTypeById(i) == SkillType.Passive)
            {
                if (_heroSkills.GetSkillLevel(i) == 0)
                {
                    Debug.Log("Skill not studied");
                    continue;
                }
                _heroSkills.ExecuteSkillById(i, this);
            }
        }
    }

    private void HandleOnAttackPassiveSkills(HeroEventsArgs HeroArgs)
    {
        for (int i = 0; i < 4; i++)
        {
            if (_heroSkills.GetSkillTypeById(i) == SkillType.OnAttackPassive)
            {
                if (_heroSkills.GetSkillLevel(i) == 0)
                {
                    Debug.Log("Skill not studied");
                    continue;
                }
                _heroSkills.ExecuteSkillById(i, this);
            }
        }
    }

    private IEnumerator GainAttributesLoop()
    {
        while (true)
        {
            ChangeHealth(false, Hero_Attributes.HealthGain);
            ChagneMana(true, Hero_Attributes.ManaGain);

            yield return new WaitForSeconds(1f);
        }
    }

    public void ChangeHealth(bool isGettingDamage, int damage)
    {
        if (isGettingDamage)
        {
            Hero_Attributes.CurrentHealth -= damage;
        }
        else
        {
            Hero_Attributes.CurrentHealth += damage;
        }

        if (Hero_Attributes.CurrentHealth < 0)
        {
            Hero_Attributes.CurrentHealth = 0;
        }
        else if (Hero_Attributes.CurrentHealth > Hero_Attributes.MaxHealth)
        {
            Hero_Attributes.CurrentHealth = Hero_Attributes.MaxHealth;
        }

        HeroEventsArgs HeroArgs = new HeroEventsArgs(Hero_Attributes);
        HeroEvents.OnHealthChangeHandler?.Invoke(HeroArgs);
    }

    public void ChagneMana(bool isGettingMana, int magaAmount)
    {
        if (isGettingMana)
        {
            Hero_Attributes.CurrentMana += magaAmount;
        }
        else
        {
            Hero_Attributes.CurrentMana -= magaAmount;
        }

        if (Hero_Attributes.CurrentMana < 0)
        {
            Hero_Attributes.CurrentMana = 0;
        }
        else if (Hero_Attributes.CurrentMana > Hero_Attributes.MaxMana)
        {
            Hero_Attributes.CurrentMana = Hero_Attributes.MaxMana;
        }

        HeroEventsArgs HeroArgs = new HeroEventsArgs(Hero_Attributes);
        HeroEvents.OnManaChangeHandler?.Invoke(HeroArgs);
    }

    public void GainXp(float XpAmount)
    {
        Hero_Attributes.CurrentXP += XpAmount;

        if (Hero_Attributes.CurrentXP >= Hero_Attributes.XPForLevelUP)
        {
            Hero_Attributes.XPForLevelUP *= 1.5f;
            Hero_Attributes.XPForLevelUP = Mathf.Round(Hero_Attributes.XPForLevelUP * 10f) / 10f;

            Hero_Attributes.CurrentXP = 0f;

            LevelUp();
        }

        HeroEventsArgs HeroArgs = new HeroEventsArgs(Hero_Attributes);
        HeroEvents.OnXpGainHandler?.Invoke(HeroArgs);
    }

    //Balance later :
    public void GainXpWrapper(EnemyEventArgs EnemyArgs)
    {
        float XpAmount = EnemyArgs.CurrentEnemyController.GetEnemyCurrentAttributes().CurrentLevel * 50;

        GainXp(XpAmount);
    }

    //Balance later :
    public void GainGoldWrapper(EnemyEventArgs EnemyArgs)
    {
        _heroInventory.SetGold(true, 25);
    }

    public void LevelUp()
    {
        Hero_Attributes.Lv += 1;

        // Change Later : 
        Hero_Attributes.MaxHealth += 50;
        Hero_Attributes.MaxMana += 10;
        Hero_Attributes.CurrentDamage += 10;
        Hero_Attributes.CurrentMagicDamage += 5;
        Hero_Attributes.PointsForLevelUpSckills += 1;
        Hero_Attributes.RespawnTime += 2;
        Hero_Attributes.BuyBackCost += 50;

        HeroEventsArgs HeroArgs = new HeroEventsArgs(_heroSkills.GetAllSkillsData(), Hero_Attributes, _heroInventory, 
            _heroSkills.GetAllSkillsLevels());
        HeroEvents.OnLevelUpHandler?.Invoke(HeroArgs);
    }

    private void HandleItemPickup(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemController item = other.GetComponent<ItemController>();

            int ItemsCountInInventory = _heroInventory.GetItemsCount();

            if (item != null && ItemsCountInInventory < _heroInventory.GetMaxItemsInInventory())
            {
                ItemConfig ItemsAttributes = item.GetItemData();

                int ItemAttackDamage = ItemsAttributes.GetItemDamage();
                int ItemMagicDamage = ItemsAttributes.GetItemMagicDamage();
                int ItemAttackRange = ItemsAttributes.GetItemAttackRange();
                int ItemMoveSpeed = ItemsAttributes.GetItemMoveSpeed();
                float ItemAttackSpeed = ItemsAttributes.GetItemAttackSpeed();
                int ItemHealth = ItemsAttributes.GetItemHealth();
                int ItemMana = ItemsAttributes.GetItemMana();
                int ItemHealthGain = ItemsAttributes.GetItemHealthGain();
                int ItemManaGain = ItemsAttributes.GetItemManaGain();

                AddHeroAttributes(ItemAttackDamage, ItemMagicDamage, ItemAttackRange, ItemMoveSpeed, ItemAttackSpeed,
                    ItemHealth, ItemMana, ItemHealthGain, ItemManaGain);

                _heroInventory.AddItemInInventory(item.GetItemData());

                item.DeleteItem();

                HeroEventsArgs HeroArgs = new HeroEventsArgs(_heroInventory, Hero_Attributes);
                HeroEvents.OnItemTakeHandler?.Invoke(HeroArgs);
            }
            else
            {
                Debug.Log("Can't take item because Inventory is full");
            }
        }
    }

    private void HandleItemBuy(ItemConfig Item)
    {
        if (_heroInventory.GetGold() < Item.GetItemCost())
        {
            Debug.Log("Not enough money to buy item");

            return;
        }

        if (Item != null)
        {
            _heroInventory.SetGold(false, Item.GetItemCost());

            int ItemAttackDamage = Item.GetItemDamage();
            int ItemMagicDamage = Item.GetItemMagicDamage();
            int ItemAttackRange = Item.GetItemAttackRange();
            int ItemMoveSpeed = Item.GetItemMoveSpeed();
            float ItemAttackSpeed = Item.GetItemAttackSpeed();
            int ItemHealth = Item.GetItemHealth();
            int ItemMana = Item.GetItemMana();
            int ItemHealthGain = Item.GetItemHealthGain();
            int ItemManaGain = Item.GetItemManaGain();

            AddHeroAttributes(ItemAttackDamage, ItemMagicDamage, ItemAttackRange, ItemMoveSpeed, ItemAttackSpeed,
                    ItemHealth, ItemMana, ItemHealthGain, ItemManaGain);

            _heroInventory.AddItemInInventory(Item);

            HeroEventsArgs HeroArgs = new HeroEventsArgs(_heroInventory, Hero_Attributes);
            HeroEvents.OnItemTakeHandler?.Invoke(HeroArgs);
        }
    }

    public void TryToDropItemFromInventory(int ItemIdToDrop)
    {
        if (Physics.Raycast(Ray, out _hit, 100f))
        {
            if (_hit.collider.gameObject.CompareTag("Environment"))
            {
                Debug.Log($"Can't drop item on :{_hit.collider.gameObject.tag}");
            }
            else if (_hit.collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log($"Can't drop item on :{_hit.collider.gameObject.tag}");
            }
            else if (_hit.collider.gameObject.CompareTag("Item"))
            {
                Debug.Log($"Can't drop item on :{_hit.collider.gameObject.tag}");
            }
            else if (_hit.collider.gameObject.CompareTag("Hero"))
            {
                Debug.Log($"Can't drop item on :{_hit.collider.gameObject.tag}");
            }
            else if (_hit.collider.gameObject.CompareTag("Floor"))
            {
                GameObject ItemPrefab = _heroInventory.GetItemPrefabById(ItemIdToDrop);

                if (ItemPrefab != null)
                {
                    Instantiate(ItemPrefab, _hit.point, ItemPrefab.transform.rotation);

                    ItemConfig ItemsAttributes = _heroInventory.GetItemById(ItemIdToDrop);

                    int ItemAttackDamage = ItemsAttributes.GetItemDamage();
                    int ItemMagicDamage = ItemsAttributes.GetItemMagicDamage();
                    int ItemAttackRange = ItemsAttributes.GetItemAttackRange();
                    int ItemMoveSpeed = ItemsAttributes.GetItemMoveSpeed();
                    float ItemAttackSpeed = ItemsAttributes.GetItemAttackSpeed();
                    int ItemHealth = ItemsAttributes.GetItemHealth();
                    int ItemMana = ItemsAttributes.GetItemMana();
                    int ItemHealthGain = ItemsAttributes.GetItemHealthGain();
                    int ItemManaGain = ItemsAttributes.GetItemManaGain();

                    RemoveHeroAttributes(ItemAttackDamage, ItemMagicDamage, ItemAttackRange, ItemMoveSpeed, ItemAttackSpeed,
                        ItemHealth, ItemMana, ItemHealthGain, ItemManaGain);

                    _heroInventory.RemoveItemFromInventory(ItemIdToDrop);

                    HeroEventsArgs HeroArgs = new HeroEventsArgs(_heroInventory, Hero_Attributes);
                    HeroEvents.OnItemDropHandler?.Invoke(HeroArgs);

                    Debug.Log($"Item droped on :{_hit.collider.gameObject.tag}");
                }
            }
        }
    }

    public void TryToSwapItemsInInventory(int DraggedItemId, int DroppedOnItemId)
    {
        _heroInventory.SpawItemInInventory(DraggedItemId, DroppedOnItemId);

        HeroEventsArgs HeroArgs = new HeroEventsArgs(_heroInventory);
        HeroEvents.OnItemSwapHandler?.Invoke(HeroArgs);

        Debug.Log($"Items swapped");
    }

    public void AddHeroAttributes(int AttackDamage, int MagicDamage, int AttackRange, int MoveSpeed, float AttackSpeed,
        int Health, int Mana, int HealthGain, int ManaGain)
    {
        Hero_Attributes.CurrentDamage += AttackDamage;
        Hero_Attributes.CurrentMagicDamage += MagicDamage;
        Hero_Attributes.CurrentAttackRange += AttackRange;
        Hero_Attributes.CurrentMoveSpeed += MoveSpeed;

        Hero_Attributes.CurrentAttackSpeed -= AttackSpeed;
        Hero_Attributes.CurrentAttackSpeed = Mathf.Round(Hero_Attributes.CurrentAttackSpeed * 10f) / 10f;

        Hero_Attributes.MaxHealth += Health;
        Hero_Attributes.MaxMana += Mana;
        Hero_Attributes.HealthGain += HealthGain;
        Hero_Attributes.ManaGain += ManaGain;
    }

    public void RemoveHeroAttributes(int AttackDamage, int MagicDamage, int AttackRange, int MoveSpeed, float AttackSpeed,
        int Health, int Mana, int HealthGain, int ManaGain)
    {
        Hero_Attributes.CurrentDamage -= AttackDamage;
        Hero_Attributes.CurrentMagicDamage -= MagicDamage;
        Hero_Attributes.CurrentAttackRange -= AttackRange;
        Hero_Attributes.CurrentMoveSpeed -= MoveSpeed;

        Hero_Attributes.CurrentAttackSpeed += AttackSpeed;
        Hero_Attributes.CurrentAttackSpeed = Mathf.Round(Hero_Attributes.CurrentAttackSpeed * 10f) / 10f;

        Hero_Attributes.MaxHealth -= Health;
        Hero_Attributes.MaxMana -= Mana;
        Hero_Attributes.HealthGain -= HealthGain;
        Hero_Attributes.ManaGain -= ManaGain;
    }

    public IEnumerator RespawnHero()
    {
        isDead = true;

        CurrentTarget = null;

        _heroMovement.Stop();

        _animationController.SetBool("IsDead", true);
        _animationController.ApplyRootMotion(true);

        StopCoroutine(HealthAndManaGain);

        HeroEventsArgs HeroArgs = new HeroEventsArgs(Hero_Attributes);
        HeroEvents.OnHeroDeathHandler?.Invoke(HeroArgs);

        yield return new WaitForSeconds(Hero_Attributes.RespawnTime);

        _animationController.SetBool("IsDead", false);
        _animationController.ApplyRootMotion(false);

        Transform HeroModel = transform.GetChild(0);

        HeroModel.localPosition = Vector3.zero;
        HeroModel.localRotation = Quaternion.identity;

        HealthAndManaGain = StartCoroutine(GainAttributesLoop());

        ChangeHealth(false, Hero_Attributes.MaxHealth);
        ChagneMana(true, Hero_Attributes.MaxMana);

        HeroEventsArgs HeroArgs1 = new HeroEventsArgs(Hero_Attributes);
        HeroEvents.OnHeroRespawnHandler?.Invoke(HeroArgs1);

        isDead = false;
    }

    public void BuyBack()
    {
        if (HeroInventory.GetGold() >= Hero_Attributes.BuyBackCost)
        {
            StopCoroutine(Respawn);

            _animationController.SetBool("IsDead", false);
            _animationController.ApplyRootMotion(false);

            Transform HeroModel = transform.GetChild(0);

            HeroModel.localPosition = Vector3.zero;
            HeroModel.localRotation = Quaternion.identity;

            HealthAndManaGain = StartCoroutine(GainAttributesLoop());

            ChangeHealth(false, Hero_Attributes.MaxHealth);
            ChagneMana(true, Hero_Attributes.MaxMana);

            HeroEventsArgs HeroArgs1 = new HeroEventsArgs(Hero_Attributes);
            HeroEvents.OnHeroRespawnHandler?.Invoke(HeroArgs1);

            isDead = false;

            HeroInventory.SetGold(false, Hero_Attributes.BuyBackCost);
        }
    }

    // NavMeshAgent :
    public float GetAgentMagnitude()
    {
        return _agent.velocity.magnitude;
    }

    public void SetAgentDestination(Vector3 point)
    {
        _agent.SetDestination(point);
    }

    public bool GetIsAgentStop()
    {
        return _agent.isStopped;
    }

    public void SetAgentStop(bool stopAgent)
    {
        if (stopAgent)
        {
            _agent.isStopped = true;
        }
        else
        {
            _agent.isStopped = false;
        }
    }

    public void ResetAgentPath()
    {
        _agent.ResetPath();
    }

    // Ray :
    public Vector3 GetRayOrigin()
    {
        return Ray.origin;
    }

    public Vector3 GetRayDirection()
    {
        return Ray.direction;
    }

    public Vector3 GetRayHitPoint()
    {
        return _hit.point;
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleItemPickup(other);
    }

    public void OnEnable()
    {
        AllHeroes.Add(this);

        UIEvents.OnItemDropUIHandler += TryToDropItemFromInventory;
        UIEvents.OnItemSwapUIHandler += TryToSwapItemsInInventory;

        HeroEvents.OnHeroAttackHandler += HandleOnAttackPassiveSkills;

        EnemyEvents.OnEnemyDeathHandler += GainXpWrapper;
        EnemyEvents.OnEnemyDeathHandler += GainGoldWrapper;

        UIEvents.OnBuyBackUIHandler += BuyBack;

        UIEvents.OnItemBuyUIHandler += HandleItemBuy;
    }

    public void OnDisable()
    {
        AllHeroes.Remove(this);

        UIEvents.OnItemDropUIHandler -= TryToDropItemFromInventory;
        UIEvents.OnItemSwapUIHandler -= TryToSwapItemsInInventory;

        HeroEvents.OnHeroAttackHandler -= HandleOnAttackPassiveSkills;

        EnemyEvents.OnEnemyDeathHandler -= GainXpWrapper;
        EnemyEvents.OnEnemyDeathHandler -= GainGoldWrapper;

        UIEvents.OnBuyBackUIHandler -= BuyBack;

        UIEvents.OnItemBuyUIHandler -= HandleItemBuy;
    }
}
