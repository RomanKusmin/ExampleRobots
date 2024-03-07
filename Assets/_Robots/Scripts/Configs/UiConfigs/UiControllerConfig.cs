using _Constants;
using Configs.UiConfigs._Bases;
using UnityEngine;

namespace Configs.UiConfigs
{
    [CreateAssetMenu(fileName = nameof(UiControllerConfig), 
        menuName = Menu.Ui + nameof(UiControllerConfig), 
        order = Menu.UiOrder)]
    public class UiControllerConfig : BaseUiControllerConfig
    {
    }
}
