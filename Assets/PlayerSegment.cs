using UnityEngine;

public class PlayerSegment : MonoBehaviour
{
    [SerializeField] int ticks;
    public void UpdateMe()
    {
        ticks++;
        if(ticks > Snake.Instance.applesEaten + 1)
        {
            Snake.Instance.KillSegment(this);
        }
    }
}
