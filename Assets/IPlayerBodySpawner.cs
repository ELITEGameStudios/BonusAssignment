using UnityEngine;

public class IPlayerBodySpawner : MonoBehaviour, ISpawner
{
    public PlayerSegment segmentPrefab;
    public void DoSpawn()
    {
        PlayerSegment segment = Instantiate(segmentPrefab, Snake.Instance.transform.position, transform.rotation);
        Snake.Instance.AddSegment(segment);
    }
}
