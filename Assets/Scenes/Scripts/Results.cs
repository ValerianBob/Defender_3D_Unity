using UnityEngine;

public class Results : MonoBehaviour
{
    public static Results Instance;

    public int Kills = 0;
    public int DamageDealt = 0;
    public int HealthLose = 0;
    public int GoldEarned = 0;
    public string TimePlayed = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
