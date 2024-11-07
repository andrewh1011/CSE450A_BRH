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
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(launchSound);
        }

        public void playExplosionSound()
        {
            audioSource.PlayOneShot(explosionSound);
        }

        public void playDeathSound()
        {
            audioSource.PlayOneShot(deathSound);
        }

        public void playJumpSound()
        {
            audioSource.PlayOneShot(jumpSound);
        }

        public void playDashSound()
        {
            audioSource.PlayOneShot(dashSound);
        }
    }
}

