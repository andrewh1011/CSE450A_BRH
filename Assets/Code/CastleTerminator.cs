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
                    if (collision.gameObject.tag == "Player")
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                   
                }

            }
    }
    
}
