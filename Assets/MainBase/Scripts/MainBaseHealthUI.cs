using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainBaseHealthUI : MonoBehaviour
{
    private MainBaseController _mainBaseController;

    [SerializeField] private Slider HealthBar;
    [SerializeField] private TMP_Text HelathText;

    private Camera _camera;

    public void Init(MainBaseController MainBaseController)
    {
        _mainBaseController = MainBaseController;

        int CurrentHealth = _mainBaseController.CurrentHealth;
        int MaxHealth = _mainBaseController.MaxHealth;
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
