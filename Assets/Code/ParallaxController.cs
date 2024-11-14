using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code {
    public class ParallaxController : MonoBehaviour {
        //used this tutorial https://medium.com/@Code_With_K/parallax-background-in-unity-fd8766d5a9bd
        private float length, startpos;
        public GameObject cam;
        public float parallaxEffect;
        private Vector3 lastCameraPosition;

        void Start() {
            startpos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
            lastCameraPosition = cam.transform.position;
        }

        void Update() {
            float deltaX = (cam.transform.position.x - lastCameraPosition.x);
            float temp = (cam.transform.position.x * (1 - parallaxEffect));
            float dist = deltaX * parallaxEffect;

            transform.position = new Vector3(transform.position.x + dist, transform.position.y, transform.position.z);

            if (temp > startpos + length) startpos += length;
            else if (temp < startpos - length) startpos -= length;

            lastCameraPosition = cam.transform.position;
        }
    }
}