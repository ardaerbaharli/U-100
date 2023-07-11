using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Common
{
    [Serializable]
    public enum Sound
    {
        Background
    }

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;
        [SerializeField] private AudioMixer mixer;

        [SerializeField] private DictionaryUnity<Sound, AudioSource> sounds;
        [SerializeField] private DictionaryUnity<Sound, AudioSource> gameSpecificSounds;

        [Space] [SerializeField] [Range(-80, 0)]
        public int maxSoundValue = -20;

        [SerializeField] private string mainMixerParam = "Master";
        [SerializeField] private string musicMixerParam = "MusicMaster";
        [SerializeField] private string sfxMixerParam = "SFXMaster";
        [SerializeField] private string volumeLevelSuffix = "_Volume";
        public bool IsSoundOn => PlayerPrefsX.GetBool(mainMixerParam, true);
        public bool IsMusicOn => PlayerPrefsX.GetBool(musicMixerParam, true);
        public bool IsSfxOn => PlayerPrefsX.GetBool(sfxMixerParam, true);


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            // add gameSpecificSounds to sounds
            foreach (var sound in gameSpecificSounds.dictionary)
                sounds.Add(sound.key, sound.value);
        }

        private void Start()
        {
            ToggleAllSound(PlayerPrefsX.GetBool(mainMixerParam, true));
            ToggleMusicSound(PlayerPrefsX.GetBool(musicMixerParam, true));
            ToggleSfxSound(PlayerPrefsX.GetBool(sfxMixerParam, true));
        }


        public void ToggleAllSound(bool value)
        {
            if (IsSoundOn == value) return;
            PlayerPrefsX.SetBool(mainMixerParam, value);
            var mixerValue = value ? 0 : -80;
            mixer.SetFloat(mainMixerParam, mixerValue);

            ToggleMusicSound(value);
            ToggleSfxSound(value);
        }

        public void ToggleMusicSound(bool value)
        {
            if (IsMusicOn == value) return;
            if (value)
                ToggleAllSound(true);

            PlayerPrefsX.SetBool(musicMixerParam, value);
            var mixerValue = value ? PlayerPrefs.GetFloat(musicMixerParam + volumeLevelSuffix, maxSoundValue) : -80;
            mixer.SetFloat(musicMixerParam, mixerValue);
        }


        public void ToggleSfxSound(bool value)
        {
            if (IsSfxOn == value) return;
            if (value)
                ToggleAllSound(true);

            PlayerPrefsX.SetBool(sfxMixerParam, value);
            var mixerValue = value ? PlayerPrefs.GetFloat(sfxMixerParam + volumeLevelSuffix, maxSoundValue) : -80;
            mixer.SetFloat(sfxMixerParam, mixerValue);
        }

        public void SetSfxVolume(int value)
        {
            mixer.SetFloat(sfxMixerParam, value);
            PlayerPrefs.SetInt(sfxMixerParam + volumeLevelSuffix, value);

            if (value == -80)
                ToggleSfxSound(false);
            else if (value == maxSoundValue)
                ToggleSfxSound(true);
        }

        public void SetMusicVolume(int value)
        {
            mixer.SetFloat(musicMixerParam, value);
            PlayerPrefs.SetInt(musicMixerParam + volumeLevelSuffix, value);

            if (value == -80)
                ToggleMusicSound(false);
            else if (value == maxSoundValue)
                ToggleMusicSound(true);
        }

        public void PlaySound(string sound)
        {
            sounds[(Sound) Enum.Parse(typeof(Sound), sound)].Play();
        }

        public void PlaySound(Sound sound)
        {
            sounds[sound].Play();
        }

        public void StopPlaying(string sound)
        {
            sounds[(Sound) Enum.Parse(typeof(Sound), sound)].Stop();
        }

        public void StopPlaying(Sound sound)
        {
            sounds[sound].Stop();
        }

        public void PauseAllSounds()
        {
            foreach (var sound in sounds.Values) sound.Pause();
        }

        public void UnPauseAllSounds()
        {
            foreach (var sound in sounds.Values) sound.UnPause();
        }

        public void StopPlayingAllSounds()
        {
            foreach (var sound in sounds.Values) sound.Stop();
        }
    }
}