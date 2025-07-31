using Cinemachine;
using UnityEngine;
using GorillaCam.Behaviours.CameraStuff.ModeScripts;
using GorillaCam.Tools;

namespace GorillaCam.Behaviours
{
    enum cameraMode
    {
        DYNAMIC_FPV = 0,
        FPV = 1,
        TPC = 2,
        FOLLOW = 3,
    }

    internal class Core : MonoBehaviour
    {
        public static cameraMode currentMode = cameraMode.DYNAMIC_FPV;
        private static cameraMode? lastLoadedMode = null;
        private static GameObject modeObject;
        public CinemachineBrain cBrain;

        public void Update()
        {
            if (cBrain == null) cBrain = GorillaTagger.Instance.thirdPersonCamera.GetComponentInChildren<CinemachineBrain>(); cBrain.enabled = false;
        }

        public static int GetCameraMode(cameraMode mode)
        {
            switch (mode)
            {
                case cameraMode.DYNAMIC_FPV: return 0;
                case cameraMode.FPV: return 1;
                case cameraMode.TPC: return 2;
                case cameraMode.FOLLOW: return 3;
                default:
                    Tools.Logger.Error("'GetCameraMode()' returned -1.");
                    return -1;
            }
        }

        public static void LoadScriptBasedOnMode()
        {
            if (!PlayerSpawned.IsPlayerSpawned()) return;

            if (lastLoadedMode == currentMode) return;

            UnloadScriptBasedOnMode();

            modeObject = new GameObject("CameraModeObject");
            switch (currentMode)
            {
                case cameraMode.DYNAMIC_FPV:
                    modeObject.AddComponent<DynamicFPV>();
                    break;
                case cameraMode.FPV:
                    modeObject.AddComponent<FPV>();
                    break;
                case cameraMode.TPC:
                    modeObject.AddComponent<TPC>();
                    break;
                case cameraMode.FOLLOW:
                    modeObject.AddComponent<Follow>();
                    break;
                default:
                    Tools.Logger.Error("Invalid camera mode specified.");
                    Destroy(modeObject);
                    modeObject = null;
                    break;
            }

            lastLoadedMode = currentMode;
        }

        public static void UnloadScriptBasedOnMode()
        {
            if (modeObject != null)
            {
                Destroy(modeObject);
                modeObject = null;
            }
        }

        public void SetCameraMode(cameraMode mode)
        {
            if (mode == currentMode) return;

            currentMode = mode;
            LoadScriptBasedOnMode();
        }
    }
}