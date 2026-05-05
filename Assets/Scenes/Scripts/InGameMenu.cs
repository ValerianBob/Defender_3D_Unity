using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _inGameMenuWindow;

    [SerializeField] private Button Resume;
    [SerializeField] private Button Settings;
    [SerializeField] private Button Quit;

    [SerializeField] private GameObject Timer;
    [SerializeField] private GameObject EnemiesWavesInfo;
    [SerializeField] private GameObject HeroPanel;
    [SerializeField] private GameObject DeathPanel;
    [SerializeField] private GameObject Shop;
    [SerializeField] private GameObject InfoWindow;

    private bool shopWasOppened = false;
    private bool deathPanelWasOppened = false;

    private bool isOppened = false;

    private void Start()
    {
        Resume.onClick.AddListener(ToggleMenuWindow);
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ToggleMenuWindow();
        }
    }

    private void ToggleMenuWindow()
    {
        isOppened = !isOppened;

        _inGameMenuWindow.SetActive(isOppened);

        if (isOppened)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        TogglePlayerUI(!isOppened);
    }

    private void TogglePlayerUI(bool toggle)
    {
        Timer.SetActive(toggle);
        EnemiesWavesInfo.SetActive(toggle);
        HeroPanel.SetActive(toggle);

        if (!toggle && DeathPanel.activeSelf)
        {
            deathPanelWasOppened = true;
            DeathPanel.SetActive(false);
        }
        if (toggle && deathPanelWasOppened)
        {
            DeathPanel.SetActive(true);
            deathPanelWasOppened = false;
        }

        if (!toggle && Shop.activeSelf)
        {
            shopWasOppened = true;
            Shop.SetActive(false);
        }
        if (toggle && shopWasOppened)
        {
            Shop.SetActive(true);
            shopWasOppened = false;
        }

        InfoWindow.SetActive(false);
    }
}
