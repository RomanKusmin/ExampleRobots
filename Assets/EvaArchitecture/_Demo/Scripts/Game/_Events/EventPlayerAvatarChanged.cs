using EvaArchitecture._Bases;
using EvaArchitecture._Demo.Scripts._Constants;
using UnityEngine;

namespace EvaArchitecture._Demo.Game._Events
{
    [CreateAssetMenu(fileName = nameof(EventPlayerAvatarChanged)
        , menuName = DemoGameMenu.Events + nameof(EventPlayerAvatarChanged), 
        order = DemoGameMenu.EventsOrder)]
    public class EventPlayerAvatarChanged : EvaEvent<EventPlayerAvatarChanged>
    {
    }
}
