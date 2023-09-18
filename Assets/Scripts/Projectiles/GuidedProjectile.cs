using System;
using Contracts;
using Monsters.Contracts;
using UnityEngine;

namespace Projectiles
{
	public class GuidedProjectile : BaseProjectile
	{
		private Monster _monster;
		private Vector3 _startPos;

		private void Start()
		{
			_startPos = transform.position;
		}

		private void FixedUpdate()
		{
			CheckMaxRange(_startPos);

			if (_monster == null || !_monster.gameObject.activeSelf) 
			{
				Recycle();
				return;
			}

			var positionMonster = _monster.transform.position;

			var selfPosition = transform.position;

			var translation = positionMonster - selfPosition;
		
			if (translation.magnitude > speedProjectile) 
			{
				translation = translation.normalized * speedProjectile;
			}
		
			transform.Translate (translation);
		}

		public void SetMonster(Monster monster)
		{
			_monster = monster;
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
