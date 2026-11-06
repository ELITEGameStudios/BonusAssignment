using UnityEngine;
using System;

public class Snake : MonoBehaviour
{

    public Vector2 nextMovementInput;
    public Vector2 currentPosition;
    public Vector2 bounds;

    public int tickInterval = 45;
    private int currentTick;
    private int applesEaten = 0;

    private List<GameObject>() segmentObjectList;


    public static Snake Instance {get; private set;}
    void Start() {
        if(Instance == null){Instance = this;}
        else if(Instance != this){Destroy(this);}
        segmentObjectList = new();
    }


    public void Periodic()
    {
        HandleInput();
        HandleTick();
    }

    public void Reset()
    {
        BanishSegments();
    }

    void HandleTick(){
        currentTick--;
        
        if(currentTick <= 0){
            currentTick = tickInterval;
            DoMove();
        }

    } 

    public void AddSegment(GameObject segment)
    {
        segmentObjectList.Add(segment);
    }

    public void BanishSegments()
    {
        for (int i = segmentObjectList.Count-1; i <= 0; i--)
        {
            Destroy(segmentObjectList[i]);
        }

        segmentObjectList.Clear();
    }

    void HandleInput()
    {

        float currentHorizontal = Input.GetAxisRaw("Horizontal");
        float currentVertical = Input.GetAxisRaw("Vertical");
        if(currentHorizontal != 0){nextMovementInput = new Vector2(currentHorizontal / Mathf.Abs(currentHorizontal), 0);}
        if(currentVertical != 0){nextMovementInput = new Vector2(0, currentVertical / Mathf.Abs(currentVertical));}


        if(nextMovementInput.magnitude != 0){
            nextMovementInput = nextMovementInput;
        }
    }

    void DoMove(){
        currentPosition += nextMovementInput; 
    }

    void EatApple(){
        GameStateManager.Instance.SummonNewApple();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "playerSegment")
        {
            GameStateManager.Instance.EndGame();
        }    
    }
}
