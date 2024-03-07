using EvaArchitecture.Controllers._Bases;
using UnityEngine.EventSystems;

namespace Game.Controllers.PlayerControllers
{
	public class SimpleTouchAreaButton : BaseMonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		private bool _touched;
		private int _pointerId;
		private bool _canFire;
		
		public bool Touched => _touched;

		protected override void Awake()
		{
			base.Awake();
			_touched = false;
		}

		public void OnPointerDown(PointerEventData data)
		{
			if (_touched) 
				return;
			
			_touched = true;
			_pointerId = data.pointerId;
		}

		public void OnPointerUp(PointerEventData data)
		{
			if (data.pointerId != _pointerId) 
				return;
			
			_touched = false;
		}
	}
}
