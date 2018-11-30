using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour {
    [SerializeField] Text text;

    PlayerShoot playerShoot;
	// Use this for initialization
	void Awake () {
        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
        
    }
    void HandleOnLocalPlayerJoined (Player player)
    {

        playerShoot = player.GetComponent<PlayerShoot>();
        playerShoot.ActiveWeapon.reloader.OnAmmoChanged += HandleOnAmmoChanged;
      

    }

     void HandleOnAmmoChanged()
    {
        text.text = playerShoot.ActiveWeapon.reloader.RoundsRemainingInClip.ToString();
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
