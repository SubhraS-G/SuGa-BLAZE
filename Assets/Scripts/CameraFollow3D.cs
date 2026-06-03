using UnityEngine;

public class CameraFollow3D : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        if (player == null) return;

        // Fixed height, follows Z only
        transform.position = new Vector3(
            0f,
            5f,
            player.position.z - 10f
        );

        transform.rotation = Quaternion.Euler(20f, 0f, 0f);
    }
}