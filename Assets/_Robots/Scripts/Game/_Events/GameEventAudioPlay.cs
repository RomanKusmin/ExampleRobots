using _Constants;
using EvaArchitecture._Bases;
using Services.AudioServices._Bases;
using UnityEngine;

namespace Game._Events
{
    [CreateAssetMenu(fileName = nameof(GameEventAudioPlay)
        , menuName = Menu.Events + nameof(GameEventAudioPlay), 
        order = Menu.EventsOrder)]
    public class GameEventAudioPlay : EvaEvent<GameEventAudioPlay, (AudioKind, AudioClip, float)>
    {
    }
}
