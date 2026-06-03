using UnityEngine;

public class EndlessTrack : MonoBehaviour
{
    public Transform player;
    public float trackLength = 40f;
    public int numberOfSegments = 3;

    private GameObject[] segments;
    private float segmentOffset;
    private int lastSegmentIndex = 0;

    void Start()
    {
        segments = new GameObject[numberOfSegments];
        segmentOffset = trackLength;

        // Create track segments
        for (int i = 0; i < numberOfSegments; i++)
        {
            segments[i] = CreateSegment(i * trackLength);
        }
    }

    void Update()
    {
        if (player == null) return;

        // Reposition segments that are behind player
        for (int i = 0; i < numberOfSegments; i++)
        {
            if (segments[i] == null) continue;

            float segEnd = segments[i].transform.position.z
                + trackLength / 2f;

            if (segEnd < player.position.z - 10f)
            {
                // Find furthest segment
                float furthestZ = GetFurthestZ();
                segments[i].transform.position = new Vector3(
                    0f, 0f, furthestZ + trackLength
                );
            }
        }
    }

    GameObject CreateSegment(float zPos)
    {
        GameObject seg = GameObject.CreatePrimitive(
            PrimitiveType.Plane);
        seg.name = "TrackSegment";
        seg.transform.position = new Vector3(0f, 0f, zPos);
        seg.transform.localScale = new Vector3(3f, 1f, 8f);

        // Copy material from original track
        GameObject originalTrack = GameObject.Find("Track");
        if (originalTrack != null)
        {
            Renderer r = seg.GetComponent<Renderer>();
            Renderer or = originalTrack.GetComponent<Renderer>();
            if (r != null && or != null)
                r.material = or.material;
        }

        // Set layer to Ground
        seg.layer = LayerMask.NameToLayer("Ground");

        return seg;
    }

    float GetFurthestZ()
    {
        float furthest = float.MinValue;
        foreach (GameObject seg in segments)
        {
            if (seg != null && seg.transform.position.z > furthest)
                furthest = seg.transform.position.z;
        }
        return furthest;
    }
}