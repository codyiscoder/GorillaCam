using System;
using UnityEngine;

namespace GorillaCam.Tools
{
    internal class PlayerSpawned : MonoBehaviour
    {
        public static bool IsPlayerSpawned()
        {
            if (GorillaTagger.Instance == null)
                return false;

            if (GorillaLocomotion.GTPlayer.Instance == null)
                return false;

            return true;
        }
    }
}