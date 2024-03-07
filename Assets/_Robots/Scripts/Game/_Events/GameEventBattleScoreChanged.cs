using _Constants;
using EvaArchitecture._Bases;
using UnityEngine;

namespace Game._Events
{
    [CreateAssetMenu(fileName = nameof(GameEventBattleScoreChanged)
        , menuName = Menu.Events + nameof(GameEventBattleScoreChanged), 
        order = Menu.EventsOrder)]
    public class GameEventBattleScoreChanged : EvaEvent<GameEventBattleScoreChanged>
    {
    }
}
