using EvaArchitecture._Bases;
using EvaArchitecture._Demo.Scripts._Constants;
using UnityEngine;

namespace EvaArchitecture._Demo.Game._Events
{
    [CreateAssetMenu(fileName = nameof(EventPlayerCreateCoroutine)
        , menuName = DemoGameMenu.Events + nameof(EventPlayerCreateCoroutine), 
        order = DemoGameMenu.EventsOrder)]
    public class EventPlayerCreateCoroutine : EvaEvent<EventPlayerCreateCoroutine>
    {
    }
}
