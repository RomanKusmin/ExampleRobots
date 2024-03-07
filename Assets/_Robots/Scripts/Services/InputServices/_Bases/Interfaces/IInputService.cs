using Services._Bases.Interfaces;

namespace Services.InputServices._Bases.Interfaces
{
    public interface IInputService : IService
    {
        public abstract float GetAxis(string axisName);
        public abstract bool GetButton(string buttonName);
        public abstract bool GetMouseButtonDown(int button);
    }
}
