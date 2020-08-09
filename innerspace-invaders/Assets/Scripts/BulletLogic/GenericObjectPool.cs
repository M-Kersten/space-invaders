using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Logic for pooling a generic object, add as a component to make the gameobject a pool to spawn the objects in
/// </summary>
/// <typeparam name="T">The object you're pooling, has to be monobehaviour to allow for instantiating</typeparam>
public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{
    public static GenericObjectPool<T> Instance { get; private set; }

    [SerializeField]
    private T prefab;

    [SerializeField]
    private int maxObjectLimit;

    private bool ActiveObjectLimitReached => activeObjects >= maxObjectLimit;
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
