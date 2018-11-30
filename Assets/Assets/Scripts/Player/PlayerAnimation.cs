using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    Animator animator;
    string name;
    Player_ML playerInput;
	// Use this for initialization
	void Awake () {
        animator = GetComponentInChildren<Animator>();
        name = this.gameObject.name;
        if (name == "Player")
            playerInput = ML_Manager.Instance.playerInput;
        else
            playerInput = ML_Manager.Instance2.playerInput;

    }

    // Update is called once per frame
    void Update () {
        //animator.SetFloat("Vertical", GameManager.Instance.InputController.Vertical);
        //animator.SetFloat("Horizontal", GameManager.Instance.InputController.Horizontal);
        //animator.SetBool("IsWalking", GameManager.Instance.InputController.IsWalking);
        //animator.SetBool("IsSprinting", GameManager.Instance.InputController.IsRunning);
        //animator.SetBool("IsCrouched", GameManager.Instance.InputController.IsCrouched);
        animator.SetFloat("Vertical", playerInput.Vertical);
        animator.SetFloat("Horizontal", playerInput.Horizontal);
        animator.SetBool("IsWalking", playerInput.IsWalking);
        animator.SetBool("IsSprinting",playerInput.IsRunning);
        animator.SetBool("IsCrouched", playerInput.IsCrouched);
    }
}
