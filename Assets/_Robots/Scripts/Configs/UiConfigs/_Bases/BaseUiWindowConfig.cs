using System;
using EvaArchitecture._Services.FiniteStateMachineServices.Fsm._Configs._Bases;
using UnityEngine;

namespace Configs.UiConfigs._Bases
{
    [Serializable]
    public abstract class BaseUiWindowConfig<TStateConfig> : BaseUiConfig
        where TStateConfig : BaseStateConfig
    { 
        [SerializeField] private TStateConfig _stateConfig;

        public TStateConfig StateConfig => _stateConfig;
    }
}
