using System;
using System.Collections;
using System.Collections.Generic;
//using HyperCasual.Core;
using UnityEngine;
//using AudioSettings = HyperCasual.Core.AudioSettings;

namespace EndRun
{
    /// <summary>
    /// Handles playing sounds and music based on their sound ID
    /// </summary>
    public class AudioManager : Singleton<AudioManager> 
    {
        [SerializeField] AudioSource bgm;
        [SerializeField] AudioSource effect;
        
        [SerializeField] private AudioClip sndBgm;
        [SerializeField] private AudioClip sndTouch;
        [SerializeField] private AudioClip sndHurt;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        void PlayMusic(AudioClip audioClip, bool looping = true)
        {
            if (bgm.isPlaying)
                return;
            
            bgm.clip = audioClip;
            bgm.loop = looping;
            bgm.Play();
        }
        
        public void PlayMusic()
        {
            PlayMusic(sndBgm, true);
        }

        public void StopMusic()
        {
            bgm.Stop();
        }

        void PlayEffect(AudioClip audioClip)
        {
            effect.PlayOneShot(audioClip);
        }

        public void PlayTouch()
        {
            PlayEffect(sndTouch);
        }
        
        public void PlayHurt()
        {
            PlayEffect(sndHurt);
        }

    }
}