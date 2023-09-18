using System.Collections.Generic;
using Monsters;
using Monsters.Contracts;
using Pool;
using ScriptableObjects;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _moveTarget;
    [SerializeField] private GameSettings _settings;
    private float _timeToSpawn;
    private void Start()
    {
        _timeToSpawn = _settings.SpawnInterval;
    }

    void Update()
    {
        _timeToSpawn -= Time.deltaTime;
        
        if (_timeToSpawn <= 0)
        {
            _timeToSpawn = _settings.SpawnInterval;
            
            var parent = transform;
            var spawnedMonster = ObjectPool.Instance.Spawn<Monster>(GetRandomMonster(GameSettings.Instance.monstersPrefabs),
                parent.position, Quaternion.identity);

            var spawnedMonsterComponent = spawnedMonster.GetComponent<Monster>();
                
            spawnedMonsterComponent.SetTarget(_moveTarget);
            spawnedMonsterComponent.SetSpeed(_settings.MonsterSpeed);
            spawnedMonsterComponent.SetHp(_settings.MonsterHp);
        }
    }

    private GameObject GetRandomMonster(List<GameObject> listGameObjects)
    {
        var randomIndex = Random.Range(0, listGameObjects.Count);

        return listGameObjects[randomIndex];
    }
}