using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloader : MonoBehaviour
{

    [SerializeField] int maxAmmo;
    [SerializeField] float reloadTime;
    [SerializeField] int clipSize;
    [SerializeField] Containers Inventory;
    public int shotFiredInClip;
    bool isReloading;

    public event System.Action OnAmmoChanged;
    System.Guid containerItemID;
    void Awake()
    {
        Inventory.OnContainerReady += () =>
        {
            containerItemID = Inventory.Add(this.name, maxAmmo);
        };

    }
    public int RoundsRemainingInClip
    {
        get
        {
            return clipSize - shotFiredInClip;
        }
    }
    //public int RoundsRemainingInInventory
    //{
    //    get
    //    {
    //        return Inventory.Items.l;
    //    }
    //}
    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }
    public void Reload()
    {
        if (isReloading)
            return;
        isReloading = true;
       
        print("Reload Started");
        GameManager.Instance.Timer.Add(() => { ExecuteReload(Inventory.TakeFromContainer(containerItemID, clipSize - RoundsRemainingInClip)); }, reloadTime);
        
    }
    private void ExecuteReload(int amount)
    {
        print("Reload Executed");
        isReloading = false;
        shotFiredInClip -= amount;
        if (OnAmmoChanged != null)
            OnAmmoChanged();
    }

    public void TakeFromClip(int amount)
    {
        shotFiredInClip += amount;
        if (OnAmmoChanged != null)
            OnAmmoChanged();
    }
}
