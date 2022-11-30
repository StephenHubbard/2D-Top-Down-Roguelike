using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;
    
    public Sound[] sounds;

    // public static AudioManager instance;

    protected override void Awake() {
        base.Awake();
        // if (instance == null) {
        //     instance = this;
        // } else {
        //     Destroy(gameObject);
        // }

        // DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clips[0];
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            switch (s.audioType) {
                case Sound.AudioTypes.sfx:
                    s.source.outputAudioMixerGroup = sfxMixerGroup;
                    break;

                case Sound.AudioTypes.music:
                    s.source.outputAudioMixerGroup = musicMixerGroup;
                    break;
            }
        }
    }

    private void Start() {
        Play("Theme Music");
    }

    public void StopMusic() {
        musicMixerGroup.audioMixer.SetFloat("Music Volume", -80f);
    }

    public void Play (string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning(("Sound: " + name + " not found!"));
            return;
        }

        int RandomClipFromSoundArray = UnityEngine.Random.Range(0, s.clips.Length);
        s.source.clip = s.clips[RandomClipFromSoundArray];

        s.source.Play();

    }

    public void UpdateMixerVolume() {
        musicMixerGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
        sfxMixerGroup.audioMixer.SetFloat("SFX Volume", Mathf.Log10(AudioOptionsManager.sfxVolume) * 20);
    }

}