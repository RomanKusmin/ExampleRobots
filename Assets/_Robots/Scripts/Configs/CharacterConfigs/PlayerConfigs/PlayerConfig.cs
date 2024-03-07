using _Constants;
using Configs.CharacterConfigs._Bases;
using UnityEngine;

namespace Configs.CharacterConfigs.PlayerConfigs
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), 
        menuName = Menu.Character + nameof(PlayerConfig), 
        order = Menu.CharacterOrder)]
    public class PlayerConfig : BaseCharacterConfig
    {
    }
}
