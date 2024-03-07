using _Constants;
using Configs.CharacterConfigs._Bases;
using UnityEngine;

namespace Configs.CharacterConfigs.PlayerConfigs
{
    [CreateAssetMenu(fileName = nameof(PlayerStateConfig), 
        menuName = Menu.FiniteStateMachine + nameof(PlayerStateConfig), 
        order = Menu.FiniteStateMachineOrder)]
    public class PlayerStateConfig : BaseCharacterStateConfig
    {
    }
}
