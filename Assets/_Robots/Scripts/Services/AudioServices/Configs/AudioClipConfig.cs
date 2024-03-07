using _Constants;
using EvaArchitecture.Configs._Bases;
using Services.AudioServices._Bases;
using UnityEngine;

namespace Services.AudioServices.Configs
{
    [CreateAssetMenu(fileName = nameof(AudioClipConfig)
        , menuName = Menu.AudioService + nameof(AudioClipConfig), 
        order = Menu.AudioServiceOrder)]
    public class AudioClipConfig : BaseConfig
    {
        [SerializeField] private AudioKind _audioKind;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private float _volume;

        public AudioKind AudioKind => _audioKind;
        public AudioClip AudioClip => _audioClip;
        public float Volume => _volume;
    }
}
