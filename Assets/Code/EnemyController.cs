using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class EnemyController : MonoBehaviour
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

            // Kill the enemy if colliding with the player projectile
            if (collision.gameObject.GetComponent<Projectile>())
            {
                Destroy(gameObject);
            }
        }
    }

}