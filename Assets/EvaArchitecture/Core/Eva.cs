using System.Collections.Generic;
using EvaArchitecture._Bases;
using EvaArchitecture.Configs;
using EvaArchitecture.Logger;
using UnityEngine;

namespace EvaArchitecture.Core
{
    [ExecuteInEditMode]
    public class Eva : MonoBehaviour
    {
        [SerializeField] private EventManagerConfig _eventManagerConfig;

        private static Eva _instance;
        
        public static Eva Instance => _instance;
        public EventManagerConfig EventManagerConfig => _eventManagerConfig;

        private void Awake()
        {
            _instance = this;
            if (_eventManagerConfig == null)
                Log.Error(() => $"Eva, Awake, _eventManagerConfig is null. Create and assign asset using menu = EvaArchitecture/Configs/EventManagerConfig");
        }

        private void OnEnable()
        {
            _instance = this;
        }

        public void SetEventConfigs(List<EvaEvent> eventConfigs)
        {
            _eventManagerConfig.SetEventConfigs(eventConfigs);
        }

        public static T GetEvent<T>()
            where T : EvaEvent
        {
            var eventManager = Eva.Instance;
            if (eventManager == null)
                return null;

            var eventManagerConfig = eventManager.EventManagerConfig;

            if (eventManagerConfig == null)
            {
                Log.Error(() => $"EventManagerConfig is not assigned");
                return null;
            }

            var result = eventManagerConfig.GetEvent<T>();
            if (result == null)
            {
                Log.Error(() => $"Event <Type>={typeof(T)} not found in EventManagerConfig. Please select the component Eva and press button SetEventConfigs");
            }

            return result;
        }
    }
}
