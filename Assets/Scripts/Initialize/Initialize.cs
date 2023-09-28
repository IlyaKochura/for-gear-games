using Pool;
using UnityEngine;

namespace Initialize
{
    public class Initialize : MonoBehaviour
    {
        private void Awake()
        {
            ObjectPool.CreateInstance();
        }
    }
}