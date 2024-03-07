using _Constants;
using EvaArchitecture._Bases;
using UnityEngine;

namespace Game._Events
{
    [CreateAssetMenu(fileName = nameof(GameEventPlayerSightColorChanged), 
        menuName = Menu.Events + nameof(GameEventPlayerSightColorChanged), 
        order = Menu.EventsOrder)]
    public class GameEventPlayerSightColorChanged : EvaEvent<GameEventPlayerSightColorChanged>
    {
    }
}
