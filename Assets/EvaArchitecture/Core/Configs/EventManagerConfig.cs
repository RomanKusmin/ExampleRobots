using System.Collections.Generic;
using EvaArchitecture._Bases;
using EvaArchitecture._Constants;
using EvaArchitecture.EvaHelpers;
using UnityEngine;

namespace EvaArchitecture.Core.Configs
{
    [CreateAssetMenu(fileName = nameof(EventManagerConfig), menuName = MenuEva.Configs + nameof(EventManagerConfig))]
    public class EventManagerConfig : ScriptableObject
    {
        [SerializeField] private List<EvaEvent> _eventConfigs;

        public T GetEvent<T>()
            where T : EvaEvent
        {
            var configs = _eventConfigs;
            
            if (configs.IsNull())
                return null;

            foreach (var eventConfig in configs)
            {
                if (eventConfig.IsNull())
                    continue;
                
                if (!(eventConfig is T result))
                    continue;

                return result;
            }

            return null;
        }
        
        public void SetEventConfigs(List<EvaEvent> eventConfigs)
        {
            _eventConfigs = eventConfigs;
        }
    }
}
