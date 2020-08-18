using UnityEngine;

public class PalmSpawner : MonoBehaviour
{
    [SerializeField]
    private PalmTreePool spawner;

    [SerializeField]
    private float delay;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > delay)
        {
            timer = 0;
            PalmTree tree = spawner.GetPooledObject();
            if (tree != null)
                tree.Initialize(spawner.spawnLocations[Random.Range(0,spawner.spawnLocations.Length)]);
            
        }
    }

}
