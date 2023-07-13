using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public class ObjectToPool
    {
        public PooledObjectType type;
        public GameObject gameObject;
        public int amount;
        public Transform parent;
    }

    [Serializable]
    public class PooledObject
    {
        public string name;
        public GameObject gameObject;
        public Transform transform;
        public PooledObjectType type;

        public void ReturnToPool()
        {
            ObjectPool.Instance.TakeBack(this);
        }

        public void ReturnToPoolDelayed(float time)
        {
            ObjectPool.Instance.TakeBackDelayed(this, time);
        }
    }

    [Serializable]
    public enum PooledObjectType
    {
        Enemy,
        Boss,
        WandAmmo,
        Arrow
    }

    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance;
        public List<ObjectToPool> objectToPool;

        public bool isPoolSet;
        private List<PooledObject> allPooledObjects;
        public Dictionary<PooledObjectType, Queue<PooledObject>> PoolDictionaryType;
        public Queue<PooledObject> PooledObjectsQ;

        private void Awake()
        {
            Instance = this;
            StartPool();
        }


        private void StartPool()
        {
            PoolDictionaryType = new Dictionary<PooledObjectType, Queue<PooledObject>>();
            allPooledObjects = new List<PooledObject>();

            foreach (var item in objectToPool)
            {
                PooledObjectsQ = new Queue<PooledObject>();
                for (var i = 0; i < item.amount; i++)
                {
                    var obj = Instantiate(item.gameObject, item.parent);

                    obj.SetActive(false);

                    obj.name = item.type.ToString() + i;
                    var pooledObject = new PooledObject
                    {
                        name = item.type.ToString(),
                        gameObject = obj,
                        transform = obj.transform,
                        type = item.type
                    };
                    PooledObjectsQ.Enqueue(pooledObject);

                    allPooledObjects.Add(pooledObject);
                }

                PoolDictionaryType.Add(item.type, PooledObjectsQ);
            }

            isPoolSet = true;
        }

        public PooledObject GetPooledObject(PooledObjectType type, bool setActive = false)
        {
            if (!PoolDictionaryType.ContainsKey(type)) return null;

            var obj = PoolDictionaryType[type].Dequeue();
            if (obj.gameObject.activeSelf)
                return GetPooledObject(type);

            var prefabRotation = objectToPool.First(x => x.type == type).gameObject.transform.rotation;
            obj.transform.rotation = prefabRotation;
            obj.transform.localScale = objectToPool.First(x => x.type == type).gameObject.transform.localScale;
            obj.gameObject.SetActive(setActive);
            return obj;
        }


        public void TakeBack(PooledObject obj)
        {
            if (obj.gameObject == null) return;
            if (!obj.gameObject.activeSelf) return;

            obj.gameObject.SetActive(false);
            var objectType = obj.type;
            PoolDictionaryType[objectType].Enqueue(obj);
        }

        public void TakeBackDelayed(PooledObject pooledObject, float time)
        {
            StartCoroutine(TakeBackDelayedCoroutine(pooledObject, time));
        }

        private IEnumerator TakeBackDelayedCoroutine(PooledObject pooledObject, float time)
        {
            yield return new WaitForSeconds(time);
            TakeBack(pooledObject);
        }

        public void TakeBackAll(PooledObjectType typ)
        {
            var pooledObjects = allPooledObjects.Where(x => x.type == typ).ToList();
            foreach (var pooledObject in pooledObjects) TakeBack(pooledObject);
        }

        public void TakeBackAll()
        {
            foreach (var pooledObject in allPooledObjects) TakeBack(pooledObject);
        }

        public void SpawnAdditionalBatch(PooledObjectType type, int amount)
        {
            var obj = objectToPool.First(x => x.type == type);
            for (var i = 0; i < amount; i++)
            {
                var objToSpawn = Instantiate(obj.gameObject, obj.parent);
                objToSpawn.SetActive(false);
                objToSpawn.name = obj.type.ToString() + i;
                var pooledObject = new PooledObject
                {
                    name = obj.type.ToString(),
                    gameObject = objToSpawn,
                    transform = objToSpawn.transform,
                    type = obj.type
                };
                PoolDictionaryType[type].Enqueue(pooledObject);
                allPooledObjects.Add(pooledObject);
            }
        }
    }
}