using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    private EnemyController _currentEnemyController;

    [SerializeField] private Slider HealthBar;
    [SerializeField] private TMP_Text HelathText;

    private Camera _camera;

    public void Init(EnemyController CurrentEnemyController)
    {
        _currentEnemyController = CurrentEnemyController;

        int CurrentHealth = _currentEnemyController.GetEnemyCurrentAttributes().CurrentHealth;
        int MaxHealth = _currentEnemyController.GetEnemyCurrentAttributes().CurrentMaxHealth;

        SetHealthBarInfo(CurrentHealth, MaxHealth);
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        HealthBar.transform.forward = _camera.transform.forward;
    }

    public void SetHealthBarInfo(float CurrentHealth, float MaxHealth)
    {
        HealthBar.value = CurrentHealth / MaxHealth;

        HelathText.text = $"{CurrentHealth} / {MaxHealth}";
    }

    public void HideHealthBarSlider()
    {
        HealthBar.gameObject.SetActive(false);
    }
}
