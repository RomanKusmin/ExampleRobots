using _Constants;
using EvaArchitecture.Configs._Bases;
using Services.AudioServices._Bases;
using UnityEngine;

namespace Services.AudioServices.Configs.Items
{
    [CreateAssetMenu(fileName = nameof(AudioSourceConfig)
        , menuName = Menu.AudioService + nameof(AudioSourceConfig), 
        order = Menu.AudioServiceOrder)]
    public class AudioSourceConfig : BaseConfig
    {
        [SerializeField] private AudioKind _audioKind;
        [SerializeField] private float _initialVolume;

        public AudioKind AudioKind => _audioKind;
        public float InitialVolume => _initialVolume;
    }
}
