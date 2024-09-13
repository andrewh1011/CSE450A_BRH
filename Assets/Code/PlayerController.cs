using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class PlayerController : MonoBehaviour
    {
        //Outlet
        Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            //Move player left (A key)
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector2.left * 18f * Time.deltaTime, ForceMode2D.Impulse);
            }

            //Move player right (D key)
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(Vector2.right * 18f * Time.deltaTime, ForceMode2D.Impulse);
            }

            //Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
            }
        }
    }

}
