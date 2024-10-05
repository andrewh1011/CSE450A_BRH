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
        
        void UpdateSprite(GameObject digitObject, int number)
        {
            //print("Updating sprite: " + digitObject.name);

            //print("Updating with: " + number);
            
            
            SpriteRenderer spriteRenderer = digitObject.GetComponent<SpriteRenderer>();
            
            //print("Our sprite renderer of " + digitObject.name + " is " + spriteRenderer.sprite);
            
            if (spriteRenderer != null && number < numberSprites.Length)
            {
                spriteRenderer.sprite = numberSprites[number];
            }
            
        }

        void UpdateTimerDisplay()
        {
            //print("Timer Digits Count: " + timerDigits.Length);
            
            // Calculate minutes and seconds
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            
            //print("Time: " + minutes + ":" + seconds);
            
            //print("timerDigits[0] is: " + timerDigits[0]);
            
            //Update the digit sprites
            UpdateSprite(timerDigits[0], minutes % 10);
            UpdateSprite(timerDigits[1], seconds / 10);
            UpdateSprite(timerDigits[2], seconds % 10);
        }
        
        void Start()
        {
            timer = timeLimit;
            UpdateTimerDisplay();

        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;
            //timerText.text = levelTimer.ToString("0.00");

            if (timer <= 0)
            {
                timer = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
                print("Round is over because time ran out, scene has restarted.");
            }
            
            UpdateTimerDisplay();
        }
    }
    
}
