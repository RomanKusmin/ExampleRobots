using Core.Patterns;
using Services.InputServices._Bases.Interfaces;
using UnityEngine;

namespace Services.InputServices
{
    public class StandardInputService : BaseSingleton<StandardInputService>, IInputService
    {
        private const string STANDARD_INPUT_SERVICE_NAME = "StandardInputService";
        
        public string ServiceName => STANDARD_INPUT_SERVICE_NAME;
        public bool Init() => true;
        public bool DoDestroy() => true;

        public float GetAxis(string axisName) => Input.GetAxis(axisName);
        public bool GetButton(string buttonName) => Input.GetButton(buttonName);
        public bool GetMouseButtonDown(int button) => Input.GetMouseButtonDown(button);
    }
}
