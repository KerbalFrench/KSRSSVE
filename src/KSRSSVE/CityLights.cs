using System;
using UnityEngine;
using Utils;
using CityLights;
using System.Reflection;
using EVEManager;
using System.Collections.Generic;
using System.Linq;

namespace KSRSSVE
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class CityLights : MonoBehaviour
    {
        //CityLightsObject cityLights;
        //MaterialPQS macro;
        public void Start()
        {
            GlobalEVEManager manager = FindObjectOfType<GlobalEVEManager>();
            List<EVEManagerBase> managers = (List < EVEManagerBase > )manager.GetType().GetFields(BindingFlags.Static | BindingFlags.NonPublic).First(f => f.Name == "Managers").GetValue(manager);
            EVEManagerBase cityLightsManager = managers.First(m => m.configName == "EVE_CITY_LIGHTS");
            cityLightsManager.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public).First(m => m.Name == "Apply").Invoke(cityLightsManager, null);
        }
    }
}
