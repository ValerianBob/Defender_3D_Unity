using UnityEngine;

public class UIWin : MonoBehaviour
{
    [SerializeField] private GameObject WinWindow;

    [SerializeField] private GameObject Timer;
    [SerializeField] private GameObject EnemiesWavesInfo;
    [SerializeField] private GameObject HeroPanel;
    [SerializeField] private GameObject DeathPanel;
    [SerializeField] private GameObject Shop;
    [SerializeField] private GameObject InfoWindow;

    private void SetWin()
    {
        WinWindow.SetActive(true);

        Timer.SetActive(false);
        EnemiesWavesInfo.SetActive(false);
        HeroPanel.SetActive(false);
        DeathPanel.SetActive(false);
        Shop.SetActive(false);
        InfoWindow.SetActive(false);
    }

    private void OnEnable()
    {
        GameStateController.OnGameWin += SetWin;
    }

    private void OnDisable()
    {
        GameStateController.OnGameWin -= SetWin;
    }
}
