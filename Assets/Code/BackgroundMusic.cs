using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code {
    public class BackgroundMusic : MonoBehaviour {
        private AudioSource audioSource;

        void Start() {
            audioSource = GetComponent<AudioSource>();
        }

        void Update() {
            audioSource.mute = !SoundManager.music;
        }
    }
}