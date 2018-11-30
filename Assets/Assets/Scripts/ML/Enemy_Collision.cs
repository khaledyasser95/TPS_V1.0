using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Collision : MonoBehaviour {

    [HideInInspector]
    /// <summary>
    /// The associated agent.
    /// This will be set by the agent script on Initialization. 
    /// Don't need to manually set.
    /// </summary>
	public Player_ML agent;  //

    void OnCollisionEnter(Collision col)
    {
        // Touched goal.
        
        if (col.gameObject.CompareTag("goal"))
        {
            agent.Goal();
        }
    }
}
