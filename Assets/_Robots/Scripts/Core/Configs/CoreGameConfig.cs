using System.Collections.Generic;
using _Constants;
using Configs.LocationConfigs._Bases;
using Configs.UiConfigs;
using EvaArchitecture.Configs._Bases;
using UnityEngine;

namespace Core.Configs
{
    [CreateAssetMenu(fileName = nameof(CoreGameConfig), 
        menuName = Menu.Game + nameof(CoreGameConfig), 
        order = Menu.GameOrder)]
    public class CoreGameConfig : BaseConfig
    {
        [SerializeField] private BaseLocationConfig _initialLocationConfig;
        [SerializeField] private UiControllerConfig _uiControllerConfig;
        [SerializeField] private List<GameObject> _services;

        public BaseLocationConfig InitialLocationConfig => _initialLocationConfig;
        public UiControllerConfig UiControllerConfig => _uiControllerConfig;
        public List<GameObject> Services => _services;
    }
}
