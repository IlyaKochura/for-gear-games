using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class GameSettings : ScriptableObject
    {
        public static GameSettings Instance => _instance ? _instance : _instance = CreateInstance<GameSettings>();

        [Header("Prefabs")] 
        public List<GameObject> monstersPrefabs = new List<GameObject>();
        public GameObject guidedProjectilePrefab;
        public GameObject cannonProjectilePrefab;
        
        [Header("Speed Settings")]
        [Range(0.2f, 1f)] public float CannonProjectileSpeed = 0.2f;
        [Range(0.2f, 1f)] public float GuidedProjectileSpeed = 0.2f;
        [Range(0.2f, 1f)] public float MonsterSpeed = 0.2f;
        
        [Header("HP and DamageSettings")]
        public int MonsterHp = 100;
        public int CannonProjectileDamage = 30;
        public int GuidedProjectileDamage = 10;
        
        [Header("Intervals")]
        public float SpawnInterval;
        public float CannonShootInterval = 0.5f;
        public float GuidedShootInterval = 0.5f;
        
        [Header("Distance")]
        public float TowerTriggerDistance = 10f;
        public float MaxProjectileRange = 50f;
        
        
        private static GameSettings _instance;

        public GameSettings()
        {
            _instance = this;
        }
    }
}