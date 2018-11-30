using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    Animator animator;
    Player_ML playerInput;
    // Use this for initialization
    private PlayerAim m_PlayerAim;
    private PlayerAim PlayerAim
    {
        get
        {
            if (m_PlayerAim == null)
                // PlayerAim or Playeraim ?????!!!
                m_PlayerAim = GameManager.Instance.LocalPlayer.playeraim;
            return m_PlayerAim;
        }
    }
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
        animator.SetFloat("Vertical", playerInput.Vertical);
        animator.SetFloat("Horizontal", playerInput.Horizontal);
        animator.SetBool("IsWalking", playerInput.IsWalking);
        animator.SetBool("IsSprinting", playerInput.IsRunning);
        animator.SetBool("IsCrouched", playerInput.IsCrouched);
        animator.SetFloat ("AimAngle", PlayerAim.GetAngle());
    }
}
