using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform player;
    public float segmentLength = 50f;
    public int numberOfSegments = 3;

    private GameObject[] segments;
    private float startZ;

    void Start()
    {
        if (player == null) return;

        segments = new GameObject[numberOfSegments];
        startZ = player.position.z;

        // Create copies of background
        for (int i = 0; i < numberOfSegments; i++)
        {
            segments[i] = Instantiate(
                gameObject,
                new Vector3(
                    transform.position.x,
                    transform.position.y,
                    startZ + (i * segmentLength)
                ),
                transform.rotation
            );
            // Remove this script from copies
            Destroy(segments[i].GetComponent<BackgroundFollow>());
        }

        // Hide original
        GetComponent<MeshRenderer>().enabled = false;
    }

    void LateUpdate()
    {
        if (player == null || segments == null) return;

        foreach (GameObject seg in segments)
        {
            if (seg == null) continue;

            // If segment is behind player — move it ahead
            if (seg.transform.position.z <
                player.position.z - segmentLength)
            {
                float furthestZ = GetFurthestZ();
                seg.transform.position = new Vector3(
                    seg.transform.position.x,
                    seg.transform.position.y,
                    furthestZ + segmentLength
                );
            }
        }
    }

    float GetFurthestZ()
    {
        float furthest = float.MinValue;
        foreach (GameObject seg in segments)
        {
            if (seg != null &&
                seg.transform.position.z > furthest)
                furthest = seg.transform.position.z;
        }
        return furthest;
    }
}