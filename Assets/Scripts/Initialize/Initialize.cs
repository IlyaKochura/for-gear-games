using Pool;
using UnityEngine;

namespace Initialize
{
    public class Initialize : MonoBehaviour
    {
        private float _fps;
        
        private void Awake()
        {
            ObjectPool.CreateInstance();
        }
        void OnGUI()
        {
            _fps = 1.0f / Time.deltaTime;
            GUILayout.Label("FPS: " + (int)_fps);
        }
    }
}