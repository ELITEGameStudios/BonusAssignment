using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private bool gameIsActive;
    public static GameStateManager Instance {get; private set;}
    void Start() {
        if(Instance == null){Instance = this;}
        else if(Instance != this){Destroy(this);}

        StartGame();
    }

    public void EndGame()
    {
        
    }

    public void StartGame()
    {
        gameIsActive = true;
        Snake.Instance.Reset();
    }

    void Update()
    {
        Snake.Instance.Periodic();
    }

    public void SummonNewApple()
    {
        
    }

}
