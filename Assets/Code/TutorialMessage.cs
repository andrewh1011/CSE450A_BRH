using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code {
    public class TutorialMessage : MonoBehaviour {
        public Sprite[] letterSprites = new Sprite[26];

        public GameObject[] letterObjects;

        private string msg = "use a and d to move left and right   spacebar will allow you to jump or to dash if you have already jumped   use the cursor to aim and shoot your weapon at enemies   collect powerups to upgrade your weapon   make sure to finish levels in time without being hit by any arrows   reach the castle to move on";

        void Start() {
            letterObjects = new GameObject[msg.Length];

            for (int i = 0; i < msg.Length; i++) {
                letterObjects[i] = new GameObject("Letter_" + i);
                letterObjects[i].transform.parent = transform;

                letterObjects[i].transform.localPosition = new Vector3(i * 1.0f, 0, 0);

                SpriteRenderer spriteRenderer = letterObjects[i].AddComponent<SpriteRenderer>();

                char letter = char.ToLower(msg[i]);
                if (letter >= 'a' && letter <= 'z') {
                    int spriteIndex = letter - 'a';
                    spriteRenderer.sprite = letterSprites[spriteIndex];
                } else if (letter == ' ') {
                    spriteRenderer.sprite = null;
                }
            }
        }
    }
}
