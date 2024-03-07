using System;
using System.Collections.Generic;
using System.Diagnostics;
using Core.Controllers;
using EvaArchitecture.Core;
using EvaArchitecture.EvaHelpers;
using EvaArchitecture.EventHelpers;
using EvaArchitecture.Logger;
using Game._Events;
using Services._Bases.Interfaces;
using Services.AudioServices;
using Services.AudioServices._Bases;
using Services.InputServices;
using UnityEngine;

namespace Game.Controllers
{
    public class GameController : CoreGameController<GameController>
    {
        private const string APP_VERSION = "0.1.5.0";
        private const string HOW_TO_USE_CONTROLS_HINT = "Controls: Space==Jump, Ctrl==Fire, WSDA==Move/Rotate";
        
        protected readonly string _classLogMessage = Log.LogColorize(() => "GameController", Color.green);
        protected readonly string _startedLogMessage = Log.LogColorize(() => "Started", Color.cyan);

        public override void Subscribe(bool mustSubscribe)
        {
            base.Subscribe(mustSubscribe);
            Eva.GetEvent<GameEventBattleEnemyDeath>().Subscribe(mustSubscribe, OnEvent);
        }
        
        #region Serializable fields
        
        [SerializeField] private Camera _camera;

        #endregion

        #region private fields

        private int _score;
        private int _killsSequentialCount;
        private List<IService> _services;

        #endregion

        #region Properties
        
        public List<IService> Services => _services;
        public Camera Camera => _camera;

        #endregion

        #region unity methods

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        protected override void Start()
        {
            base.Start();
            StartNewGame();
            StartCoroutine(WaitAndExec(1f, PublishEventAppStarted));
        }

        private void PublishEventAppStarted()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var results = Eva.GetEvent<GameEventAppStarted>().Publish((DateTime.Now, $"version={APP_VERSION}, {HOW_TO_USE_CONTROLS_HINT}"));

            stopwatch.Stop();
            Log.Info(() => $"{_classLogMessage} {_startedLogMessage}"
                           + $", elapsedMilliseconds={stopwatch.ElapsedMilliseconds}"
                           + $", results={results.AsStringDelimited()}");
        }

        #endregion
        
        #region public methods

        public void ScoreAdd(int addValue)
        {
            _score += addValue;
            Eva.GetEvent<GameEventBattleScoreChanged>().Publish($"Score {_score}");
        }
        
        #endregion
        
        #region private methods
        
        private void Init()
        {
            _services = new List<IService>
            {
                new StandardInputService(),
                new AudioService()
            };
        }

        private void StartNewGame()
        {
            _killsSequentialCount = 0;
        }
        
        private void OnEvent(GameEventBattleEnemyDeath ev, object model, List<object> results)
        {
            IncKillsCount();
        }

        private void IncKillsCount()
        {
            var index = _killsSequentialCount;
            
            var texts = _gameConfig.KillsSequentialTexts;
            var audioClips = _gameConfig.KillsSequentialAudioClips;
            if (index >= texts.Length 
                || index >= audioClips.Length)
            {
                _killsSequentialCount = 0;
                index = 0;
            }

            var text = 0 == texts.Length || index >= texts.Length ? null : texts[index];
            var clip = 0 == audioClips.Length || index >= audioClips.Length ? null : audioClips[index];
            
            if (!text.IsNullOrEmpty() 
                && !clip.IsNull())
                ShowKillsCount(texts[index].ToUpper(), clip);
            
            _killsSequentialCount++;
        }

        private void ShowKillsCount(string text, AudioClip clip)
        {
            if (!text.IsNullOrEmpty())
                Eva.GetEvent<GameEventKillInfo>().Publish(text);
            
            if (!clip.IsNull())
                Eva.GetEvent<GameEventAudioPlay>().Publish((AudioKind.Voice, clip, 1f));
        }

        #endregion
    }
}
