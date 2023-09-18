using System.Linq;
using Monsters;
using Monsters.Contracts;
using Pool;
using Projectiles;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class SimpleController : MonoBehaviour
    {
        [SerializeField] private Transform startPoint;

        private float _timeToShoot;

        void Start()
        {
            _timeToShoot = GameSettings.Instance.GuidedShootInterval;
        }

        private void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            _timeToShoot -= Time.deltaTime;
            
            if (_timeToShoot <= 0)
            {
                if (ObjectPool.Instance.GetActiveObject<Monster>() == null)
                {
                    return;
                }
                
                var monster = ObjectPool.Instance.GetActiveObject<Monster>().FirstOrDefault(
                    x => (x.transform.position - transform.position).sqrMagnitude
                         < GameSettings.Instance.TowerTriggerDistance * GameSettings.Instance.TowerTriggerDistance);
            
                _timeToShoot = GameSettings.Instance.CannonShootInterval;
            
                var spawnedProjectile = ObjectPool.Instance.Spawn<GuidedProjectile>(
                    GameSettings.Instance.guidedProjectilePrefab, startPoint.position, startPoint.rotation);
                
                if (spawnedProjectile != null)
                {
                    var projectile = spawnedProjectile.GetComponent<GuidedProjectile>();
                    
                    projectile.SetSpeed(GameSettings.Instance.GuidedProjectileSpeed);
            
                    projectile.SetDamage(GameSettings.Instance.GuidedProjectileDamage);

                    if (monster == null)
                    {
                        return;
                    }

                    projectile.SetMonster(monster.GetComponent<Monster>());
                }
            }
        }
    }
}
