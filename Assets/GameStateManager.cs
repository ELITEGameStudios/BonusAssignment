using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{
    private bool gameIsActive;
    public ISpawner fruitSpawner;
    public int fruitCount;
    public GameObject fruit;
    
    public Vector2 bounds;


    public static GameStateManager Instance {get; private set;}
    void Start() {
        if(Instance == null){Instance = this;}
        else if(Instance != this){Destroy(this);}

        fruit = new();

        StartGame();
    }

    public void EndGame()
    {
        Destroy(fruit.gameObject);
    }

    public void StartGame()
    {
        gameIsActive = true;
        Snake.Instance.Reset();
        for (int i = 0; i < fruitCount; i++)
        {
            fruitSpawner.DoSpawn();
        }
    }


    void Update()
    {
        Snake.Instance.Periodic();
    }

    public void SummonNewApple()
    {
        fruitSpawner.DoSpawn();
    }

}
