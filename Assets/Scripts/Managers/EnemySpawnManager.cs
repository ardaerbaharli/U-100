using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using Levels;
using Player;
using UnityEngine;
using WaveDesign;

namespace Managers
{
    public class EnemySpawnManager : MonoBehaviour
    {
        public static EnemySpawnManager Instance;
        [SerializeField] private LevelProperty levelProperty;

        private float _cameraSize;


        private PlayerManager _player;

        private List<EnemyController> activeEnemies;

        private void Awake()
        {
            Instance = this;
            _cameraSize = Helpers.Camera.orthographicSize;
            activeEnemies = new List<EnemyController>();
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
                for (var i = 0; i < enemy.value; i++)
                {
                    SpawnEnemy(enemy.key);
                    yield return new WaitForSeconds(wave.SpawnInterval);
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

            activeEnemies.Add(enemy);

            enemy.OnEnemyDied += () => activeEnemies.Remove(enemy);
            enemy.gameObject.SetActive(true);

            enemy.SetEnemyProperty(enemyProperty);
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

        public Vector2 GetClosestEnemyDirection(Vector3 target)
        {
            var closestEnemy = activeEnemies.Where(x => !x.isDead)
                .OrderBy(enemy => Vector2.Distance(enemy.transform.position, target))
                .FirstOrDefault();
            if (closestEnemy == null) return Vector2.zero;

            return (closestEnemy.transform.position - target).normalized;
        }
    }
}