using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Code {
    public class PlayerController : MonoBehaviour {
        //Outlet
        Rigidbody2D _rigidbody;
        public Transform aimPivot;
        public GameObject projectilePrefab;
        
        //Gamepad Configuration:
        
        

        // State Tracking
        public int jumpsLeft;

        // Dash Tracking
        enum DashDirection {
            Left,
            Right,
            NoDirection
        }

        public float dashSpeed;
        private DashDirection dashDirection;
        public float dashDuration;
        public float dashTimer;

        // Start is called before the first frame update
        void Start() {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update() {
            if (dashTimer <= 0) {
                //Move player left (A key)
                if (Input.GetKey(KeyCode.A)) {
                    _rigidbody.AddForce(Vector2.left * 18f * Time.deltaTime, ForceMode2D.Impulse);
                    dashDirection = DashDirection.Left;
                }

                //Move player right (D key)
                if (Input.GetKey(KeyCode.D)) {
                    _rigidbody.AddForce(Vector2.right * 18f * Time.deltaTime, ForceMode2D.Impulse);
                    dashDirection = DashDirection.Right;
                }
            }

            //Jump
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (jumpsLeft == 2) {
                    jumpsLeft--;
                    _rigidbody.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
                }
                else if (jumpsLeft == 1) {
                    jumpsLeft--;
                    _rigidbody.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
                }
                else if (jumpsLeft == 0 && dashTimer <= 0) {
                    dashTimer = dashDuration;
                }
            }

            // Dash (used 2d dash tutorial https://generalistprogrammer.com/unity/unity-2d-dash-movement-effect-learn-to-how-to-tutorial/)
            if (dashTimer > 0) {
                dashTimer -= Time.deltaTime;
                if (dashDirection == DashDirection.Left) {
                    _rigidbody.velocity = Vector2.left * dashSpeed;
                }
                else if (dashDirection == DashDirection.Right) {
                    _rigidbody.velocity = Vector2.right * dashSpeed;
                }

                // End dash
                if (dashTimer <= 0) {
                    _rigidbody.velocity = Vector2.zero;
                }
            }

            //Aim toward mouse
            Vector3 mousePosition = Input.mousePosition;
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

            float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
            float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

            aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

            //Shoot
            if (Input.GetMouseButtonDown(0)) {
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = aimPivot.rotation;
            }
        }

        void OnCollisionStay2D(Collision2D other) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 1.125f);

                for (int i = 0; i < hits.Length; i++) {
                    RaycastHit2D hit = hits[i];

                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                        jumpsLeft = 2;
                    }
                }
            }
             
        }
    }
}
