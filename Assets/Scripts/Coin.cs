using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed = 180f;
    public int value = 10;

    void Update()
    {
        // Rotate around Y axis — faces player correctly
        transform.Rotate(rotateSpeed * Time.deltaTime, rotateSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager3D.instance != null)
                GameManager3D.instance.AddCoins(value);
            Destroy(gameObject);
        }
    }
}