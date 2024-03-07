using _Constants;
using EvaArchitecture._Bases;
using UnityEngine;

namespace Game._Events
{
    [CreateAssetMenu(fileName = nameof(GameEventPlayerHealthChanged)
        , menuName = Menu.Events + nameof(GameEventPlayerHealthChanged), 
        order = Menu.EventsOrder)]
    public class GameEventPlayerHealthChanged : EvaEvent<GameEventPlayerHealthChanged>
    {
    }
}
