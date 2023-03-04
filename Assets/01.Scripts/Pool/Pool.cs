using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : IPoolable
{
    private Stack<T> pool = new Stack<T>();

    public Pool(T prefab, Transform parent)
    {
        for(int i = 0; i < 5; )
    }
}
