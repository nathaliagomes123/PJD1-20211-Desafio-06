using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory = FactoryController;

public class FactoryController : MonoBehaviour
{
    static private Transform tf;

    static protected Dictionary<string, GameObject> register = new Dictionary<string, GameObject>();
    static protected Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        tf = GetComponent<Transform>();

    }

    static public void Clear()
    {
        register.Clear();
        pool.Clear();
    }

    static public void Register(string key, GameObject prefab, int count)
    {
        register.Add(key, prefab);

        GameObject parent = new GameObject(prefab.name);
        parent.transform.parent = tf;

        Queue<GameObject> queue = new Queue<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(prefab, parent.transform);
            go.name = key + "_" + i;
            IPoolableObject obj = go.GetComponent<IPoolableObject>();
            obj.Recycle();
            queue.Enqueue(go);
        }
        pool.Add(key, queue);
    }

    static public GameObject GetObject(string key, Vector3 position, Quaternion rotation)
    {
        GameObject go = pool[key].Dequeue();
        go.transform.position = position;
        go.transform.rotation = rotation;
        IPoolableObject obj = go.GetComponent<IPoolableObject>();
        obj.TurnOn();
        return go;
    }

    static public void Recycle(string key, GameObject go)
    {
        IPoolableObject obj = go.GetComponent<IPoolableObject>();
        obj.Recycle();
        pool[key].Enqueue(go);
    }
}
