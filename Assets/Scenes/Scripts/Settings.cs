using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings instance;

    public float MusicVolume = 1.0f;
    public float GeneralVolume = 1.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
