using System;
using System.Collections.Generic;

using Com.BaiZe.MonoToolSet;

using UnityEngine;

namespace Com.BaiZe.GameBase
{
    public enum EnumBgmID
    {

    }

    public enum EnumSoundID
    {

    }

    [RequireComponent(typeof(AudioSource))]
    public class AudioMgr : DntdMonoSingleton<AudioMgr>
    {
        public const string PATH_BG = "";

        private Dictionary<EnumBgmID, AudioClip> bgmMap = new Dictionary<EnumBgmID, AudioClip>();
        private Dictionary<EnumSoundID, AudioClip> soundMap = new Dictionary<EnumSoundID, AudioClip>();

        private Dictionary<EnumSoundID, AudioSource> audioMap = new Dictionary<EnumSoundID, AudioSource>();

        private AudioSource audioSource;
        private bool isSilent = false;

        public bool IsSilent { get => isSilent; set => isSilent = value; }

        private void Awake()
        {
            if (!TryGetComponent<AudioSource>(out audioSource))
                audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void Setup()
        {

        }

        private void LoadAsset()
        {
            AudioClip[] audioClips = Resources.LoadAll<AudioClip>(PATH_BG);
            foreach (AudioClip clip in audioClips)
            {
                bgmMap.Add((EnumBgmID)Enum.Parse(typeof(EnumBgmID), clip.name), clip);
            }
        }

        public void PlayBgm(EnumBgmID scene, bool loop = true)
        {
            if (!bgmMap.ContainsKey(scene))
            {
                Debug.LogWarning(string.Format("{0} Bgms does not exist in the bgmDict", name));
                return;
            }
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            audioSource.clip = bgmMap[scene];
            audioSource.loop = loop;
            audioSource.Play();
        }

        public AudioSource GetAvailableSource()
        {
            return new AudioSource();
        }

        public void Play3DEffect(EnumSoundID sound, Vector3 position)
        {
            if (!soundMap.ContainsKey(sound))
            {
                Debug.LogWarning(string.Format("{0} Bgms does not exist in the bgmDict", name));
                return;
            }
            AudioSource source = null;
            if (audioMap.ContainsKey(sound))
            {
                source = audioMap[sound];
            }
            else
            {
                GameObject go = new GameObject(sound.ToString());
                go.transform.position = position;
                go.transform.SetParent(transform);
                source = go.AddComponent<AudioSource>();
                source.volume = audioSource.volume;
                audioMap.Add(sound, source);
            }
            source.clip = soundMap[sound];
            source.loop = false;
            source.PlayOneShot(soundMap[sound]);
        }

        public void StopBgm()
        {
            audioSource.Stop();
        }

        public void SetVolume(float value)
        {
            audioSource.volume = value;
            if (Mathf.Approximately(value, 0f))
            {
                isSilent = true;
            }
            else
            {
                isSilent = false;
            }
            foreach (AudioSource source in audioMap.Values)
            {
                source.volume = value;
            }
        }

        public void PlayUIEffect(AudioClip clip)
        {

        }
    }
}