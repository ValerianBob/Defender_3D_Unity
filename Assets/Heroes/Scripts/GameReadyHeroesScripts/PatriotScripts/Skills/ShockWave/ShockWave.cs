using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShockWave", menuName = "Skills/ShockWave")]
public class ShockWave : Skill
{
    public float DetectRange;

    [SerializeField] private List<float> nearDamageMultipliers;

    public float DamageForNearEnemies;

    public ShockWave(SkillConfig config) : base(config)
    {
    }

    public override void InitSkill(string name, string description, int CoolDown, int ManaCost, int SkillDuration)
    {
        CurrentSkillLevel = 0;

        this.SkillName = name;
        this.SkillDescription = description;

        this.CoolDown = CoolDown;
        this.ManaCost = ManaCost;
        this.SkillDuration = SkillDuration;

        isReloading = false;

        nearDamageMultipliers.Clear();

        nearDamageMultipliers.Add(0.5f);
        nearDamageMultipliers.Add(0.7f);
        nearDamageMultipliers.Add(0.85f);
        nearDamageMultipliers.Add(1f);

        DamageForNearEnemies = nearDamageMultipliers[CurrentSkillLevel];

        DetectRange = 10f;
    }

    public override void Execute(HeroController CurrentHeroController)
    {
        List<EnemyController> NearEnemies = EnemyController.AllEnemies;

        NearEnemies.Remove(CurrentHeroController.CurrentTarget.GetComponent<EnemyController>());

        foreach (var enemy in NearEnemies)
        {
            if (enemy == null)
            {
                continue;
            }
                
            if (Vector3.Distance(CurrentHeroController.transform.position, enemy.transform.position) > DetectRange)
            {
                continue;
            }

            float damage = CurrentHeroController.Hero_Attributes.CurrentDamage * DamageForNearEnemies;

            int finalDamage = Mathf.RoundToInt(damage);

            enemy.ChangeHealth(true, finalDamage);

            Results.Instance.DamageDealt += finalDamage;
        }

        Debug.Log($"ShockWave hit");
    }

    public override void UpdateSkill()
    {
        DamageForNearEnemies = nearDamageMultipliers[CurrentSkillLevel - 1];
    }
}
