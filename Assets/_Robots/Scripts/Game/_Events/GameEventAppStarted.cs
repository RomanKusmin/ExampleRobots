using System;
using _Constants;
using EvaArchitecture._Bases;
using UnityEngine;

namespace Game._Events
{
    [CreateAssetMenu(fileName = nameof(GameEventAppStarted)
        , menuName = Menu.Events + nameof(GameEventAppStarted), 
        order = Menu.EventsOrder)]
    public class GameEventAppStarted : EvaEvent<GameEventAppStarted, (DateTime, string)>
    {
    }
}
