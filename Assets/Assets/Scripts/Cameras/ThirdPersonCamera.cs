using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	[System.Serializable]
	public class CameraRig
	{

		public Vector3 CameraOffset;
		public float Damping;
        public float CrouchHeight;



    }



    [SerializeField] 
	CameraRig defaultCamera;
	[SerializeField] 
	CameraRig aimCamera;


    Transform cameraLookTarget;
	Player localPlayer;
//    [SerializeField]Vector3 cameraOffset;
//    [SerializeField]float damping;

	// Use this for initialization
	void Awake () {
        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined; ;
    }

    private void HandleOnLocalPlayerJoined(Player player)
    {
        localPlayer = player;
        cameraLookTarget = localPlayer.transform.Find("cameraLookTarget");
        if (cameraLookTarget == null) { 
            cameraLookTarget = localPlayer.transform;
        }

    }

    // Update is called once per frame
    void Update () {
        if (localPlayer == null)
            return;

		CameraRig cameraRig = defaultCamera;
        //


        if (localPlayer.PlayerState.WeaponState == PlayerState1.EWeaponState.AIMEDFIRING || localPlayer.PlayerState.WeaponState == PlayerState1.EWeaponState.AIMING)
            cameraRig = aimCamera;
        float targetHeight = cameraRig.CameraOffset.y + (localPlayer.PlayerState.MoveState == PlayerState1.EMoveState.CROUCHING ? cameraRig.CrouchHeight : 0);
        Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraRig.CameraOffset.z +
			localPlayer.transform.up * targetHeight +
            localPlayer.transform.right *  cameraRig.CameraOffset.x;
		 
        Quaternion targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);
       
		transform.position = Vector3.Lerp(transform.position, targetPosition,  cameraRig.Damping * Time.deltaTime);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,  cameraRig.Damping * Time.deltaTime);
    }
}
