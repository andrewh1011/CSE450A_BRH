using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Code
{
    public class Timer : MonoBehaviour {
        // Start is called before the first frame update
        public float timeLimit = 30.0f;

        public float timer;

        //Array to hold number sprites (0-9)
        public Sprite[] numberSprites = new Sprite[10];

        //Array of GameObjects to display each digit
        public GameObject[] timerDigits = new GameObject[3];
        public GameObject levelTimer;
        
        void UpdateSprite(GameObject digitObject, int number)
        {
            //print("Updating sprite: " + digitObject.name);

            //print("Updating with: " + number);
            
            
            Image image = digitObject.GetComponent<Image>();
            
            //print("Our sprite renderer of " + digitObject.name + " is " + spriteRenderer.sprite);
            
            if (image != null && number < numberSprites.Length)
            {
                image.sprite = numberSprites[number];
            }
            
        }

        void UpdateTimerDisplay()
        {
            if (!Settings.timerModifier) return;
            //print("Timer Digits Count: " + timerDigits.Length);

            // Calculate minutes and seconds
            //int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer);

            //print("Time: " + minutes + ":" + seconds);

            //print("timerDigits[0] is: " + timerDigits[0]);

            //Update the digit sprites
            UpdateSprite(timerDigits[0], (seconds / 100) % 10);
            UpdateSprite(timerDigits[1], (seconds / 10) % 10);
            UpdateSprite(timerDigits[2], seconds % 10);
        }
        
        void Start()
        {

            timer = timeLimit;
            UpdateTimerDisplay();
            if (levelTimer != null) {
                levelTimer.SetActive(Settings.timerModifier);
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (IntroSequence.panComplete && Settings.timerModifier) {
                timer -= Time.deltaTime;
                //timerText.text = levelTimer.ToString("0.00");

                if (timer <= 0) {
                    timer = 0;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }

                UpdateTimerDisplay();
            }
        }

        public void toggleTimer() {
            Settings.timerModifier = !Settings.timerModifier;
            if (levelTimer != null) {
                levelTimer.SetActive(Settings.timerModifier);
            } 
        }
    }
    
}
