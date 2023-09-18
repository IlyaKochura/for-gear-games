using Pool.Contracts;
using UnityEngine;

namespace Monsters.Contracts
{
    public class Monster : MonoBehaviour, IVulnerable, IRecycle
    {
        private Transform _targetPos;
        private float _speed;
        private int _maxHp;

        public Vector3 speedV3;
        public Vector3 SpeedV3 => speedV3;

        private readonly float _reachDistance = 0.5f;
        private Transform _recyclePos;
        
        private void FixedUpdate()
        {
            if (_targetPos == null)
            {
                return;
            }

            if (Vector3.Distance(transform.position, _targetPos.position) <= _reachDistance)
            {
                Recycle();
                return;
            }

            var translation = _targetPos.position - transform.position;

            if (translation.magnitude > _speed)
            {
                translation = translation.normalized * _speed;
            }

            transform.Translate(translation);

            speedV3 = translation;
        }

        public void SetTarget(Transform target)
        {
            _targetPos = target;
        }

        public void SetHp(int hp)
        {
            _maxHp = hp;
        }

        public void SetSpeed(float speedf)
        {
            _speed = speedf;
        }

        public void Recycle()
        {
            gameObject.SetActive(false);
        }

        public void TakeDamage(int damageCount)
        {
            _maxHp -= damageCount;
            if (_maxHp <= 0)
            {
                Recycle();
            }
        }
    }
}