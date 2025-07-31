using Cinemachine;
using GorillaLocomotion;
using UnityEngine;

namespace GorillaCam.Behaviours.CameraStuff.ModeScripts
{
    internal class DynamicFPV : MonoBehaviour
    {
        private MainCamara _mainCam;

        private void Start()
        {
            if (Core.currentMode != cameraMode.DYNAMIC_FPV) return;

            Tools.Logger.Log("DynamicFPC: Setting up camera...");
            GameObject tempCam = GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer/TurnParent/Main Camera/");
            if (tempCam == null) return;

            var cam = tempCam.GetComponent<Camera>();
            var listener = tempCam.GetComponent<AudioListener>();
            if (cam == null || listener == null) return;

            Tools.Logger.Log("DynamicFPC: Setting up components...");
            _mainCam = new MainCamara
            {
                Camera = cam,
                Listener = listener,
                Object = tempCam,
            };

            Tools.Logger.Log("DynamicFPC: Setting POS...");
            if (GorillaTagger.Instance?.headCollider != null)
            {
                _mainCam.Object.transform.position = GorillaTagger.Instance.headCollider.transform.position;
                _mainCam.Object.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;
            }
        }

        private void Update()
        {
            if (Core.currentMode == cameraMode.DYNAMIC_FPV && _mainCam.Object != null)
            {
                if (GorillaTagger.Instance?.headCollider != null)
                {
                    _mainCam.Object.transform.position = GorillaTagger.Instance.headCollider.transform.position;
                    _mainCam.Object.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;

                    var thirdPersonCam = GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<Camera>();
                    if (thirdPersonCam != null)
                    {
                        thirdPersonCam.transform.position = _mainCam.Object.transform.position;
                        thirdPersonCam.transform.rotation = _mainCam.Object.transform.rotation;
                        
                        float speed = GTPlayer.Instance.RigidbodyVelocity.magnitude;
                        float targetFOV = Mathf.Clamp(90 + speed * 6f, 80f, 130f);
                        float currentFOV = 0;
                        currentFOV = Mathf.Lerp(currentFOV, targetFOV, Time.deltaTime * 5f);
                        thirdPersonCam.fieldOfView = currentFOV;
                    }
                }
            }
        }
    }
}