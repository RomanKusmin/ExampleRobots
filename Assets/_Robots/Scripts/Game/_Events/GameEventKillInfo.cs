using _Constants;
using EvaArchitecture._Bases;
using UnityEngine;

namespace Game._Events
{
    [CreateAssetMenu(fileName = nameof(GameEventKillInfo)
        , menuName = Menu.Events + nameof(GameEventKillInfo), 
        order = Menu.EventsOrder)]
    public class GameEventKillInfo : EvaEvent<GameEventKillInfo>
    {
    }
}
