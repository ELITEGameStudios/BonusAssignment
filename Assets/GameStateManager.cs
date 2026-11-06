using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{
    private bool gameIsActive;
    public ISpawner fruitSpawner;
    public int fruitCount;
    public GameObject fruit;
    
    public Vector2 bounds;
    public GameObject gameOverUI;


    public static GameStateManager Instance {get; private set;}
    void Awake() {
        if(Instance == null){Instance = this;}
        else if(Instance != this){Destroy(this);}

        fruit = new();

    }

    void Start()
    {
        
        StartGame();
    }

    public void EndGame()
    {
        Destroy(fruit.gameObject);
        gameIsActive = false;
        gameOverUI.SetActive(true);
    }

    public void StartGame()
    {
        gameIsActive = true;
        Snake.Instance.Reset();
        for (int i = 0; i < fruitCount; i++)
        {
            fruitSpawner.DoSpawn();
        }
        gameOverUI.SetActive(false);
    }


    void Update()
    {
        if(gameIsActive){
            Snake.Instance.Periodic();
        }
    }

    public void SummonNewApple()
    {
        fruitSpawner.DoSpawn();
    }

}
