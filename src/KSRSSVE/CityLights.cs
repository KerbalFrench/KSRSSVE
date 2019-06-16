using System;
using UnityEngine;
using Utils;
namespace KSRSSVE
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class CityLights : MonoBehaviour
    {
        GameObject cityLights;
        MaterialPQS macro;
        public void Start()
        {
            cityLights = GameObject.Find("EVE City Lights");
            macro = cityLights.GetComponent<MaterialPQS>();
        }

        public void Update()
        {
            if (FlightGlobals.ActiveVessel.altitude < 10000 && macro.modEnabled)
            {
                macro.modEnabled = false;
                macro.RebuildSphere();
            }
            else if (FlightGlobals.ActiveVessel.altitude >= 10000 && !macro.modEnabled)
            {
                macro.modEnabled = true;
                macro.RebuildSphere();
            }
        }
    }
}
