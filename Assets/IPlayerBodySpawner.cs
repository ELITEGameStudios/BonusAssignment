using UnityEngine;

public class IPlayerBodySpawner : ISpawner
{
    public PlayerSegment segmentPrefab;
    public override void DoSpawn()
    {
        PlayerSegment segment = Instantiate(segmentPrefab, Snake.Instance.transform.position, transform.rotation);
        Snake.Instance.AddSegment(segment);
    }
}
