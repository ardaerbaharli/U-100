using System;
using System.Collections.Generic;
using Collectables;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        private List<CollectableProperty> _collectableProperties;
        public static CollectableManager Instance;

        private void Awake()
        {
            Instance = this;
            _collectableProperties = new List<CollectableProperty>();
            _collectableProperties.AddRange(Resources.LoadAll<CollectableProperty>("CollectableProperties"));
        }

        public void SpawnCollectable(CollectableType type, Vector2 pos)
        {
            var collectablePooledObject = ObjectPool.Instance.GetPooledObject(PooledObjectType.Collectable);
            collectablePooledObject.transform.position = pos;
            Collectable collectable = null;
            switch (type)
            {
                case CollectableType.Health:
                    collectable = collectablePooledObject.gameObject.AddComponent<Health>();
                    break;
                case CollectableType.Chest:
                    collectable = collectablePooledObject.gameObject.AddComponent<Chest>();
                    break;
                case CollectableType.Coin:
                    collectable = collectablePooledObject.gameObject.AddComponent<Coin>();
                    break;
            }

            if (collectable == null)
            {
                collectablePooledObject.ReturnToPool();
                return;
            }

            collectable.PooledObject = collectablePooledObject;
            collectable.gameObject.SetActive(true);
            collectable.SetCollectableProperty(GetProperty(type));
        }

        private CollectableProperty GetProperty(CollectableType type)
        {
            return _collectableProperties.Find(x => x.Type == type);
        }
    }
}