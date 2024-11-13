using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Code {
    public class IntroSequence : MonoBehaviour {
        public CinemachineVirtualCamera virtualCamera;
        public CinemachineTrackedDolly dolly;
        public CinemachineFramingTransposer transposer;
        public Transform player;
        public float panDuration;
        public float delayDuration;
        public float zoomDuration;
        public float panTimer = 0;
        public float delayTimer = 0;
        public float zoomTimer = 0;
        public bool panComplete;
        public bool delayComplete;
        public bool zoomComplete;
        public float startLensSize;
        public float endLensSize;

        private void Start() {
            dolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
            dolly.enabled = true;
            dolly.m_PathPosition = 0;
        }

        private void Update() {
            //Debug.Log($"Camera Position: {virtualCamera.transform.position}");

            if (!delayComplete) {
                delayTimer += Time.deltaTime;
                if (delayTimer >= delayDuration) {
                    delayComplete = true;
                }
                return;
            }

            if (!panComplete & delayComplete) {
                panTimer += Time.deltaTime;
                float progress = panTimer / panDuration;
                dolly.m_PathPosition = progress * 123f;

                if (progress >= 1) {
                    panComplete = true;
                    Vector3 finalCameraPosition = virtualCamera.transform.position;
                    dolly.enabled = false;


                    transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
                    if (transposer == null) {
                        transposer = virtualCamera.AddCinemachineComponent<CinemachineFramingTransposer>();
                    }

                    transposer.m_XDamping = 1f;  // Adjust damping values to smooth transition
                    transposer.m_YDamping = 1f;
                    transposer.m_ZDamping = 1f;

                    transposer.enabled = true;

                    virtualCamera.transform.position = finalCameraPosition;
                    virtualCamera.Follow = player;
                }
            }

            if (!zoomComplete) {
                zoomTimer += Time.deltaTime;
                float zoomProgress = zoomTimer / zoomDuration;
                virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startLensSize, endLensSize, Mathf.SmoothStep(0, 1, zoomProgress));

                if (zoomProgress >= 1) {
                    zoomComplete = true;
                    virtualCamera.m_Lens.OrthographicSize = endLensSize;
                }
            }
        }
    }
}