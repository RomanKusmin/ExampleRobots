using _Constants;
using EvaArchitecture._Bases;
using UnityEngine;

namespace Game._Events
{
    [CreateAssetMenu(fileName = nameof(GameEventBattleEnemyDeath)
        , menuName = Menu.Events + nameof(GameEventBattleEnemyDeath), 
        order = Menu.EventsOrder)]
    public class GameEventBattleEnemyDeath : EvaEvent<GameEventBattleEnemyDeath>
    {
    }
}
