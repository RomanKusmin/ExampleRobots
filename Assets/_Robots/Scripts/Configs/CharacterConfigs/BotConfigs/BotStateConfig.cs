using _Constants;
using Configs.CharacterConfigs._Bases;
using UnityEngine;

namespace Configs.CharacterConfigs.BotConfigs
{
    [CreateAssetMenu(fileName = nameof(BotStateConfig), 
        menuName = Menu.FiniteStateMachine + nameof(BotStateConfig), 
        order = Menu.FiniteStateMachineOrder)]
    public class BotStateConfig : BaseCharacterStateConfig
    {
    }
}
