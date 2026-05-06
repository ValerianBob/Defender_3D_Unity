using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Sound Library")]
public class SoundLibrary : ScriptableObject
{
    public List<SoundData> Sounds;

    private Dictionary<SoundType, SoundData> _library;

    public void Init()
    {
        _library = new Dictionary<SoundType, SoundData>();

        foreach (var sound in Sounds)
        {
            _library[sound.Type] = sound;
        }
    }

    public SoundData GetByType(SoundType type)
    {
        return _library[type];
    }
}
