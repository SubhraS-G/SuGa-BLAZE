using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController3D : MonoBehaviour
{
    [Header("Lane Settings")]
    public float laneWidth = 2f;
    public float laneSwitchSpeed = 10f;
    private int currentLane = 0;

    [Header("Jump Settings")]
    public float jumpForce = 10f;

    [Header("Slide Settings")]
    public float slideDuration = 0.8f;

    [Header("Run Settings")]
    public float forwardSpeed = 8f;
    public float speedIncreaseRate = 0.05f;

    private CharacterController cc;
    private float verticalVelocity = 0f;
    private float gravity = -25f;
    public bool isSliding = false;
    public bool isGrounded = false;
    private float targetX = 0f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!enabled) return;

        // Ground check
        isGrounded = cc.isGrounded;

        // Speed increase
        forwardSpeed += speedIncreaseRate * Time.deltaTime;

        // Gravity
        if (isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;
        else
            verticalVelocity += gravity * Time.deltaTime;

        // Lane switch
        if (Keyboard.current.aKey.wasPressedThisFrame ||
            Keyboard.current.leftArrowKey.wasPressedThisFrame)
            SwitchLane(-1);

        if (Keyboard.current.dKey.wasPressedThisFrame ||
            Keyboard.current.rightArrowKey.wasPressedThisFrame)
            SwitchLane(1);

        // Jump
        if ((Keyboard.current.spaceKey.wasPressedThisFrame ||
             Keyboard.current.wKey.wasPressedThisFrame ||
             Keyboard.current.upArrowKey.wasPressedThisFrame)
             && isGrounded && !isSliding)
            Jump();

        // Slide
        if ((Keyboard.current.sKey.wasPressedThisFrame ||
             Keyboard.current.downArrowKey.wasPressedThisFrame)
             && isGrounded && !isSliding)
            StartCoroutine(Slide());

        // Smooth lane switch
        float newX = Mathf.Lerp(
            transform.position.x,
            targetX,
            laneSwitchSpeed * Time.deltaTime
        );

        // Move
        Vector3 move = new Vector3(
            newX - transform.position.x,
            verticalVelocity * Time.deltaTime,
            forwardSpeed * Time.deltaTime
        );

        cc.Move(move);
    }

    void SwitchLane(int dir)
    {
        int newLane = currentLane + dir;
        if (newLane < -1 || newLane > 1) return;
        currentLane = newLane;
        targetX = currentLane * laneWidth;
    }

    void Jump()
    {
        verticalVelocity = jumpForce;
        if (AudioManager.instance != null)
            AudioManager.instance.PlayJump();
    }

    System.Collections.IEnumerator Slide()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySlide();

        isSliding = true;
        cc.height = 0.4f;
        cc.center = new Vector3(0, 0.2f, 0);

        // Only scale character child not whole player
        Transform character = transform.Find("characterMedium");
        if (character != null)
            character.localScale = new Vector3(1f, 0.4f, 1f);

        yield return new WaitForSeconds(slideDuration);

        cc.height = 2f;
        cc.center = new Vector3(0, 1f, 0);

        if (character != null)
            character.localScale = new Vector3(1f, 1f, 1f);

        isSliding = false;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            GameManager3D gm = FindAnyObjectByType<GameManager3D>();
            if (gm != null) gm.GameOver();
        }
    }
}