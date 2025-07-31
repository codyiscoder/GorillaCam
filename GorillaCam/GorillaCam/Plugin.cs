using BepInEx;
using GorillaCam.Behaviours;

namespace GorillaCam
{
    [BepInPlugin(Constants.GUID, Constants.NAME, Constants.VERS)]
    public class Plugin : BaseUnityPlugin
    {
        private void Start()
        {
            gameObject.AddComponent<Core>();
            Tools.Logger.LogSource = Logger;
        }
    }

    internal class Constants
    {
        public const string
            GUID = "net.tc.gorillacam",
            NAME = "GorillaCam",
            VERS = "1.0.0";
    }
}