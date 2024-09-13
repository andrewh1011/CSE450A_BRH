using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class PlayerController : MonoBehaviour
    {
        //Outlet
        Rigidbody2D _rigidbody;
        public Transform aimPivot;
        public GameObject projectilePrefab;


        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            //Move player left (A key)
            if (Input.GetKey(KeyCode.A))
            {
                _rigidbody.AddForce(Vector2.left * 18f * Time.deltaTime, ForceMode2D.Impulse);
            }

            //Move player right (D key)
            if (Input.GetKey(KeyCode.D))
            {
                _rigidbody.AddForce(Vector2.right * 18f * Time.deltaTime, ForceMode2D.Impulse);
            }

            //Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
            }

            //Aim toward mouse
            Vector3 mousePosition = Input.mousePosition;
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

            float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
            float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

            aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

            //Shoot
            if (Input.GetMouseButtonDown(0))
            {
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = aimPivot.rotation;
            }
        }
    }

}
