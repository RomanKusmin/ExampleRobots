using EvaArchitecture._Bases;
using EvaArchitecture._Demo.Scripts._Constants;
using UnityEngine;

namespace EvaArchitecture._Demo.Game._Events
{
    [CreateAssetMenu(fileName = nameof(EventPlayerCreateAsync)
        , menuName = DemoGameMenu.Events + nameof(EventPlayerCreateAsync), 
        order = DemoGameMenu.EventsOrder)]
    public class EventPlayerCreateAsync : EvaEvent<EventPlayerCreateAsync>
    {
    }
}
