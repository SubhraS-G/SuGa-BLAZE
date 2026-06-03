using UnityEngine;

public class LaneFollow : MonoBehaviour
{
    public Transform player;
    public float length = 100f;

    private Vector3 startOffset;

    void Start()
    {
        if (player != null)
            startOffset = transform.position - player.position;
    }

    void Update()
    {
        if (player == null) return;

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            player.position.z + startOffset.z
        );
    }
}