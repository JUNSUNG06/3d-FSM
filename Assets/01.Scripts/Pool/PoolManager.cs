using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    public List<IPoolable> poolObjectList;
    public Dictionary<string, List<IPoolable>> pool = new Dictionary<string, List<IPoolable>>();

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

        foreach(IPoolable prefab in poolObjectList)
        {
            IPoolable obj = GameObject.Instantiate(prefab);
        }
    }

    public void Pop(string name)
    {
        
    }

    public void Push(IPoolable obj)
    {

    }
}
