using UnityEngine;

public class RunningManAnimation : MonoBehaviour
{
    [Header("Bob Settings")]
    public float bobSpeed = 10f;
    public float bobHeight = 0.1f;

    [Header("Lean Settings")]
    public float leanAmount = 5f;

    [Header("Arm Swing")]
    public Transform leftArm;
    public Transform rightArm;
    public float armSwingAmount = 30f;
    public float armSwingSpeed = 10f;

    private float bobTimer = 0f;
    private Vector3 startPos;
    private PlayerController3D player;

    void Start()
    {
        startPos = transform.localPosition;
        player = GetComponentInParent<PlayerController3D>();
    }

    void Update()
    {
        if (player == null) return;
        if (!player.enabled) return;

        bobTimer += Time.deltaTime * bobSpeed;

        // Bob up and down
        float newY = startPos.y +
            Mathf.Sin(bobTimer) * bobHeight;
        transform.localPosition = new Vector3(
            startPos.x, newY, startPos.z);

        // Swing arms if assigned
        if (leftArm != null && rightArm != null)
        {
            float swing = Mathf.Sin(bobTimer) * armSwingAmount;
            leftArm.localRotation =
                Quaternion.Euler(swing, 0f, 0f);
            rightArm.localRotation =
                Quaternion.Euler(-swing, 0f, 0f);
        }

        // Lean forward slightly
        transform.localRotation = Quaternion.Euler(
            leanAmount, 180f, 0f);
    }
}