using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris.Audio
{
    [Serializable]
    public class AudioClipSettings
    {
        public AudioClip Clip;
        public string Name;

        [Range(0f, 1f)]
        public float Volume = 1f;
    }

    public class AudioManager : MonoBehaviour, IAudioManager
    {
        private Stack<AudioSource> _audioSourcePool = new Stack<AudioSource>();

        [SerializeField]
        private List<AudioClipSettings> _audioClips;

        public void Play(string clipName)
        {
            AudioSource soundSource = GetAudiosource();
            var clipSettings = GetClipSettings(clipName);
            StartCoroutine(PlayClip(soundSource, clipSettings));
        }

        private IEnumerator PlayClip(AudioSource audioSource, AudioClipSettings clipSettings)
        {
            audioSource.clip = clipSettings.Clip;
            audioSource.volume = clipSettings.Volume;
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            _audioSourcePool.Push(audioSource);
        }

        private AudioClipSettings GetClipSettings(string clipName)
        {
            foreach (var audioClip in _audioClips)
            {
                if (audioClip.Name == clipName)
                {
                    return audioClip;
                }
            }

            return null;
        }

        private AudioSource GetAudiosource()
        {
            if (_audioSourcePool.Count > 0)
            {
                return _audioSourcePool.Pop();
            }

            return gameObject.AddComponent<AudioSource>();
        }
    }
}