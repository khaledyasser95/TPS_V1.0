﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState1 : MonoBehaviour {
	
	public enum EMoveState
	{
		WALKING,
		RUNNING,
		CROUCHING,
		SPRINTING
	}

	public enum EWeaponState
	{
		IDLE,
		FIRING,
		AIMING,
		AIMEDFIRING
	}
	public EMoveState MoveState;
	public EWeaponState WeaponState;

	private Player_ML m_inputController;
	public Player_ML InputController 
	{
		get{
            name = this.gameObject.name;
            if (m_inputController == null) {
                if (name == "Player")
                    m_inputController = ML_Manager.Instance.playerInput;
                else
                    m_inputController = ML_Manager.Instance2.playerInput;
            }
            return m_inputController;
			}
	}
     
	void Update()
	{
		SetMovestate ();
		SetWeaponState ();
	}

	void SetMovestate()
	{
		MoveState = EMoveState.RUNNING;

        // TODO  Change Sprinting to RUNNING 
		if (InputController.IsRunning)
			MoveState = EMoveState.SPRINTING;
		if (InputController.IsWalking)
			MoveState = EMoveState.WALKING;
		if (InputController.IsCrouched)
			MoveState = EMoveState.CROUCHING;
		
	}

	void SetWeaponState()
	{
		WeaponState = EWeaponState.IDLE;

		if (InputController.Fire1)
			WeaponState = EWeaponState.FIRING;

		//if (InputController.Fire2)
		//	WeaponState = EWeaponState.AIMING;

		//if (InputController.Fire1 && InputController.Fire2)
		//	WeaponState = EWeaponState.FIRING;
	}


		


}