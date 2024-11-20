using System.Collections;
using System.Collections.Generic;
using Code;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class CastleTerminator : MonoBehaviour
    {
            private void OnCollisionEnter2D(Collision2D collision)
            {
                //Reload the scene when colliding with player
                if (collision.gameObject.GetComponent<PlayerController>())
                {
                    if (SceneManager.GetActiveScene().name == "Tutorial") {
                        SceneManager.LoadScene("Level1");
                    } else if (SceneManager.GetActiveScene().name == "Level1")
                    {
                        SceneManager.LoadScene("Level2");
                    } else if (SceneManager.GetActiveScene().name == "Level2")
                    {
                        SceneManager.LoadScene("Level1");
                    }
                    
                   
                }

            }
    }
    
}
