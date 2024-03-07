using _Constants;
using EvaArchitecture._Bases;
using UnityEngine;

namespace Game._Events
{
    [CreateAssetMenu(fileName = nameof(GameEventPlayerReceivedDamage), 
        menuName = Menu.Events + nameof(GameEventPlayerReceivedDamage), 
        order = Menu.EventsOrder)]
    public class GameEventPlayerReceivedDamage : EvaEvent<GameEventPlayerReceivedDamage>
    {
    }
}
