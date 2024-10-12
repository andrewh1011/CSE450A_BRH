using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class EnemyController : MonoBehaviour
    {

        public float speed = 1f;

        Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (isFacingRight())
            {
                rb.velocity = new Vector2(speed, 0f);
            } else
            {
                rb.velocity = new Vector2(-speed, 0f);
            }
        }

        //From https://www.youtube.com/watch?v=MPnN9i1SD6g
        private bool isFacingRight()
        {
            return transform.localScale.x > Mathf.Epsilon;
        }


        //From https://www.youtube.com/watch?v=MPnN9i1SD6g
        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                transform.localScale = new Vector2((-Mathf.Sign(rb.velocity.x)), transform.localScale.y);
            }
            
        }


        void OnCollisionEnter2D(Collision2D collision)
        {
            //Reload the scene when colliding with player
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }

            // Kill the enemy if colliding with the player projectile
            if (collision.gameObject.GetComponent<Projectile>())
            {
                Destroy(gameObject);
            }
        }
    }

}