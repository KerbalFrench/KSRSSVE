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
            GameEvents.onVesselSituationChange.Add(OnVesselSituationChanged);
            if (FlightGlobals.ActiveVessel.situation == Vessel.Situations.ORBITING && FlightGlobals.ActiveVessel.mainBody.isHomeWorld)
                ApplyEVECityLights();
        }

        public void OnDestroy()
        {
            GameEvents.onVesselSituationChange.Remove(OnVesselSituationChanged);
        }

        private void ApplyEVECityLights()
        {
            GlobalEVEManager manager = FindObjectOfType<GlobalEVEManager>();
            List<EVEManagerBase> managers = (List < EVEManagerBase > )manager.GetType().GetFields(BindingFlags.Static | BindingFlags.NonPublic).First(f => f.Name == "Managers").GetValue(manager);
            EVEManagerBase cityLightsManager = managers.First(m => m.configName == "EVE_CITY_LIGHTS");
            cityLightsManager.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public).First(m => m.Name == "Apply").Invoke(cityLightsManager, null);
        }

        private void OnVesselSituationChanged(GameEvents.HostedFromToAction<Vessel, Vessel.Situations> data)
        {
            Vessel curVessel = data.host;
            if (!curVessel.mainBody.isHomeWorld || !curVessel.isActiveVessel)
                return;
            if (data.from == Vessel.Situations.FLYING && data.to == Vessel.Situations.SUB_ORBITAL)
            {
                ApplyEVECityLights();
            }
            
        }
    }
}
