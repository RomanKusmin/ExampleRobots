using System;
using _Constants;
using Configs.CharacterConfigs.PlayerConfigs;
using Configs.UiConfigs._Bases;
using UnityEngine;

namespace Configs.UiConfigs.UiWindowsConfigs
{
    [CreateAssetMenu(fileName = nameof(PlayerUiWindowConfig), 
        menuName = Menu.Ui + nameof(PlayerUiWindowConfig), 
        order = Menu.UiOrder)]
    [Serializable]
    public class PlayerUiWindowConfig : BaseUiWindowConfig<PlayerStateConfig>
    {        
    }
}
