using System;
using _Constants;
using Configs.LocationConfigs._Bases;
using UnityEngine;

namespace Configs.LocationConfigs.MultiPlayerLocationConfigs
{
    [CreateAssetMenu(fileName = nameof(PvEMultiPlayerLocationConfig), 
        menuName = Menu.Location + nameof(PvEMultiPlayerLocationConfig), 
        order = Menu.LocationOrder)]
    [Serializable]
    public class PvEMultiPlayerLocationConfig : BaseMultiPlayerLocationConfig
    {
    }
}
