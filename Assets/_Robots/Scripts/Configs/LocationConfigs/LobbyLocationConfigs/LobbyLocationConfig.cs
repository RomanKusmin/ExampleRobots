using System;
using _Constants;
using Configs.LocationConfigs._Bases;
using UnityEngine;

namespace Configs.LocationConfigs.LobbyLocationConfigs
{
    [CreateAssetMenu(fileName = nameof(LobbyLocationConfig), 
        menuName = Menu.Location + nameof(LobbyLocationConfig), 
        order = Menu.LocationOrder)]
    [Serializable]
    public class LobbyLocationConfig : BaseLobbyLocationConfig
    {
    }
}
