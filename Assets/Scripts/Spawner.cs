using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float spawnAhead = 40f;
    public float minInterval = 1.5f;
    public float maxInterval = 3f;
    public float laneWidth = 2f;

    private Transform target;
    private float timer;
    private float next;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) target = p.transform;
        next = 2f;
    }

    void Update()
    {
        if (target == null) return;

        timer += Time.deltaTime;
        if (timer < next) return;

        timer = 0f;
        next = Random.Range(minInterval, maxInterval);

        if (prefabs.Length == 0) return;

        int idx = Random.Range(0, prefabs.Length);
        float x = 0f;

        float[] lanes = { -laneWidth, 0f, laneWidth };
        if (prefabs[idx].name == "LaneBlocker")
            x = lanes[Random.Range(0, 3)];

        Vector3 pos = new Vector3(x, 0f, target.position.z + spawnAhead);
        Instantiate(prefabs[idx], pos, Quaternion.identity);
    }
}