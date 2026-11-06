
using UnityEngine;

public class IFruitSpawner : ISpawner
{
    public GameObject fruitPrefab;
    public override void DoSpawn()
    {
        GameObject fruit = Instantiate(
            fruitPrefab, 
            new Vector3(
                Random.Range(-GameStateManager.Instance.bounds.x, GameStateManager.Instance.bounds.x),
                Random.Range(-GameStateManager.Instance.bounds.y, GameStateManager.Instance.bounds.y),
                0
            ), 
            transform.rotation
        );


        GameStateManager.Instance.fruit = fruit;
    }
}
