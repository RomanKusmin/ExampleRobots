using System;
using EvaArchitecture._Bases;
using EvaArchitecture._Demo.Scripts._Constants;
using UnityEngine;

namespace EvaArchitecture._Demo.Game._Events
{
    [CreateAssetMenu(fileName = nameof(EventAppStarted)
        , menuName = DemoGameMenu.Events + nameof(EventAppStarted), 
        order = DemoGameMenu.EventsOrder)]
    public class EventAppStarted : EvaEvent<EventAppStarted, (DateTime, bool)>
    {        
    }
}
