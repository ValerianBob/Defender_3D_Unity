using UnityEngine;

public enum SoundType
{
    Attack,
    Music
}

public enum AudioChannel
{
    Music,
    SFX,
    UI,
    Ambient
}

[CreateAssetMenu(menuName = "Audio/Sound")]
public class SoundData : ScriptableObject
{
    public SoundType Type;
    public AudioChannel Channel;

    public AudioClip[] Clips;

    [Range(0f, 1f)] public float Volume = 1f;
    [Range(0.5f, 2f)] public float Pitch = 1f;

    public bool Loop;
}
