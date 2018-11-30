using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    Transform cameraLookTarget;

    [SerializeField] Vector3 cameraOffset;
    [SerializeField] float damping;
    [SerializeField] Transform target;
    private  Player localPlayer;

	// Use this for initialization
	void Awake () {
        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined; ;
        cameraLookTarget = target.transform.Find("cameraLookTarget");
    }

    private void HandleOnLocalPlayerJoined(Player player)
    {
        localPlayer = player;
        //cameraLookTarget = localPlayer.transform.Find("cameraLookTarget");
     
        if (cameraLookTarget == null) { 
            cameraLookTarget = localPlayer.transform;
        }

    }

    // Update is called once per frame
    void Update () {
         Camera();
        
    }
    void Local_Player()
    {
        if (localPlayer == null)
            return;
        Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraOffset.z +
            localPlayer.transform.up * cameraOffset.y +
            localPlayer.transform.right * cameraOffset.x;
        Quaternion targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);
        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, damping * Time.deltaTime);
    }
    void Camera()
    {
      
        Vector3 targetPosition = cameraLookTarget.position + target.transform.forward * cameraOffset.z +
            target.transform.up * cameraOffset.y +
            target.transform.right * cameraOffset.x;
        Quaternion targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);
        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, damping * Time.deltaTime);
    }
}
