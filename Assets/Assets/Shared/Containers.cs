using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Containers : MonoBehaviour
{
    [Serializable]
    public class ContainerItem
    {
        public Guid ID;
        public string Name;
        public int Maximum;

        private int amountTaken;
        public ContainerItem()
        {
            ID = Guid.NewGuid();
        }
        public int Remaining
        {
            get
            {
                return Maximum - amountTaken;
            }
        }
        internal int Get(int amount)
        {
            if ((amountTaken + amount) > Maximum)
            {
                int toMuch = (amountTaken + amount) - Maximum;
                amountTaken = Maximum;
                return amount - toMuch;
            }
            amountTaken += amount;
            return amount;
        }

    }
    public List<ContainerItem> Items;
    public event Action OnContainerReady;
     void Awake()
    {
        Items = new List<ContainerItem>();
        if (OnContainerReady != null)
            OnContainerReady();
        
    }
    public Guid Add(string name, int maximum)
    {
        Items.Add(new ContainerItem
        {
            Maximum = maximum,
            Name = name
        });
        return Items.Last().ID;

    }

    public int TakeFromContainer(Guid id, int amount)
    {
        var containerItem = GetContainerItem(id);
        if (containerItem == null)
            return -1;
        return containerItem.Get(amount);
    }

    public int GetAmountRemaining(System.Guid id)
    {
        var containerItem = GetContainerItem(id);
        if (containerItem == null)
            return -1;
        return containerItem.Remaining;
    }
    private ContainerItem GetContainerItem(System.Guid id)
    {
        var containerItem = Items.Where(x => x.ID == id).FirstOrDefault();
        if (containerItem == null)
            return null;
        return containerItem;
    }
}
