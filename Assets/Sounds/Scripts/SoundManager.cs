using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private int _poolZise = 20;

    private List<AudioSource> _pool = new List<AudioSource>();

    [Range(0f, 1f)] public float MusicVolume = 1.0f;
    [Range(0f, 1f)] public float GeneralVolume = 1.0f;

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

        CreatePool();
    }

    private void Update()
    {
        GeneralVolume = Settings.instance.GeneralVolume;
        MusicVolume = Settings.instance.MusicVolume;
    }

    public void PlaySound(SoundData Sound, Vector3? pos = null)
    {
        var source = GetFreeSource();

        source.clip = GetRandomClip(Sound);

        source.volume = GetFinalVolume(Sound);
        source.pitch = Sound.Pitch;
        source.loop = Sound.Loop;

        if (pos.HasValue)
        {
            source.transform.position = pos.Value;
            source.spatialBlend = 1f;
        }
        else
        {
            source.spatialBlend = 0f;
        }

        source.Play();
    }

    private void CreatePool()
    {
        for (int i = 0; i < _poolZise; i++)
        {
            GameObject go = new GameObject("AudioSource_" + i);
            go.transform.parent = transform;

            var source = go.AddComponent<AudioSource>();
            source.playOnAwake = false;
            _pool.Add(source);
        }
    }

    private AudioSource GetFreeSource()
    {
        foreach (var s in _pool)
        {
            if (!s.isPlaying)
            {
                return s;
            }
        }

        return _pool[0];
    }
    
    private AudioClip GetRandomClip(SoundData data)
    {
        return data.Clips[Random.Range(0, data.Clips.Length)];
    }

    public void SetMusicVolume(float value)
    {
        MusicVolume = value;
    }

    public void SetGeneralVolume(float value)
    {
        GeneralVolume = value;
    }

    private float GetFinalVolume(SoundData sound)
    {
        float typeVolume = sound.Type switch
        {
            SoundType.Music => MusicVolume,
            _ => GeneralVolume
        };

        return sound.Volume * typeVolume;
    }
}
