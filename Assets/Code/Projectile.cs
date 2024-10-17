using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class Projectile : MonoBehaviour
    {
        //Outlet
        Rigidbody2D _rigidbody2D;
        Rigidbody2D _playerRB;
        public GameObject explosionPrefab;
        public float speed;

        // Start is called before the first frame update
        void Start()
        {
            _playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.velocity = transform.right * speed + (Vector3)_playerRB.velocity / 2;
            Destroy(gameObject, 10f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                if (collision.gameObject.tag == "Head")
                {
                    Destroy(collision.gameObject.transform.parent.gameObject);
                }
                else
                {
                    Destroy(collision.gameObject);
                }
            }

            Destroy(gameObject);

            GameObject explosion = Instantiate(
                explosionPrefab, transform.position, Quaternion.identity
                );

            Destroy(explosion, 0.25f);
        }
    }

}