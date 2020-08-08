using Invaders.Tools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGrid : StateBehaviour
{
    [SerializeField]
    private Enemy enemyPrefab;

    [SerializeField]
    private LevelSettings settings;
    [SerializeField]
    private GameSettings gameSettings;

    [SerializeField]
    private Transform enemyRoot;

    [SerializeField]
    private float offset;

    private Enemy[,] activeEnemies = new Enemy[0,0];
    private GameObject gridInstance;
    private float direction;
    private HashSet<Enemy> matchedEnemies = new HashSet<Enemy>();

    private int TotalEnemies => (int)(settings.GridCollumns * settings.GridRows);

    private void Update()
    {
        MoveEnemies();
    }

    private void MoveEnemies()
    {
        if (gridInstance == null || CurrentState != GameState.Playing)
            return;

        if ((gridInstance.transform.position.x > gameSettings.PlayerBounds.y && direction > 0) || (gridInstance.transform.position.x < gameSettings.PlayerBounds.x && direction < 0))
        {
            direction *= -1;
            gridInstance.transform.Translate(Vector3.back, Space.Self);
        }
        gridInstance.transform.Translate((Vector3.right * Time.deltaTime * direction * settings.EnemySpeed.Evaluate((float)((float)ActiveEnemies() / (float)TotalEnemies))), Space.Self);
    }

    private void SpawnGrid()
    {
        gridInstance = Instantiate(new GameObject(), enemyRoot);
        
        activeEnemies = new Enemy[settings.GridCollumns, settings.GridRows];

        for (int y = 0; y < settings.GridRows; y++)
        {
            for (int x = 0; x < settings.GridCollumns; x++)
            {
                CreateNewEnemy(x, y);
            }
        }
    }
    
    private void ResetGrid()
    {
        Destroy(gridInstance);
        activeEnemies = new Enemy[0, 0];
    }

    private int ActiveEnemies()
    {
        int returnAmount = 0;
        for (int x = 0; x < settings.GridCollumns; x++)
        {
            for (int y = 0; y < settings.GridRows; y++)
            {
                if (activeEnemies[x,y] != null)
                {
                    returnAmount++;
                }
            }
        }
        return returnAmount;
    }

    private Enemy CreateNewEnemy(int x, int y)
    {
        Vector3 enemyPosition = new Vector3(x * (enemyPrefab.transform.localScale.x * offset), 0, y * (enemyPrefab.transform.localScale.y * offset));
        Enemy newEnemy = Instantiate(enemyPrefab.gameObject, gridInstance.transform.position + enemyPosition, enemyPrefab.gameObject.transform.rotation, gridInstance.transform).GetComponent<Enemy>();

        activeEnemies[x, y] = newEnemy;
        newEnemy.Initialize(new Vector2Int(x, y), settings);
        newEnemy.CurrentlyShooting = y == 0;
        newEnemy.EnemyKilled += KillNeighbours;
        return newEnemy;
    }

    private void KillNeighbours(Enemy enemy)
    {
        // remove the enemy reference from the grid array
        activeEnemies[enemy.Index.x, enemy.Index.y] = null;

        // enable firing for the closest enemy to the player in the collumn
        for (int y = 0; y < settings.GridRows; y++)
        {
            if (activeEnemies[enemy.Index.x, y] != null)
            {
                activeEnemies[enemy.Index.x, y].CurrentlyShooting = true;
                break;
            }
        }

        matchedEnemies.Add(enemy);

        int remainingNeighbours = 0;
        foreach (Enemy matched in matchedEnemies)
            remainingNeighbours += GetNeighbours(matched).Count();        

        if (remainingNeighbours <= 0)
        {
            // adding fibonacci points
            PointManager.Instance.AddScore(matchedEnemies.Count * CustomMaths.CalculateFibonacci(matchedEnemies.Count) * 10);
            matchedEnemies.Clear();

            if (ActiveEnemies() <= 1)
                SetState?.Invoke(GameState.Stopped);
            
            return;
        }

        // get all valid neighbours and destroy them
        foreach (Enemy neighbour in GetNeighbours(enemy))
            neighbour.Die();        
    }

    public IEnumerable<Enemy> GetNeighbours(Enemy checkEnemy)
    {
        List<Enemy> neighbours = new List<Enemy>();
        for (int x = checkEnemy.Index.x - 1; x <= checkEnemy.Index.x + 1; x++)
        {
            for (int y = checkEnemy.Index.y - 1; y <= checkEnemy.Index.y + 1; y++)
            {
                // avoid cycling through cells outside of grid scope or the current cell
                if (x >= 0 && y >= 0 && x < settings.GridCollumns && y < settings.GridRows && activeEnemies[x, y] != null && activeEnemies[x, y].Index != checkEnemy.Index && (x == checkEnemy.Index.x || y == checkEnemy.Index.y))
                {
                    // if only valid/linkable cells should be returned, skip the current cell if invalid
                    if (activeEnemies[x, y].EnemyColor != checkEnemy.EnemyColor)
                        continue;
                    neighbours.Add(activeEnemies[x, y]);
                }
            }
        }
        return neighbours;
    }

    public override void UpdateState(GameState state)
    {
        if (CurrentState == GameState.Stopped && state == GameState.Playing)
        {
            SpawnGrid();
            direction = 1;
        }
        if (CurrentState != GameState.Stopped && state == GameState.Stopped)
            ResetGrid();

        foreach (Enemy enemy in activeEnemies)
        {
            if (enemy != null)
                enemy.ShootingEnabled = state == GameState.Playing;
        }
        CurrentState = state;
    }
}
