using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private GameObject GameOverWindow;

    [SerializeField] private TMP_Text Title;

    [SerializeField] private TMP_Text Kills;
    [SerializeField] private TMP_Text DamageDealt;
    [SerializeField] private TMP_Text HealthLose;
    [SerializeField] private TMP_Text GoldEarned;
    [SerializeField] private TMP_Text TimePlayed;

    [SerializeField] private GameObject Timer;
    [SerializeField] private GameObject EnemiesWavesInfo;
    [SerializeField] private GameObject HeroPanel;
    [SerializeField] private GameObject DeathPanel;
    [SerializeField] private GameObject Shop;
    [SerializeField] private GameObject InfoWindow;

    private void SetWin()
    {
        GameOverWindow.SetActive(true);

        Title.text = "You Won";
        Title.color = Color.green;

        ShowResults();

        HidePlayerUI();
    }

    private void SetLose()
    {
        GameOverWindow.SetActive(true);

        Title.text = "You Lose";
        Title.color = Color.red;

        ShowResults();

        HidePlayerUI();
    }

    private void ShowResults()
    {
        Kills.text = $"Kills : {Results.Instance.Kills}";
        DamageDealt.text = $"Damage Dealt : {Results.Instance.DamageDealt}";
        HealthLose.text = $"Health Lose : {Results.Instance.HealthLose}";
        GoldEarned.text = $"Gold Earned : {Results.Instance.GoldEarned}";
        TimePlayed.text = $"Time Played : {Results.Instance.TimePlayed}";
    }

    private void HidePlayerUI()
    {
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

        GameStateController.OnGameLose += SetLose;
    }

    private void OnDisable()
    {
        GameStateController.OnGameWin -= SetWin;

        GameStateController.OnGameLose -= SetLose;
    }
}
