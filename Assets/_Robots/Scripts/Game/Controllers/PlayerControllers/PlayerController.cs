using System;
using System.Collections.Generic;
using Core.Helpers;
using EvaArchitecture.Controllers._Bases;
using EvaArchitecture.Core;
using EvaArchitecture.EvaHelpers;
using EvaArchitecture.EventHelpers;
using EvaArchitecture.Logger;
using Game._Events;
using Game.Controllers.BotControllers;
using Game.Controllers.UiControllers.UiWindowControllers;
using Services.InputServices;
using UnityEngine;

namespace Game.Controllers.PlayerControllers
{
    public class PlayerController : SingletonMonoBehaviour<PlayerController>
    {
        private const string SHOOTABLE_MASK = "Shootable";
        
        #region Subscribe
        
        public override void Subscribe(bool mustSubscribe)
        {
            base.Subscribe(mustSubscribe);
            Eva.GetEvent<GameEventAppStarted>().Subscribe(mustSubscribe, OnEvent);
        }
        
        #endregion
        
        #region Serialized fields
        
        [SerializeField] private bool _useTouchPad;
        [SerializeField] private SimpleTouchAreaButton _joystickJump;
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _audioSourceFootSteps;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private GameObject _atCameraInitial;
        [SerializeField] private float _speed = 6.0f;
        [SerializeField] private float _speedRotation = 70f;
        [SerializeField] private float _jumpSpeed = 8.0f;
        [SerializeField] private float _gravity = 20.0f;
        [SerializeField] private float _range = 1024f;

        #endregion
        
        #region private Fields
        
        private Vector3 _moveDirection;
        private float _moveVer;
        private float _moveJump;
        private bool _isWalking;
        private bool _isJumping;
        private int _shootMask;
        
        #endregion
        
        #region Properties
        
        public bool UseTouchPad => _useTouchPad;
        private Camera Camera => GameController.Instance.IsNull() ? null : GameController.Instance.Camera;

        #endregion

        #region unity methods
        
        protected override void Start()
        {
            base.Start();
            Init();
        }
        
        protected override void Update()
        {
            base.Update();

            InternalUpdate();
        }

        #endregion
        
        #region public methods
        
        #endregion
        
        #region private methods

        private void Init()
        {
            _animator.enabled = false;
            SetCameraInitialPosition();
            _shootMask = LayerMask.GetMask(SHOOTABLE_MASK);
            PlayerExplorerUiWindowController.Instance.MoveTouchPad.Init();
        }

        private void SetCameraInitialPosition()
        {
            var cameraTransform = Camera.transform;
            cameraTransform.SetParent(this.transform);
            cameraTransform.localPosition = _atCameraInitial.transform.localPosition;
            cameraTransform.localRotation = _atCameraInitial.transform.localRotation;
        }

        private void InternalUpdate()
        {
            if (_characterController.isGrounded)
            {
                SetSight();
                ProcessInput();
                PlayAudioFootsteps();
            }

            Move();
        }

        private void SetSight()
        {
            var ev = Eva.GetEvent<GameEventPlayerSightColorChanged>();
            ev.Publish(Color.white);

            if (Camera.IsNull())
                return;
            
            var ray = Camera.ScreenPointToRay(ScreenHelper.GetScreenCenter());
            if (Physics.Raycast(ray, out var hit, _range))
            {
                var enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null) // if this is the enemy robot (or bot) then
                {
                    ev.Publish(Color.red);
                }
            }
        }

        private void PlayAudioFootsteps()
        {
            if (!_audioSourceFootSteps.isPlaying && _isWalking)
            {
                _audioSourceFootSteps.Play();
            }
            else if (_audioSourceFootSteps.isPlaying && ((!_isWalking) || _isJumping))
            {
                _audioSourceFootSteps.Stop();
            }
        }

        private void ProcessInput()
        {
            Vector2 movement;
            if (_useTouchPad)
            {
                movement = PlayerExplorerUiWindowController.Instance.MoveTouchPad.GetDirection();
            }
            else
            {
                movement = new Vector3(InputService.Instance.GetAxis("Horizontal"), InputService.Instance.GetAxis("Vertical"));
            }

            var rotation = movement.x * _speedRotation * Time.deltaTime;
            _moveVer = movement.y;
            transform.Rotate(0, rotation, 0);

            var isJumpPressed = false;
            if (_useTouchPad)
            {
                isJumpPressed = _joystickJump.Touched;
            }
            else
            {
                isJumpPressed = InputService.Instance.GetButton("Jump");
            }

            if (isJumpPressed)
            {
                _moveJump = _jumpSpeed;
                _moveVer *= 3f;
            }
            else
            {
                _moveJump = 0f;
            }

            _isWalking = ((0f != rotation) || (0f != _moveVer));
            _isJumping = (0f != _moveJump);
            _animator.enabled = _isWalking && (!_isJumping);
                
            if (_isWalking || _isJumping)
            {
                _moveDirection = new Vector3(0f, 0f, _moveVer);
                _moveDirection *= _speed;
                _moveDirection.y = _moveJump;
                _moveDirection = transform.TransformDirection(_moveDirection);
            }
            else
            {
                _moveDirection = Vector3.zero;
            }
        }

        private void Move()
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
            _characterController.Move(_moveDirection * Time.deltaTime);
        }

        private void OnEvent(GameEventAppStarted ev, (DateTime, string) model, List<object> results)
        {
            Log.Info(() => $"PlayerController, GameEvents.App.StartedType, model={model}");
        }
        
        #endregion
    }
}
