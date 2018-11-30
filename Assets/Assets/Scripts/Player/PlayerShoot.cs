using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {


    [SerializeField] float weaponSwitchTime;
    Shooter[] weapons;
    Shooter activeWeapon;
    int currentWeaponIndex;
    bool canFire;
    Transform weaponHolster;

    public Shooter ActiveWeapon
    {
        get
        {
            return activeWeapon;
        }
    }
    private void Awake()
    {

        canFire = true;
        weaponHolster = transform.Find("Weapons");
        weapons = weaponHolster.GetComponentsInChildren<Shooter>();
        
        if (weapons.Length > 0)
            Equip(0);
    }
    private void Update()
    {
        if (GameManager.Instance.InputController.MouseWheelDown)
            SwitchWeapon(1);

        if (GameManager.Instance.InputController.MouseWheelUp)
            SwitchWeapon(-1);

        ///To be done with SPRINTING WALKING
        //if (GameManager.Instance.LocalPlayer.PlayerState.MoveState == PlayerState.EMoveState.SPRINTING);
        //return;

        if (!canFire)
            return;
        if (GameManager.Instance.InputController.Fire1)
        {
            activeWeapon.Fire();
        }
    }
    void DeactivateWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].transform.SetParent(weaponHolster);
            weapons[i].gameObject.SetActive(false);
        }
    }

    void SwitchWeapon(int direction)
    {
        canFire = false;
        currentWeaponIndex += direction;
        if (currentWeaponIndex > weapons.Length - 1)
            currentWeaponIndex = 0;
        if (currentWeaponIndex < 0)
            currentWeaponIndex = weapons.Length - 1;

        GameManager.Instance.Timer.Add(() =>
        {
            Equip(currentWeaponIndex);
        }, weaponSwitchTime);
    }
    void Equip(int index)
    {
        DeactivateWeapon();
        canFire = true;
        activeWeapon = weapons[index];
        activeWeapon.Equip();
        
        weapons[index].gameObject.SetActive(true);

    }
}
