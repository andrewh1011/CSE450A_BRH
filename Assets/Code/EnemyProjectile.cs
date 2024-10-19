using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code {
    public class EnemyProjectile : MonoBehaviour {

        // Outlet
        Rigidbody2D _rb;

        // State Tracking
        float arrowSpeed = 2f;
        Vector2 direction;

        void Start() {
            _rb = GetComponent<Rigidbody2D>();
            AimAtPlayer();
        }

        // Update is called once per frame
        void Update() {

            
        }

        void AimAtPlayer() {
            PlayerController player = FindObjectOfType<PlayerController>();
            direction = player.transform.position - transform.position;
            _rb.velocity = direction * arrowSpeed;
        }

        void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.GetComponent<PlayerController>()) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }

            Destroy(gameObject);
        }
    }
}