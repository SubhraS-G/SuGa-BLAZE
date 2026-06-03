using UnityEngine;

public class ObSpawner : MonoBehaviour
{
    [Header("Obstacles")]
    public GameObject obs1;
    public GameObject obs2;
    public GameObject obs3;

    [Header("Coins")]
    public GameObject coinPrefab;

    public Transform runner;
    public float ahead = 40f;
    public float laneWidth = 2f;

    float t = 0f;
    float next = 1.5f;
    float coinTimer = 0f;
    float nextCoin = 1f;

    void Update()
    {
        if (runner == null) return;

        // Obstacle spawning
        t += Time.deltaTime;
        if (t >= next)
        {
            t = 0f;
            next = Random.Range(1.5f, 3f);
            SpawnObstacle();
        }

        // Coin spawning — separate timer
        coinTimer += Time.deltaTime;
        if (coinTimer >= nextCoin)
        {
            coinTimer = 0f;
            nextCoin = Random.Range(0.8f, 2f);
            SpawnSingleCoin();
        }
    }

    void SpawnObstacle()
    {
        System.Collections.Generic.List<GameObject> choices =
            new System.Collections.Generic.List<GameObject>();
        if (obs1 != null) choices.Add(obs1);
        if (obs2 != null) choices.Add(obs2);
        if (obs3 != null) choices.Add(obs3);

        if (choices.Count == 0) return;

        int i = Random.Range(0, choices.Count);
        float[] lanes = { -laneWidth, 0f, laneWidth };
        float x = lanes[Random.Range(0, 3)];

        Vector3 p = new Vector3(x, 0f,
            runner.position.z + ahead);
        Instantiate(choices[i], p, Quaternion.identity);
    }

    void SpawnSingleCoin()
    {
        if (coinPrefab == null) return;

        float[] lanes = { -laneWidth, 0f, laneWidth };
        float x = lanes[Random.Range(0, 3)];

        // Random height — ground level or jump height
        float[] heights = { 0.8f, 0.8f, 2.5f };
        float y = heights[Random.Range(0, heights.Length)];

        float zOffset = Random.Range(ahead - 5f, ahead + 5f);

        Vector3 p = new Vector3(
            x, y,
            runner.position.z + zOffset
        );

        Instantiate(coinPrefab, p, Quaternion.identity);
    }
}