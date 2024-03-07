using _Constants;
using Configs.CharacterConfigs._Bases;
using UnityEngine;

namespace Configs.CharacterConfigs.BotConfigs
{
    [CreateAssetMenu(fileName = nameof(BotConfig), 
        menuName = Menu.Character + nameof(BotConfig), 
        order = Menu.CharacterOrder)]
    public class BotConfig : BaseCharacterConfig
    {
    }
}
