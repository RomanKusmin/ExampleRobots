using System.Collections.Generic;
using _Constants;
using EvaArchitecture.Configs._Bases;
using Services.AudioServices.Configs.Items;
using UnityEngine;

namespace Services.AudioServices.Configs
{
    [CreateAssetMenu(fileName = nameof(AudioServiceConfig)
        , menuName = Menu.AudioService + nameof(AudioServiceConfig), 
        order = Menu.AudioServiceOrder)]
    public class AudioServiceConfig : BaseConfig
    {
        [SerializeField] private List<AudioSourceConfig> _audioSourceConfigs;

        public List<AudioSourceConfig> AudioSourceConfigs => _audioSourceConfigs;
    }
}
