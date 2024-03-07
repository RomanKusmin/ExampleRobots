using EvaArchitecture.Controllers._Bases;
using Game.Controllers.PlayerControllers;
using Game.Controllers.UiControllers.UiWindowControllers._Bases.Interfaces;
using UnityEngine;

namespace Game.Controllers.UiControllers.UiWindowControllers
{
    public class PlayerExplorerUiWindowController : SingletonMonoBehaviour<PlayerExplorerUiWindowController>, IUiWindowController
    {
        #region Serialized fields
        
        [SerializeField] private SimpleTouchPad _moveTouchPad;
        [SerializeField] private SimpleTouchAreaButton _fireTouchPad;

        #endregion
        
        #region Properties
        
        public SimpleTouchPad MoveTouchPad => _moveTouchPad;
        public SimpleTouchAreaButton FireTouchPad => _fireTouchPad;

        #endregion
    }
}
