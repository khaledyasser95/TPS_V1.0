using MLAgents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPS_ML : Agent
{
    public Transform Target;
    string[] detectableObjects;
    RayPerception rayPer;
    Rigidbody agentRB;
    ML_Anim anim;
    public int speed;
    public Rigidbody projectile;
    private float fireRate = 5f;
    private float nextTimeToFire = 0f;
    [HideInInspector]
    private Transform Char_Forward;
    public override void InitializeAgent()
    {
        Char_Forward = transform.Find("front");
        rayPer = GetComponent<RayPerception>();
        anim = GetComponent<ML_Anim>();
        detectableObjects = new string[] { "Enemy" };
        agentRB = GetComponent<Rigidbody>();
    }

    public override void AgentReset()
    {
        if (this.transform.position.y < -1.0 || this.transform.position.y > 2.0)
        {
            // The Agent fell
            agentRB.transform.position = Vector3.zero;
            agentRB.angularVelocity = Vector3.zero;
            agentRB.velocity = Vector3.zero;
        }
        else
        {
            // Move the target to a new spot
            Target.position = new Vector3(Random.value * 8 - 3,
                                          0.5f,
                                          Random.value * 8 - 3);

        }
    }

    public override void CollectObservations()
    {
        Vector3 relativePosition = Target.position - this.transform.position;
        // Relative position
        AddVectorObs(relativePosition /5f);
        var rayDistance = 12f;
        float[] rayAngles = { 0f, 45f, 90f, 135f, 180f, 110f, 70f };
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0.1f, 0f));
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 1.5f, 0f));
        //Position of the Agent itself within the confines of the floor. This data is collected as the Agent's distance from each edge of the floor.
        // Distance to edges of platform

    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        
        MoveAgent(vectorAction);
       // Fire();
        
        if (this.transform.position.y < -1.0 || this.transform.position.y > 5.0)
        {
            AddReward(-1.0f);
            Done();
        }

    }
    void fire2()
    {
        Instantiate(projectile, Char_Forward.position, transform.rotation);
    }
    void cont_Move(float[] act)
    {
        Vector3 controlSignal = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;
        controlSignal.x = act[0];
        controlSignal.z = act[1];
        controlSignal.y = act[2];
        rotateDir = transform.up * act[3];
        Vector2 direction = new Vector2(act[0] * speed, act[0] * speed);

        transform.Rotate(rotateDir, Time.deltaTime * 300f);
        //The RollerAgent applies the values from the action[] array to its Rigidbody component, rBody, using the Rigidbody.AddForce
        agentRB.AddForce(direction);
        anim.Vertical = controlSignal.z;
        anim.Horizontal = controlSignal.x;

    }
    public void MoveAgent(float[] act)
    {

        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;
       
        int dirToGoForwardAction = (int)act[1];
        int rotateDirAction = (int)act[3];
        int dirToGoSideAction = (int)act[0];
        int jumpAction = (int)act[2];


      
        if (jumpAction == 1)
            fire2();

        

        if (dirToGoForwardAction == 1)
            dirToGo = transform.forward * 1f ;
        else if (dirToGoForwardAction == -1)
            dirToGo = transform.forward * -1f;
        if (rotateDirAction > 0)
            rotateDir = transform.up * -1f;
        else if (rotateDirAction < 0)
            rotateDir = transform.up * 1f;
        if (dirToGoSideAction == -1)
            dirToGo = transform.right * -0.6f;
        else if (dirToGoSideAction == 1)
            dirToGo = transform.right * 0.6f ;
       
        transform.Rotate(rotateDir, Time.fixedDeltaTime * 200f);
        agentRB.AddForce(dirToGo * speed);
        anim.Vertical = dirToGo.z;
        anim.Horizontal = dirToGo.x;

    }
    void Fire()
    {
        if (Time.time > nextTimeToFire)
        {
            RaycastHit hit;
            nextTimeToFire = Time.time + 1f / fireRate;
            if (Physics.Raycast(agentRB.position + new Vector3(0, 0.5f, 0), Char_Forward.forward, out hit))
            {

                if (hit.transform.tag == "Enemy")
                {
                    print("HIT");
                    // Instantiate the projectile at the position and rotation of this transform
                    Rigidbody clone;
                    clone = Instantiate(projectile, Char_Forward.position, transform.rotation);
                    // Give the cloned object an initial velocity along the current
                    // object's Z axis
                    clone.velocity = transform.TransformDirection(Vector3.forward * 5 * Time.deltaTime);

                }



            }
        }

    }
}
