using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    private T prefab;

    [SerializeField]
    private int maxObjectLimit;

    public static GenericObjectPool<T> Instance { get; private set; }
    private Queue<T> objects = new Queue<T>();
    private int activeObjects;

    private void Awake() => Instance = this;

    public T GetPooledObject()
    {
        if (ActiveObjectLimitReached)
            return null;

        activeObjects++;
        if (objects.Count == 0)
            AddObject();
        return objects.Dequeue();
    }

    private bool ActiveObjectLimitReached => activeObjects >= maxObjectLimit;

    public void ReturnToPool(T returnObject)
    {
        activeObjects--;
        returnObject.gameObject.SetActive(false);
        objects.Enqueue(returnObject);
    }

    private void AddObject()
    {
        var newObject = Instantiate(prefab, transform);
        newObject.gameObject.SetActive(false);
        objects.Enqueue(newObject);
    }
}
