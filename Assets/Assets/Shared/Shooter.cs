using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile;
    [SerializeField] AudioControl audioReload;
    [SerializeField] AudioControl audioFire;

    public Reloader reloader;

    float nextFireAllowed;
    [HideInInspector]
    private Transform muzzle;
    public bool canFire;
    [SerializeField] Transform hand;
    private void Awake()
    {
        muzzle = GameObject.FindWithTag("Muzzle").transform;
        if (!muzzle)
        {
            Debug.Log("HELLO I FIND YOUR FUCKIN MUZZle   ", muzzle);
        }
        reloader = gameObject.GetComponent<Reloader>();
        canFire = true;
        
    }
    public void Equip()
    {
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
   
    public void Reload()
    {
        if (reloader == null)
            return;
        reloader.Reload();
        audioReload.Play();
    }
    // Virtual so we can overide it from different class
    public virtual void Fire()
    {
       
        canFire = false;
        if (Time.time < nextFireAllowed)
            return;
        if (reloader != null)
        {
            if (reloader.IsReloading)
                return;
            if (reloader.RoundsRemainingInClip == 0)
                return;
            reloader.TakeFromClip(1);
        }
        nextFireAllowed = Time.time + rateOfFire;
        //Instantiate bullet
        Instantiate(projectile, muzzle.position, muzzle.rotation);
        audioFire.Play();
        canFire = true;
    }
}
