using Monsters.Contracts;
using Projectiles.Contracts;
using UnityEngine;

namespace Projectiles
{
    public class CannonProjectile : BaseProjectile
    {
        private Vector3 _startPos;

        private void Start()
        {
            _startPos = transform.position;
        }

        private void FixedUpdate()
        {
            CheckMaxRange(_startPos);
            var tr = Vector3.forward * speedProjectile;
            transform.Translate(tr);
        }

        void OnTriggerEnter(Collider other)
        {
            Recycle();
        
            var damageTakeObj = other.gameObject.GetComponent<IVulnerable>();
            if (damageTakeObj == null)
            {
                return;
            }
        
            damageTakeObj.TakeDamage(damage);
        }
    }
}