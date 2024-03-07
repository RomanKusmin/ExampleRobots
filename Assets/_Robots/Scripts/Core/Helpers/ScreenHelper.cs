using UnityEngine;

namespace Core.Helpers
{
    public static class ScreenHelper
    {
        public static Vector3 GetScreenCenter()
        {
            var screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            return screenCenter;
        }
    }
}
