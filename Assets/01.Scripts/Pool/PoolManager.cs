using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    public List<Poolable> poolObjectList;
    public Dictionary<string, Pool<Poolable>> poolList = new Dictionary<string, Pool<Poolable>>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CreatePool();
    }

    public Poolable Pop(string name, Vector3 position)
    {
        if (!poolList.ContainsKey(name))
        {
            Debug.Log("풀 없음");
            return null;
        }

        Poolable obj = poolList[name].Pop();
        obj.transform.position = position;
        obj.Reset();
        return obj;
    }

    public void Push(Poolable obj)
    {
        if(!poolList.ContainsKey(obj.gameObject.name))
        {
            Debug.Log("풀 없음");
            return;
        }

        poolList[obj.gameObject.name].Push(obj);
    }

    private void CreatePool()
    {
        foreach(Poolable poolObj in poolObjectList)
        {
            Pool<Poolable> pool = new Pool<Poolable>(poolObj, transform);
            poolList.Add(poolObj.gameObject.name, pool);
        }
    }
}
