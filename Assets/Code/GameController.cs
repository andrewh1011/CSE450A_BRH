using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code {
    public class GameController : MonoBehaviour {
        
        public static GameController instance;

        // Outlets
        public Transform spawnPoint;
        public GameObject enemyProjectile;
        public Transform player;

        // State Tracking
        public float firingDelay;

        // Methods
        void Awake() {
            instance = this;
        }

        void Start() {
            StartCoroutine("FiringTimer");
        }

        // Update is called once per frame
        void Update() {

        }

        void SpawnEnemyProjectile() {
            Vector2 directionToPlayer = (player.position - spawnPoint.position).normalized;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Instantiate(enemyProjectile, spawnPoint.position, rotation);
        }

        IEnumerator FiringTimer() {
            while (true) {
                yield return new WaitForSeconds(firingDelay);
                SpawnEnemyProjectile();
            }
        }
    }
}