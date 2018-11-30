﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    Animator animator;
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
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetFloat("Vertical", GameManager.Instance.InputController.Vertical);
        animator.SetFloat("Horizontal", GameManager.Instance.InputController.Horizontal);
        animator.SetBool("IsWalking", GameManager.Instance.InputController.IsWalking);
        animator.SetBool("IsSprinting", GameManager.Instance.InputController.IsRunning);
        animator.SetBool("IsCrouched", GameManager.Instance.InputController.IsCrouched);
        animator.SetFloat ("AimAngle", PlayerAim.GetAngle());
    }
}
