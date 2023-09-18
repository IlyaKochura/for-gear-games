using System.Linq;
using Contracts;
using Monsters.Contracts;
using Pool;
using Projectiles;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class CannonController : MonoBehaviour
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform _activePart;
        private float _timeToShoot;

        private void Start()
        {
            _timeToShoot = GameSettings.Instance.CannonShootInterval;
        }

        private void Update()
        {
            CalculatePreemption();
        }

        private void Shoot()
        {
            _timeToShoot -= Time.deltaTime;

            if (_timeToShoot <= 0)
            {
                _timeToShoot = GameSettings.Instance.CannonShootInterval;

                var spawnedProjectile = ObjectPool.Instance.Spawn<CannonProjectile>(
                    GameSettings.Instance.cannonProjectilePrefab, startPoint.position, startPoint.rotation);

                if (spawnedProjectile == null)
                {
                    return;
                }

                var projectile = spawnedProjectile.GetComponent<CannonProjectile>();

                projectile.SetSpeed(GameSettings.Instance.CannonProjectileSpeed);

                projectile.SetDamage(GameSettings.Instance.CannonProjectileDamage);
            }
        }

        private float CalculateBulletSpeed()
            {
                return GameSettings.Instance.CannonProjectileSpeed * Vector3.forward.z * 50;
            }

            private void CalculatePreemption()
            {
                if (ObjectPool.Instance.GetActiveObject<Monster>() == null)
                {
                    return;
                }

                var activeMonster = ObjectPool.Instance.GetActiveObject<Monster>().FirstOrDefault(
                    x => (x.transform.position - transform.position).sqrMagnitude
                         < GameSettings.Instance.TowerTriggerDistance * GameSettings.Instance.TowerTriggerDistance);

                if (activeMonster == null)
                {
                    return;
                }
                
                var monster = activeMonster.GetComponent<Monster>();
                
                Vector3 targetPosition = monster.transform.position;
                Vector3 targetVel = monster.SpeedV3 * 50;
                float bulletSpeed = CalculateBulletSpeed();

                float leadDistance = CalculateLead(targetPosition, targetVel, bulletSpeed, startPoint.position);
                Vector3 aimPoint = targetPosition + targetVel * (leadDistance / bulletSpeed);

                Aim(aimPoint);
            }

            float CalculateLead(Vector3 targetPos, Vector3 targetVel, float bulletSpeed, Vector3 bulletPos)
            {
                Vector3 relativePos = targetPos - bulletPos;
                float timeToIntercept = relativePos.magnitude / bulletSpeed;
                Vector3 interceptPoint = targetPos + targetVel * timeToIntercept;
                return (interceptPoint - bulletPos).magnitude;
            }

            public void Aim(Vector3 target)
            {
                var selfTransform = transform;
                selfTransform.LookAt(target);
                _activePart.eulerAngles = new Vector3(0, selfTransform.eulerAngles.y, 0);
                Shoot();
#if UNITY_EDITOR
                Debug.DrawLine(startPoint.position, target, Color.red);
#endif
            }
        
    }
}