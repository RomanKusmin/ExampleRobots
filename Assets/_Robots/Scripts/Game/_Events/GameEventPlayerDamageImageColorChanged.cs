using _Constants;
using EvaArchitecture._Bases;
using UnityEngine;

namespace Game._Events
{
    [CreateAssetMenu(fileName = nameof(GameEventPlayerDamageImageColorChanged)
        , menuName = Menu.Events + nameof(GameEventPlayerDamageImageColorChanged), 
        order = Menu.EventsOrder)]
    public class GameEventPlayerDamageImageColorChanged : EvaEvent<GameEventPlayerDamageImageColorChanged>
    {
    }
}
