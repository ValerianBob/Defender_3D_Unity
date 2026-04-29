using System.Collections;
using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    [SerializeField] private TMP_Text TimerText;

    public int Seconds;
    public int Minutes;

    private void Start()
    {
        StartCoroutine(CountTimer());
    }

    private IEnumerator CountTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            Seconds += 1;

            if (Seconds >= 60)
            {
                Seconds = 0;
                Minutes += 1;
            }

            TimerText.text = $"{Minutes:00}:{Seconds:00}"; ;
        }
    }
}
