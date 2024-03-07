using EvaArchitecture._Bases;
using EvaArchitecture._Demo.Scripts._Constants;
using UnityEngine;

namespace EvaArchitecture._Demo.Game._Events
{
    [CreateAssetMenu(fileName = nameof(EventPlayerReceivedDamage), 
        menuName = DemoGameMenu.Events + nameof(EventPlayerReceivedDamage), 
        order = DemoGameMenu.EventsOrder)]
    public class EventPlayerReceivedDamage : EvaEvent<EventPlayerReceivedDamage>
    {
    }
}
