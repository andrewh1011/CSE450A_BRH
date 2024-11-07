using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class Explosion : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter2D(Collision2D collision)

        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))

            {
                SoundManager.instance.playDeathSound();
                if (collision.gameObject.tag == "Head")
                {
                    //If the head was hit, destroy the entire parent enemy object
                    Destroy(collision.gameObject.transform.parent.gameObject);
                }
                else
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }

}
