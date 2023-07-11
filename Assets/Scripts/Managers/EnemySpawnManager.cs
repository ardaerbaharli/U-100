using System;
using System.Collections;
using System.Linq;
using Enemy;
using Levels;
using Player;
using UnityEngine;
using WaveDesign;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [SerializeField] private LevelProperty levelProperty;

        private float _cameraSize;


        private PlayerManager _player;

        private void Awake()
        {
            _cameraSize = Helpers.Camera.orthographicSize;
        }

        private void Start()
        {
            GameManager.Instance.OnGameStarted += OnGameStarted;
        }

        private void OnGameStarted()
        {
            _player = PlayerManager.Instance;
            StartCoroutine(StartWaves());
        }

        private IEnumerator StartWaves()
        {
            foreach (var wave in levelProperty.Waves)
            {
                yield return StartCoroutine(StartWave(wave));
                yield return new WaitForSeconds(levelProperty.Interval);
            }
        }

        private IEnumerator StartWave(WaveProperty wave)
        {
            var enemies = wave.Enemies.dictionary;

            foreach (var enemy in enemies)
            {
                for (int i = 0; i < enemy.value; i++)
                {
                    SpawnEnemy(enemy.key);
                    yield return new WaitForSeconds(wave.SpawnInterval);
                }
            }
        }

        private void SpawnEnemy(EnemyProperty enemyProperty)
        {
            var isBoss = enemyProperty.IsBoss;
            var pooledObjectType = isBoss ? PooledObjectType.Boss : PooledObjectType.Enemy;
            var enemyPooledObject = ObjectPool.Instance.GetPooledObject(pooledObjectType);
            var enemy = enemyPooledObject.gameObject.GetComponent<EnemyController>();
            enemy.pooledObject = enemyPooledObject;
            var position = GetPosition();
            enemy.transform.position = position;

            enemy.SetEnemyProperty(enemyProperty);
            enemy.gameObject.SetActive(true);
            enemy.StartFollowing(_player);
        }

        private Vector2 GetPosition()
        {
            // position must be out of the screen but not too far, use camera size to calculate
            var x = Random.Range(-_cameraSize * 2, _cameraSize * 2);
            var y = Random.Range(-_cameraSize * 2, _cameraSize * 2);
            var position = new Vector2(x, y);
            return position;
        }
    }
}