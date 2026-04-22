using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDeath : MonoBehaviour
{
    [SerializeField] private GameObject DeathWindow;

    [SerializeField] private TMP_Text RespawnTimeText;

    [SerializeField] private TMP_Text BuyBackCostText;

    [SerializeField] private Button BuyBackButton;

    private void ShowDeathWindow(HeroEventsArgs HeroArgs)
    {
        DeathWindow.gameObject.SetActive(true);

        StartCoroutine(CountRespawnTimer(HeroArgs.CurrentHeroAttributes.RespawnTime));
    }

    private void HideDeathWindow(HeroEventsArgs HeroArgs)
    {
        DeathWindow.gameObject.SetActive(false);
    }

    private IEnumerator CountRespawnTimer(int RespawnTime)
    {
        for (int i = RespawnTime; i > 0; i--)
        {
            RespawnTimeText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        RespawnTimeText.text = "0";
    }

    private void OnEnable()
    {
        HeroEvents.OnHeroDeathHandler += ShowDeathWindow;
        HeroEvents.OnHeroRespawnHandler += HideDeathWindow;
    }

    private void OnDisable()
    {
        HeroEvents.OnHeroDeathHandler -= ShowDeathWindow;
        HeroEvents.OnHeroRespawnHandler -= HideDeathWindow;
    }
}
