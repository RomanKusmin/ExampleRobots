using System.Collections.Generic;
using System.Linq;
using Configs.CharacterConfigs.PlayerConfigs;
using Configs.UiConfigs.UiWindowsConfigs;
using Core.Controllers.UiControllers;
using Core.Helpers;
using EvaArchitecture.EvaHelpers;
using EvaArchitecture.Logger;
using Game.Controllers.PlayerControllers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Controllers.UiControllers
{
    public class UiController : CoreUiController<UiController>
    {
        #region Serialized fields
        
        [SerializeField] private Transform _uiWindowsParent;

        #endregion
        
        #region private fields
        
        private readonly Dictionary<GameObject, GameObject> _uiWindows = new Dictionary<GameObject, GameObject>();
        
        #endregion
        
        #region Properties
        
        public Transform UiWindowsParent => _uiWindowsParent;
        #endregion
        
        #region Unity methods
        
        #endregion
        
        protected override void OnEnable()
        {
            base.OnEnable();

            if (!ShowUiWindow())
            {
                Log.Error(() => $"UiController.ShowUiWindow FAILED");
                return;
            }
        }

        private bool ShowUiWindow()
        {
            var prefabPlayerUiWindow = GetPrefabPlayerUiWindow();
            if (prefabPlayerUiWindow.IsNull())
                return (bool)Log.Error(() => $"ShowUiWindow, prefabPlayerUiWindow is null");
            
            if (!_uiWindows.TryGetValue(prefabPlayerUiWindow, out var existingUiWindow))
            {
                var uiPlayerWindow = _uiWindowsParent.CreateUiFromPrefab(prefabPlayerUiWindow);
                _uiWindows.Add(prefabPlayerUiWindow, uiPlayerWindow);
            }

            return true;
        }

        private List<PlayerUiWindowConfig> GetPlayerUiWindowConfigs()
        {
            var gameController = GameController.Instance;
            if (gameController.IsNull())
                return null;
            
            var locationConfig = gameController.LocationConfig;
            if (locationConfig.IsNull())
                return null;

            var playerUiWindowConfigs = locationConfig.PlayerUiWindowConfigs;
            return playerUiWindowConfigs;
        }

        private PlayerStateConfig GetCurrentPlayerStateConfig()
        {
            var gameController = GameController.Instance;
            if (gameController.IsNull())
                return null;
            
            var playerModel = gameController.PlayerModel;
            if (playerModel.IsNull())
                return null;
            
            var currentPlayerStateModel = playerModel.StateModel;
            if (currentPlayerStateModel.IsNull())
            {
                Log.Error(() => $"ShowUiWindow, currentPlayerStateModel is null");
                return null;
            }

            var currentPlayerStateConfig = currentPlayerStateModel.Config;
            return currentPlayerStateConfig;
        }

        private GameObject GetPrefabPlayerUiWindow()
        {
            var currentPlayerStateConfig = GetCurrentPlayerStateConfig();
            if (currentPlayerStateConfig.IsNull())
                return (GameObject)Log.Error(() => $"ShowUiWindow, currentPlayerStateConfig is null");

            var playerUiWindowConfigs = GetPlayerUiWindowConfigs();
            if (playerUiWindowConfigs.IsNullOrEmpty())
                return (GameObject)Log.Error(() => $"ShowUiWindow, playerUiWindowConfigs is null");

            var playerUiWindowConfig = playerUiWindowConfigs.FirstOrDefault(it 
                => !it.IsNull() && !it.StateConfig.IsNull() && it.StateConfig == currentPlayerStateConfig);
            
            if (playerUiWindowConfig.IsNull())
                return (GameObject)Log.Error(() => $"ShowUiWindow, playerUiWindowConfig not found for currentPlayerStateConfig={currentPlayerStateConfig}");
            
            var prefabPlayerUiWindow = playerUiWindowConfig.Prefab;
            return prefabPlayerUiWindow;
        }
    }
}
