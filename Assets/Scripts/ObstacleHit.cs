using UnityEngine;

public class ObstacleHit : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit obstacle!");
            GameManager3D gm = FindAnyObjectByType<GameManager3D>();
            if (gm != null) gm.GameOver();
        }
    }
}