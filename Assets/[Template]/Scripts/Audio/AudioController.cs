using System;
using UnityEngine;

namespace Template
{
    public class AudioController : BaseMono, IModuleInit
    {
        [Header("Looping Clips")]
        public Sound[] loopSounds;

        [Header("OneShot Clips")]
        public Sound[] oneShotSounds;

        private Sound _currentlyLooping;
        
        public void Init()
        {
            foreach (Sound sound in loopSounds)
            {
                AddSourceWithSound(sound);
            }
            foreach (Sound sound in oneShotSounds)
            {
                AddSourceWithSound(sound);
            }
        }

        private void AddSourceWithSound(Sound sound)
        {
            GameObject emptyObject = new GameObject();
            emptyObject.transform.SetParent(Transform);
            emptyObject.transform.name = "Sound_" + sound.name;
            
            sound.source = emptyObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.basePitch;
            sound.source.loop = sound.isLooping;
        }

        public void PlayLooping(SoundType soundType, float speedMult = 1f)
        {
            Sound s = Array.Find(loopSounds, x => x.soundType == soundType);

            if (s is null) Debug.LogWarning("sound with name : " + soundType.ToString() + " : not found");
            else
            {
                s.source.Play();
                _currentlyLooping = s;
            }
        }
        public void PlayOneShot(SoundType soundType, float speedMult = 1f, bool canCallMultipleTimes = false)
        {
            // if (canCallMultipleTimes == true) { } // nothing
            // else if (name != "" && name == currentOneShot && oneShotSource.isPlaying == true) return;

            Sound s = Array.Find(oneShotSounds, x => x.soundType == soundType);

            if (s is null) Debug.LogWarning("sound with name : " + name + " : not found");
            else
            {
                s.source.Play();
            }
        }

        public void StopLooping()
        {
            if (_currentlyLooping is null) 
                return;
        
            _currentlyLooping.source.Stop();

            _currentlyLooping = null;
        }



        // private float volumeLooping;
        // private float volumeOneShot;
        // public void Mute(bool muted)
        // {
        //     if(muted == true)
        //     {
        //         volumeLooping = loopingSource.volume;
        //         volumeOneShot = oneShotSource.volume;
        //         loopingSource.volume = 0f;
        //         oneShotSource.volume = 0f;
        //     }       
        //     else
        //     {
        //         loopingSource.volume = volumeLooping;
        //         oneShotSource.volume = volumeOneShot;
        //     }
        // }
    }
}