using System;
using EvaArchitecture.Controllers._Bases;
using Services.AudioServices._Bases.Interfaces;
using Services.AudioServices.Configs;
using UnityEngine;

namespace Services.AudioServices._Bases
{
    [Serializable]
    public abstract class BaseAudioService<T> : SingletonMonoBehaviour<T>, IAudioService
        where T : BaseAudioService<T>
    {
        #region abstract 
        
        public abstract string ServiceName { get; }
        public abstract bool Init();
        public abstract bool DoDestroy();
        
        #endregion
        
        #region Serialiazed fields
        
        [SerializeField] protected AudioServiceConfig _config;
        
        #endregion
        
        #region Properties
        
        public AudioServiceConfig Config => _config;
        
        #endregion
    }
}
