using System;
using System.Collections.Generic;
using System.Linq;
using Configs.CharacterConfigs.BotConfigs;
using Configs.CharacterConfigs.PlayerConfigs;
using Configs.UiConfigs.UiWindowsConfigs;
using EvaArchitecture.Configs._Bases;
using EvaArchitecture.EvaHelpers;
using Services.AudioServices._Bases;
using Services.AudioServices.Configs;
using UnityEngine;

namespace Configs.LocationConfigs._Bases
{
    [Serializable]
    public abstract class BaseLocationConfig : BaseConfig
    {
        [SerializeField] private List<AudioClipConfig> _audioClipConfigs;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerStateConfig _initialPlayerStateConfig;
        [SerializeField] private BotConfig _botConfig;
        [SerializeField] private BotStateConfig _initialBotStateConfig;
        [SerializeField] private int _botsCount;
        [SerializeField] private List<PlayerUiWindowConfig> _playerUiWindowConfigs;

        public List<AudioClipConfig> AudioClipConfigs => _audioClipConfigs;
        public IEnumerable<AudioClipConfig> BackgroundAudioClipConfigs =>
            _audioClipConfigs.IsNull() ? null : _audioClipConfigs.Where(it => !it.IsNull() && it.AudioKind == AudioKind.Background);
        
        public PlayerConfig PlayerConfig => _playerConfig;
        public PlayerStateConfig InitialPlayerStateConfig => _initialPlayerStateConfig;
        public BotConfig BotConfig => _botConfig;
        public BotStateConfig InitialBotStateConfig => _initialBotStateConfig;
        public int BotsCount => _botsCount;
        public List<PlayerUiWindowConfig> PlayerUiWindowConfigs => _playerUiWindowConfigs;
    }
}
