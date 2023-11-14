using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private Sound[] audioSources;
    [System.Serializable]
    private struct Sound {
        public SoundType type;
        public AudioSource audioSource;
    }

    private Dictionary<SoundType, AudioSource> sounds = new Dictionary<SoundType, AudioSource>();

    public bool isMuted = false;

    public bool IsMuted {
        get => isMuted;
        set => isMuted = value;
    }

    public bool IsLoaded(SoundType type) {
        return sounds.ContainsKey(type);
    }

    public void Play(SoundType type) {
        if (isMuted) return;
        if (!IsLoaded(type)) {
            sounds.Add(type, GetAudio(type));
            sounds[type].Play();
        }
        sounds[type].Play();
    }

    public void Mute(SoundType type) {
        foreach (var item in sounds) {
            if (!item.Value) {
                item.Value.Stop();
            }
        }
    }

    public void MuteAll() {
        isMuted = true;
        foreach (var item in sounds) {
            item.Value.Stop();
        }
    }

    public AudioSource GetAudio(SoundType type) {
        for (int i = 0; i < audioSources.Length; ++i) {
            if (audioSources[i].type == type) {
                return audioSources[i].audioSource;
            }
        }

        return null;
    }

}