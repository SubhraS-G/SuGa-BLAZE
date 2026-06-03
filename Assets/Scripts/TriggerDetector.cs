using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            if (AudioManager.instance != null)
                AudioManager.instance.PlayCoin();

            Coin coin = other.GetComponent<Coin>();
            if (coin != null)
            {
                if (GameManager3D.instance != null)
                    GameManager3D.instance.AddCoins(coin.value);
                Destroy(other.gameObject);
            }
        }
    }
}