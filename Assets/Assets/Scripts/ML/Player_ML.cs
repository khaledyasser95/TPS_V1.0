using MLAgents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ML : Agent {
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
    public Transform Target;
    string[] detectableObjects;
    [HideInInspector]
    public Health goalDetect;
    RayPerception rayPer;

    public override void AgentReset()
    {
        //if (this.transform.position.y < -1.0 || this.transform.position.y > 2.0)
        //{
        //    // The Agent fell
        //    transform.position = Vector3.zero;
            
        //}
        //else
        //{
        //    // Move the target to a new spot
        //    Target.position = new Vector3(Random.value * 8 - 3,
        //                                  0.5f,
        //                                  Random.value * 8 - 3);

        //}

    }
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        MoveAgent(vectorAction);
        AddReward(-0.1f);
        if (this.transform.position.y < -1.0 || this.transform.position.y > 5.0)
        {
            
            Done();
            AddReward(-1f);
        }

    }
    public override void InitializeAgent()
    {
        goalDetect = Target.GetComponent<Health>();
        goalDetect.agent = this;
        rayPer = GetComponent<RayPerception>();
        detectableObjects = new string[] { "Enemy"};
    }
    public override void CollectObservations()
    {
        Vector3 relativePosition = Target.position - this.transform.position;
        // Relative position
        AddVectorObs(relativePosition.magnitude /5f);
        AddVectorObs(this.transform.rotation.y);
        var rayDistance = 12f;
        float[] rayAngles = { 0f, 45f, 90f, 135f, 180f, 110f, 70f };
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0.1f, 0f));
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 1.5f, 0f));
    }
 
    public void Goal()
    {
        Done();
        AddReward(1f);
    }
    public void MoveAgent(float[] act)
    {
        /*
         * 0 Vertical Move
         * 1 Horizontal Move
         * 2 Mouse X
         * 3 Mouse Y
         * 4 Mouse Scroll
         * 5 Fire
         * 6 Reload
         * 7 Walking
         * 8 Running
         * 9 Crouch
         */
        Vertical = act[0];
        Horizontal = act[1];
        MouseInput = new Vector2(act[2], act[3]);
        Fire1 = act[5]>0;
        Reload = act[6]>0;
        IsWalking = act[7]>0;
        IsRunning = act[8]>0;
        IsCrouched = act[9]>0;
        MouseWheelUp = act[4] > 0;
        MouseWheelDown = act[4] < 0;
       
    }
}
