using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Destructable {
    [SerializeField] float inSeconds;
    [HideInInspector]
    /// <summary>
    /// The associated agent.
    /// This will be set by the agent script on Initialization. 
    /// Don't need to manually set.
    /// </summary>
	public Player_ML agent;  //
    public override void Die()
    {
        base.Die();
        GameManager.Instance.Respawner.Despawn(gameObject, inSeconds);
    }
    private void OnEnable()
    {
        Reset();
    }

    public override void TakeDamage(float amount)
    {
        
        base.TakeDamage(amount);
        agent.Goal();
        print("Remaining: " + HitPointsRemaining);
    }
}
