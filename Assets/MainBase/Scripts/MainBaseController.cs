using UnityEngine;

public class MainBaseController : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    public int CurrentHealth;

    private MainBaseHealthUI _healthUI;

    private void Awake()
    {
        _healthUI = GetComponent<MainBaseHealthUI>();

        _healthUI.Init(this);

        CurrentHealth = _maxHealth;

        _healthUI.SetHealthBarInfo(CurrentHealth, _maxHealth);
    }

    private void Update()
    {
        if (CurrentHealth <= 0 && !GameStateController.Instance.GameOver)
        {
            GameStateController.Instance.GameOver = true;

            GameStateController.OnGameLose?.Invoke();
        }
    }

    public void GetDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }

        _healthUI.SetHealthBarInfo(CurrentHealth, _maxHealth);
    }

    public int MaxHealth
    {
        get => _maxHealth;
    }
}
