using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;
        public static bool music = true;
        public static bool sfx = true;

        //Outlets
        AudioSource audioSource;
        public AudioClip launchSound;
        public AudioClip bulletSound;
        public AudioClip explosionSound;
        public AudioClip deathSound;
        public AudioClip jumpSound;
        public AudioClip dashSound;


        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void playLaunchSound()
        {
            if(sfx) {
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.PlayOneShot(launchSound);
            }
        }

        public void playBulletSound()
        {
            if (sfx)
            {
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.PlayOneShot(bulletSound);
            }
        }

        public void playExplosionSound()
        {
            if (sfx) audioSource.PlayOneShot(explosionSound);
        }

        public void playDeathSound()
        {
            if (sfx) audioSource.PlayOneShot(deathSound);
        }

        public void playJumpSound()
        {
            if (sfx) audioSource.PlayOneShot(jumpSound);
        }

        public void playDashSound()
        {
            if (sfx) audioSource.PlayOneShot(dashSound);
        }

        public void toggleMusic() {
            music = !music;
        }

        public void toggleSFX() { 
            sfx = !sfx;
        }
    }
}

