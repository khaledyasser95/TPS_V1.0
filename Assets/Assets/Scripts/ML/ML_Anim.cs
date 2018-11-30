using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ML_Anim : MonoBehaviour {
    Animator animator;

    public float Vertical;
    public float Horizontal;
    public Vector2 MouseInput;
    public bool Fire1;
    public bool Reload;
    public bool IsWalking;
    public bool IsRunning;
    public bool IsCrouched;
    public bool MouseWheelUp;
    public bool MouseWheelDown;

    // Use this for initialization
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Vertical", Vertical);
        animator.SetFloat("Horizontal", Horizontal);
        animator.SetBool("IsWalking", IsWalking);
        animator.SetBool("IsSprinting", IsRunning);
        animator.SetBool("IsCrouched", IsCrouched);
    }
}
