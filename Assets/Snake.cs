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
    public int applesEaten = 0;
    private int currentTick;

    private List<PlayerSegment> segmentObjectList;


    public static Snake Instance {get; private set;}
    void Awake() {
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

            for (int i = 0; i < segmentObjectList.Count; i++)
            {
                segmentObjectList[i].UpdateMe();
            }

        }

    } 

    public void AddSegment(PlayerSegment segment)
    {
        segmentObjectList.Add(segment);
    }

    public void KillSegment(PlayerSegment segment)
    {
        segmentObjectList.Remove(segment);
        Destroy(segment.gameObject);
    }

    public void BanishSegments()
    {
        if(segmentObjectList.Count == 0) return;
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

        Vector2 candaditeMovement = new();
        if(currentHorizontal != 0){candaditeMovement = new Vector2(currentHorizontal / Mathf.Abs(currentHorizontal), 0);}
        if(currentVertical != 0){candaditeMovement = new Vector2(0, currentVertical / Mathf.Abs(currentVertical));}
        if(candaditeMovement != Vector2.zero)
        {
            if(candaditeMovement * -1 != nextMovementInput)
            {
                nextMovementInput = candaditeMovement;
            }
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
        Debug.Log("test");
        if(other.tag == "playerSegment")
        {
            GameStateManager.Instance.EndGame();
        }    
        else if(other.gameObject == GameStateManager.Instance.fruit)
        {
            Debug.Log("hi");
            Destroy(other.gameObject);
            GameStateManager.Instance.SummonNewApple();
        }    
    }
}
