using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : Poolable
{
    private Stack<T> pool = new Stack<T>();
    private T prefab;
    private Transform parent;

    public Pool(T _prefab, Transform _parent, int count = 10)
    {
        prefab = _prefab;
        parent = _parent;

        for(int i = 0; i < count; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            obj.gameObject.SetActive(false);
            pool.Push(obj);
        }
    }

    public T Pop()
    {
        T obj = null;

        if(pool.Count == 0)
        {
            obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = pool.Pop();
            obj.gameObject.SetActive(true);
        }

        return obj;
    }

    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Push(obj);
    }
}
