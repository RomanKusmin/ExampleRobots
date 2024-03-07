using System.Collections.Generic;
using Core.Helpers;
using EvaArchitecture.Core;
using EvaArchitecture.EvaHelpers;
using EvaArchitecture.EventHelpers;
using EvaArchitecture.Logger;
using Game._Events;
using Services.AudioServices._Bases;
using Services.AudioServices.Configs.Items;
using UnityEngine;

namespace Services.AudioServices
{
    public class Standard3dAudioService : BaseAudioService<Standard3dAudioService>
    {
        public override void Subscribe(bool mustSubscribe)
        {
            base.Subscribe(mustSubscribe);
            Eva.GetEvent<GameEventAudioPlay>().Subscribe(mustSubscribe, OnEvent);
        }

        #region Serialized fields

        #endregion
        
        #region private fields

        private bool _isInitDone;
        private Dictionary<AudioSourceConfig, AudioSource> _audioSources = new Dictionary<AudioSourceConfig, AudioSource>();
        
        #endregion
        
        #region Properties
     
        public override string ServiceName => "Standard3dAudioService";
        
        #endregion
        
        #region public methods

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        public override bool Init() => InternalInit();
        public override bool DoDestroy() => true;
        
        #endregion
        
        #region private methods

        private bool InternalInit()
        {
            if (_isInitDone)
                return true;

            if (Config.IsNull())
            {
                Log.Error(() => $"Standard3dAudioService Config IsNull");
                return false;
            }

            var audioSourceConfigs = Config.AudioSourceConfigs;
            if (audioSourceConfigs.IsNullOrEmpty())
            {
                Log.Error(() => $"Standard3dAudioService Config.AudioSourceConfigs IsNullOrEmpty");
                return false;
            }   

            foreach (var audioSourceConfig in audioSourceConfigs)
            {
                if (audioSourceConfig.IsNull())
                    continue;

                if (!CreateAudioSource(audioSourceConfig))
                    Log.Error(() => $"Standard3dAudioService.CreateAudioSource FAILED");
            }
            
            _isInitDone = true;
            return true;
        }

        private bool CreateAudioSource(AudioSourceConfig audioSourceConfig)
        {
            if (audioSourceConfig.IsNull())
                return false;

            var audioKind = audioSourceConfig.AudioKind;
            var initialVolume = audioSourceConfig.InitialVolume;
            
            var prefab = audioSourceConfig.Prefab;
            if (prefab.IsNull())
            {
                Log.Error(() => $"CreateAudioSource audioSourceConfig.Prefab IsNull");
                return false;
            }

            var audioSourceGameObject = this.transform.CreateFromPrefab(prefab);
            if (audioSourceGameObject.IsNull())
                return false;

            var audioSource = audioSourceGameObject.GetComponent<AudioSource>();
            if (audioSource.IsNull())
                return false;

            audioSourceGameObject.name = $"AudioSource_{audioKind.ToString()}";
            audioSource.volume = initialVolume;
            
            _audioSources.Add(audioSourceConfig, audioSource);

            return true;
        }

        private AudioSource GetAudioSource(AudioKind audioKind)
        {
            if (_audioSources.IsNull())
                return null;

            foreach (var foundAudioSource in _audioSources)
            {
                if (foundAudioSource.IsNull())
                    continue;

                var foundAudioSourceConfig = foundAudioSource.Key;
                if (foundAudioSourceConfig.IsNull())
                    continue;

                var foundAudioKind = foundAudioSourceConfig.AudioKind;
                if (foundAudioKind != audioKind)
                    continue;

                var result = foundAudioSource.Value;
                return result;
            }

            return null;
        }

        private void OnEvent(GameEventAudioPlay ev, (AudioKind, AudioClip, float) model, List<object> results)
        {
            if (model.IsNull())
                return;

            var (audioKind, audioClip, volume) = model;

            var audioSource = GetAudioSource(audioKind);
            if (audioSource.IsNull())
            {
                Log.Error(() => $"OnEvent GameEventAudioPlay, audioSource.IsNull(), audioKind={audioKind}");
                return;
            }

            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.Play();
        }
        
        #endregion
    }
}
