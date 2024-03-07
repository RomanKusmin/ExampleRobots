using Configs.LocationConfigs._Bases;
using Core.Controllers._Bases.Interfaces;
using Core.Helpers;
using EvaArchitecture.Controllers._Bases;
using EvaArchitecture.EvaHelpers;
using EvaArchitecture.Logger;
using Game.Controllers.PlayerControllers;
using Models.CharacterModels.PlayerModels;
using UnityEngine;

namespace Core.Controllers._Bases.LocationControllers
{
    public abstract class BaseLocationController<T> : SingletonMonoBehaviour<T>, IBaseLocationController
        where T : BaseLocationController<T> 
    {
        [SerializeField] protected Transform _playersParent;
        [SerializeField] protected Transform _botsParent;
        
        protected BaseLocationConfig _locationConfig;
        protected GameObject _playerGameObject;
        protected PlayerModel _playerModel;

        public BaseLocationConfig LocationConfig => _locationConfig;

        public PlayerModel PlayerModel => _playerModel;

        public virtual bool Init(BaseLocationConfig locationConfig)
        {
            _locationConfig = locationConfig;
            
            var playerGameObject = CreatePlayer(_playersParent);
            if (playerGameObject.IsNull())
                return (bool)Log.Error(() => $"CreatePlayer FAILED");

            if (!CreateBots(_botsParent))
                return (bool)Log.Error(() => $"CreateBots FAILED");

            return true;
        }
        
        protected virtual GameObject CreatePlayer(Transform parent)
        {
            if (_locationConfig.IsNull())
                return (GameObject) Log.Error(() => $"CreatePlayer, _locationConfig is null");

            var playerConfig = _locationConfig.PlayerConfig;
            if (playerConfig.IsNull())
                return (GameObject) Log.Error(() => $"CreatePlayer, playerConfig is null");
            
            var prefab = playerConfig.Prefab;
            if (prefab.IsNull())
                return (GameObject) Log.Error(() => $"CreatePlayer, playerConfig.Prefab is null");

            var playerGameObject = parent.CreateFromPrefab(prefab);
            playerGameObject.transform.localPosition = new Vector3(-1f, 0f, 0f);

            var playerHealth = PlayerHealth.Instance;
            if (!playerHealth.IsNull())
            {
                playerHealth.MaxHealth = playerConfig.Health;
                playerHealth.CurrentHealth = playerHealth.MaxHealth;
            }

            var initialPlayerStateConfig = _locationConfig.InitialPlayerStateConfig;
            var playerStateModel = new PlayerStateModel(initialPlayerStateConfig);
            _playerModel = new PlayerModel(playerConfig, playerStateModel);
            _playerModel.Subscribe(true);

            return playerGameObject;
        }

        protected virtual bool CreateBots(Transform parent) => true;

        protected virtual GameObject CreateBot(Transform parent)
        {
            if (_locationConfig.IsNull())
                return null;

            var botConfig = _locationConfig.BotConfig;
            if (botConfig.IsNull())
                return null;
                
            var prefab = botConfig.Prefab;
            if (prefab.IsNull())
                return null;

            var result = parent.CreateFromPrefab(prefab);
            return result;
        }
    }
}
