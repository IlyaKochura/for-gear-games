using Monsters.Contracts;
using Pool.Contracts;
using ScriptableObjects;
using UnityEngine;

namespace Projectiles.Contracts
{
    public class BaseProjectile : MonoBehaviour, IRecycle
    {
        protected float speedProjectile;
        protected int damage;

        protected void CheckMaxRange(Vector3 startPos)
        {
            if (Vector3.Distance(startPos, transform.position) >= GameSettings.Instance.MaxProjectileRange)
            {
                Recycle();
            }
        }

        public void SetSpeed(float speed)
        {
            speedProjectile = speed;
        }

        public void SetDamage(int damage)
        {
            this.damage = damage;
        }
        
        public void Recycle()
        {
            gameObject.SetActive(false);
        }

        public void OnTrigger(IVulnerable obj)
        {
            obj.TakeDamage(damage);
        }
    }
}