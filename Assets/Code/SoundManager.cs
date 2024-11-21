using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

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
            if(Settings.toggleSFX) {
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.PlayOneShot(launchSound);
            }
        }

        public void playBulletSound()
        {
            if (Settings.toggleSFX)
            {
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.PlayOneShot(bulletSound);
            }
        }

        public void playExplosionSound()
        {
            if (Settings.toggleSFX) audioSource.PlayOneShot(explosionSound);
        }

        public void playDeathSound()
        {
            if (Settings.toggleSFX) audioSource.PlayOneShot(deathSound);
        }

        public void playJumpSound()
        {
            if (Settings.toggleSFX) audioSource.PlayOneShot(jumpSound);
        }

        public void playDashSound()
        {
            if (Settings.toggleSFX) audioSource.PlayOneShot(dashSound);
        }

        public void toggleMusic() {
            Settings.toggleMusic = !Settings.toggleMusic;
        }

        public void toggleSFX() { 
            Settings.toggleSFX = !Settings.toggleSFX;
        }
    }
}

