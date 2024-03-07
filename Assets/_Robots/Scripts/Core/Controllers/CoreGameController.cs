using System.Linq;
using Configs.CharacterConfigs.PlayerConfigs;
using Configs.LocationConfigs._Bases;
using Configs.UiConfigs;
using Core.Controllers._Bases.Interfaces;
using Core.Helpers;
using EvaArchitecture.Controllers._Bases;
using EvaArchitecture.Core;
using EvaArchitecture.EvaHelpers;
using EvaArchitecture.EventHelpers;
using EvaArchitecture.Logger;
using Game._Events;
using Game.Configs;
using Game.Controllers.PlayerControllers;
using Models.CharacterModels.PlayerModels;
using UnityEngine;

namespace Core.Controllers
{
    public abstract class CoreGameController<T> : SingletonMonoBehaviour<T>
        where T : CoreGameController<T>
    {
        #region Serialized fields
        
        [SerializeField] protected GameConfig _gameConfig;
        [SerializeField] protected Transform _locationParent;
        [SerializeField] protected Transform _uiParent;
        [SerializeField] protected Transform _servicesTransform;
        
        #endregion
        
        #region fields

        protected IBaseLocationController _locationController;
        
        #endregion

        #region Properties
        
        public GameConfig GameConfig => _gameConfig;
        protected Transform ServicesTransform => _servicesTransform;
        public IBaseLocationController LocationController => _locationController;
        public PlayerModel PlayerModel => !LocationController.IsNull() ? LocationController.PlayerModel : null;
        public BaseLocationConfig LocationConfig => !LocationController.IsNull() ? LocationController.LocationConfig : null;
        public PlayerConfig PlayerConfig => !LocationConfig.IsNull() ? LocationConfig.PlayerConfig : null;

        #endregion
        
        #region unity events

        protected override void Start()
        {
            base.Start();

            if (!InternalStart())
            {
                Log.Error(() => $"InternalStart FAILED");
                return;
            }
        }
        
        #endregion
        
        #region virtual 

        protected virtual bool InternalStart()
        {
            if (_gameConfig.IsNull())
                return (bool)Log.Error(() => $"_gameConfig is null");
            
            if (!CreateServices())
                return (bool)Log.Error(() => $"CreateServices FAILED");

            var locationConfig = _gameConfig.InitialLocationConfig;
            if (locationConfig.IsNull())
                return (bool)Log.Error(() => $"locationConfig is null");

            if (!CreateAndInitLocation(_locationParent, locationConfig))
                return (bool)Log.Error(() => $"CreateAndInitLocation FAILED");

            var uiControllerConfig = _gameConfig.UiControllerConfig;
            if (uiControllerConfig.IsNull())
                return (bool)Log.Error(() => $"uiControllerConfig is null");
            
            if (!CreateUi(_uiParent, uiControllerConfig))
                return (bool)Log.Error(() => $"CreateUi FAILED");
            
            if (!UpdateUi())
                return (bool)Log.Error(() => $"UpdateUi FAILED");

            var backgroundAudioClipConfigs = locationConfig.BackgroundAudioClipConfigs.ToList();
            if (!backgroundAudioClipConfigs.IsNullOrEmpty())
            {
                var backgroundAudioClipConfig = backgroundAudioClipConfigs[0];
                if (!backgroundAudioClipConfig.IsNull()
                    && !backgroundAudioClipConfig.AudioClip.IsNull())
                {
                    Eva.GetEvent<GameEventAudioPlay>().Publish((backgroundAudioClipConfig.AudioKind, backgroundAudioClipConfig.AudioClip, backgroundAudioClipConfig.Volume));
                }
            }

            return true;
        }

        private bool UpdateUi()
        {
            if (!PlayerHealth.Instance.IsNull())
                Eva.GetEvent<GameEventPlayerHealthChanged>().Publish((PlayerHealth.Instance.CurrentHealth, PlayerHealth.Instance.MaxHealth));

            return true;
        }

        protected virtual bool CreateServices()
        {
            if (_gameConfig.IsNull())
                return false;

            var servicesPrefabs = _gameConfig.Services;
            if (servicesPrefabs.IsNull())
                return false;

            foreach (var servicePrefab in servicesPrefabs)
            {
                if (servicePrefab.IsNull())
                    continue;

                _servicesTransform.CreateFromPrefab(servicePrefab);
            }

            return true;
        }

        protected virtual bool CreateAndInitLocation(Transform parent, BaseLocationConfig locationConfig)
        {
            var locationGameObject = CreateLocation(parent, locationConfig);
            if (locationGameObject.IsNull())
                return (bool)Log.Error(() => $"CreateLocation FAILED");

            _locationController = locationGameObject.GetComponent<IBaseLocationController>();
            if (_locationController.IsNull())
                return (bool)Log.Error(() => $"_locationController is null");

            if (!_locationController.Init(locationConfig))
                return (bool)Log.Error(() => $"_locationController.Init FAILED");
            
            return true;
        }

        protected virtual GameObject CreateLocation(Transform parent, BaseLocationConfig locationConfig)
        {
            if (locationConfig.IsNull())
                return null;
            
            var prefab = locationConfig.Prefab;
            if (prefab.IsNull())
                return null;

            var result = parent.CreateFromPrefab(prefab);
            return result;
        }
        
        #endregion
        
        #region private methods
        
        private bool CreateUi(Transform parent, UiControllerConfig uiControllerConfig)
        {
            if (uiControllerConfig.IsNull())
                return (bool)Log.Error(() => $"CreateUi, uiControllerConfig is null");
            
            var prefab = uiControllerConfig.Prefab;
            if (prefab.IsNull())
                return (bool)Log.Error(() => $"CreateUi, uiControllerConfig.Prefab is null");

            var uiGameObject = parent.CreateFromPrefab(prefab);
            return true;
        }
        
        #endregion
    }
}
