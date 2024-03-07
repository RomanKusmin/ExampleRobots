using _Constants;
using Core.Configs;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = nameof(GameConfig), 
        menuName = Menu.Game + nameof(GameConfig), 
        order = Menu.GameOrder)]
    public class GameConfig : CoreGameConfig
    {
        [SerializeField] private string[] _killsSequentialTexts;
        [SerializeField] private AudioClip[] _killsSequentialAudioClips;

        public string[] KillsSequentialTexts => _killsSequentialTexts;
        public AudioClip[] KillsSequentialAudioClips => _killsSequentialAudioClips;
    }
}
