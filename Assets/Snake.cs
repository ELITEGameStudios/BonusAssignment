using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Snake : MonoBehaviour
{

    public Vector2 nextMovementInput;
    public Vector2 currentPosition;
    public ISpawner playerSegmentSpawner;

    public int tickInterval = 45;
    private int currentTick;
    private int applesEaten = 0;

    private List<PlayerSegment> segmentObjectList;


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

    public void AddSegment(PlayerSegment segment)
    {
        segmentObjectList.Add(segment);
    }

    public void BanishSegments()
    {
        for (int i = segmentObjectList.Count-1; i <= 0; i--)
        {
            Destroy(segmentObjectList[i].gameObject);
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
        transform.position = currentPosition;
        playerSegmentSpawner.DoSpawn();

    }

    void EatApple(){
        GameStateManager.Instance.SummonNewApple();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "playerSegment")
        {
            GameStateManager.Instance.EndGame();
        }    
        else if(other.tag == "fruit")
        {
            Destroy(other.gameObject);
            GameStateManager.Instance.SummonNewApple();
        }    
    }
}
