using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        Transform cameraLookTarget;
        public Transform target;
        public Vector3 offset;
        [SerializeField] float damping;

        private void Start()
        {
            cameraLookTarget = target.transform.Find("cameraLookTarget");
        }
        private void Update()
        {
           
            transform.position = target.position + offset;
            Vector3 targetPosition = cameraLookTarget.position + target.transform.forward * offset.z +
         target.transform.up * offset.y +
         target.transform.right * offset.x;
            Quaternion targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);
            transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, damping * Time.deltaTime);
        }
    }
}
