using Fade.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fade.ObjectPool
{

    [Serializable]

    internal struct Pool
    {
        public List<GameObject> pool;
        public int poolSize;
        public GameObject prefab;
        public int ActiveObjectCount { get { return pool.Count; } }
    }

    internal class PoolManager : SingletonBehaviour<PoolManager>
    {
        [SerializeField]
        private Pool[] pools;

        private void Awake()
        {
            base.Awake();
            InitializePools();
            GenerateObjectsForPool();
        }
        private void InitializePools() => Array.ForEach(pools, pool => pool.pool = new List<GameObject>());
        private void GenerateObjectsForPool()
        {
            for (int i = 0; i < pools.Length; i++)
            {
                pools[i].pool = Enumerable.Range(0, pools[i].poolSize)
                    .Select(_ =>
                    {
                        return InstantiateObjectForPool(i);

                    }).ToList();
            }
        }

        internal GameObject GetPooledObject(int poolIndex)
        {
            if (poolIndex < 0 && poolIndex > pools.Length && pools == null) return null;

            if (pools[poolIndex].pool.All(t => t.activeInHierarchy))
            {
                GameObject obj = InstantiateObjectForPool(poolIndex);
                pools[poolIndex].pool.Add(obj);
                return obj;
            }

            else if (poolIndex >= 0 && poolIndex < pools.Length)
            {
                return pools[poolIndex].pool.FirstOrDefault(t => !t.activeInHierarchy);
            }
            return null;
        }
        private GameObject InstantiateObjectForPool(int poolIndex)
        {
            GameObject createdObj = Instantiate(pools[poolIndex].prefab, transform);
            createdObj.SetActive(false);
            return createdObj;
        }
        internal void GetBackInPool(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}