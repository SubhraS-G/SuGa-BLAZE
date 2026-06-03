using UnityEngine;

public class PlayerAnimation3D : MonoBehaviour
{
    private Animator animator;
    private PlayerController3D controller;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<PlayerController3D>();
    }

    void Update()
    {
        if (animator == null || controller == null) return;

        animator.SetFloat("Speed",
            controller.enabled ? 1f : 0f);
        animator.SetBool("IsGrounded",
            controller.isGrounded);
        animator.SetBool("IsSliding",
            controller.isSliding);
    }
}