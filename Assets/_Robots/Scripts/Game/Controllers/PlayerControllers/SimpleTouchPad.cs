using EvaArchitecture.Controllers._Bases;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Controllers.PlayerControllers
{
	public class SimpleTouchPad : BaseMonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		#region Serialized fields
		
		[SerializeField] private float _smoothing;
		
		#endregion
		
		#region private fields

		private Vector2 _origin;
		private Vector2 _direction;
		private Vector2 _smoothDirection;
		private bool _touched;
		private int _pointerId;
		
		#endregion

		#region unity events
		
		protected override void Awake()
		{
			base.Awake();
			_direction = Vector2.zero;
			_touched = false;
		}

		public void OnPointerDown(PointerEventData data)
		{
			if (_touched) 
				return;
			
			_touched = true;
			_pointerId = data.pointerId;
			_origin = data.position;
		}

		public void OnDrag(PointerEventData data)
		{
			if (data.pointerId != _pointerId) 
				return;
			
			var currentPosition = data.position;
			var directionRaw = currentPosition - _origin;
			_direction = directionRaw.normalized;
		}

		public void OnPointerUp(PointerEventData data)
		{
			if (data.pointerId != _pointerId) 
				return;
			
			_direction = Vector2.zero;
			_touched = false;
		}
		
		#endregion
		
		#region public methods
		
		public void Init()
		{
			_origin = Vector2.zero;
			_direction = Vector2.zero;
			_smoothDirection = Vector2.zero;
			_touched = false;
			_pointerId = -9999;
		}

		public Vector2 GetDirection()
		{
			_smoothDirection = Vector2.MoveTowards(_smoothDirection, _direction, _smoothing);
			return _smoothDirection;
		}
		
		#endregion
	}
}
