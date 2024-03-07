using System;
using _Constants;
using Configs.LocationConfigs._Bases;
using UnityEngine;

namespace Configs.LocationConfigs.MultiPlayerLocationConfigs
{
    [CreateAssetMenu(fileName = nameof(RealTimeMultiPlayerLocationConfig), 
        menuName = Menu.Location + nameof(RealTimeMultiPlayerLocationConfig), 
        order = Menu.LocationOrder)]
    [Serializable]
    public class RealTimeMultiPlayerLocationConfig : BaseMultiPlayerLocationConfig
    {
    }
}
