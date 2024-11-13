using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Code {
    public class PlayerController : MonoBehaviour {
        //Outlet
        public Transform aimPivot;
        public GameObject projectilePrefab;
        public GameObject bulletPrefab;
        SpriteRenderer sprite;
        Animator animator;
        public GameObject launcher;
        SpriteRenderer launcherSprite;
        public Sprite bulletSprite;
        Rigidbody2D _rigidbody;

        //Movement
        public float gravityScale;
        public float gravityMultiplier;
        public float jumpForce;

        public float recoil;
        public float shootCooldown;
        public float shootTimer;
        public float shootOffset;

        //Jump Cut Variables
        public float jumpTime;
        private float jumpTimeCounter;
        private bool isJumping;

        //Gamepad Configuration:


        // State Tracking
        public int jumpsLeft;
        public int dashesLeft;

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
            sprite = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            launcherSprite = launcher.GetComponent<SpriteRenderer>();

            dashDirection = DashDirection.Right;

            _rigidbody.gravityScale = gravityScale;
            isJumping = false;
        }

        void FixedUpdate()
        {
            RotateLauncher();
            animator.SetFloat("Speed", _rigidbody.velocity.magnitude);

            if (_rigidbody.velocity.magnitude > 0)
            {
                animator.speed = _rigidbody.velocity.magnitude / 4f;
            }
            else
            {
                animator.speed = 1f;
            }
        }

        // Update is called once per frame
        void Update() {
            
       

            if (shootTimer > 0) {
                shootTimer -= Time.deltaTime;
            }

            if (dashTimer <= 0) {
                //Move player left (A key)
                if (Input.GetKey(KeyCode.A)) {
                    _rigidbody.AddForce(Vector2.left * 22f * Time.deltaTime, ForceMode2D.Impulse);
                    dashDirection = DashDirection.Left;
                    sprite.flipX = true;

                }

                //Move player right (D key)
                if (Input.GetKey(KeyCode.D)) {
                    _rigidbody.AddForce(Vector2.right * 22f * Time.deltaTime, ForceMode2D.Impulse);
                    dashDirection = DashDirection.Right;
                    sprite.flipX = false;
                }
            }

            //Jump
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (jumpsLeft >= 1) {
                    SoundManager.instance.playJumpSound();
                    
                    isJumping = true;
                    jumpsLeft--;
                    jumpTimeCounter = jumpTime;
                    _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
                else if (jumpsLeft == 0 && dashTimer <= 0 && dashesLeft >= 1) {

                    SoundManager.instance.playDashSound();

                    dashTimer = dashDuration;

                    dashesLeft--;
                }
            }

            //Jump cut, from https://www.youtube.com/watch?v=j111eKN8sJw
            if (Input.GetKey(KeyCode.Space) && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    //_rigidbody.velocity += Vector2.up * jumpForce * Time.fixedDeltaTime;
                    _rigidbody.AddForce(Vector2.up * (jumpForce/5) * Time.fixedDeltaTime, ForceMode2D.Impulse);
                    jumpTimeCounter -= Time.deltaTime;
                } else
                {
                    isJumping = false;
                }
                
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
            }

            //Increase gravity scale when falling
            if (jumpsLeft == 0)
            {
                if (_rigidbody.velocity.y < 0)
                {
                 
                    _rigidbody.gravityScale = gravityScale * gravityMultiplier;
                } else
                {
                    _rigidbody.gravityScale = gravityScale;
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
            Vector2 mousePosition = CinemachineCore.Instance.GetActiveBrain(0).OutputCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3 directionFromPlayerToMouse = mousePosition - (Vector2)transform.position;

            float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
            float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

            aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

            //Shoot
            if (Input.GetMouseButtonDown(0) && shootTimer <= 0) {
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = launcher.transform.position + launcher.transform.right * shootOffset;
                newProjectile.transform.rotation = aimPivot.rotation;

                _rigidbody.AddForce(-launcher.transform.right * recoil, ForceMode2D.Impulse);

                shootTimer = shootCooldown;
            }

            animator.SetInteger("jumpsLeft", jumpsLeft);
        }

        void OnCollisionStay2D(Collision2D other) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.up, 1.125f);

                for (int i = 0; i < hits.Length; i++) {
                    RaycastHit2D hit = hits[i];

                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                        return;
                    }
                }

                _rigidbody.gravityScale = gravityScale;
                jumpsLeft = 1;
                dashesLeft = 1;
                dashTimer = 0f;
                isJumping = false;
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)

        {
            //From https://discussions.unity.com/t/push-object-in-opposite-direction-of-collision/153430
            if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile"))
            {
                // Calculate Angle Between the collision point and the player
                Vector3 direction = collision.contacts[0].point - (Vector2)transform.position;
                // We then get the opposite (-Vector3) and normalize it
                direction = -direction.normalized;
                // And finally we add force in the direction of dir and multiply it by force. 
                // This will push back the player
                _rigidbody.AddForce(direction * 8f, ForceMode2D.Impulse);
            }

            if (collision.gameObject.tag == "Head")
            {
                SoundManager.instance.playDeathSound();
                _rigidbody.gravityScale = gravityScale;
                jumpsLeft = 1;
                dashesLeft = 1;
                dashTimer = 0f;
                Destroy(collision.gameObject.transform.parent.gameObject);
            }

            if (collision.gameObject.tag == "Powerup")
            {
                projectilePrefab = bulletPrefab;
                launcherSprite.sprite = bulletSprite;
                Destroy(collision.gameObject);
            }


        }

        void RotateLauncher()
        {
            //Rotate gun to mouse position
            Vector2 mousePosition = CinemachineCore.Instance.GetActiveBrain(0).OutputCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 launcherToMouse = (mousePosition - (Vector2)launcher.transform.position).normalized;
            launcher.transform.right = launcherToMouse;

            


            //Flips run if looking behind
            float launcherAngle = Mathf.Atan2(launcherToMouse.y, launcherToMouse.x) * Mathf.Rad2Deg;

            //Debug.Log($"Mouse World Pos: {mousePosition}, LauncherToMouse: {launcherToMouse}, Angle: {launcherAngle}");

            Vector3 localScale = new Vector3(0.6f, 0.6f, 0.6f);
            if (launcherAngle > 90 || launcherAngle < -90)
            {
                localScale.y = -0.6f;
            }
            else
            {
                localScale.y = 0.6f;
            }

            launcher.transform.localScale = localScale;
        }
    }

 
}
