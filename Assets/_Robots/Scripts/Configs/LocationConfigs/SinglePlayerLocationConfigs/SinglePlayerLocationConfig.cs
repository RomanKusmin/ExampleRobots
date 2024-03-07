using System;
using _Constants;
using Configs.LocationConfigs._Bases;
using UnityEngine;

namespace Configs.LocationConfigs.SinglePlayerLocationConfigs
{
    [CreateAssetMenu(fileName = nameof(SinglePlayerLocationConfig), 
        menuName = Menu.Location + nameof(SinglePlayerLocationConfig), 
        order = Menu.LocationOrder)]
    [Serializable]
    public class SinglePlayerLocationConfig : BaseSinglePlayerLocationConfig
    {
    }
}
