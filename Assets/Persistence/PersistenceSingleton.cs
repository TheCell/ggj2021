using System.Collections.Generic;
using UnityEngine;

public sealed class PersistenceSingleton
{
    private static PersistenceSingleton instance = null;

    public static HashSet<ItemTypes> FoundItems
    {
        get { return foundItems;  }
    }
    private static HashSet<ItemTypes> foundItems = new HashSet<ItemTypes>();

    private PersistenceSingleton() { }

    public static PersistenceSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PersistenceSingleton();
            }

            return instance;
        }
    }

    public void AddNewItem(ItemTypes itemType)
    {
        //Debug.Log("adding item " + itemType);
        var success = foundItems.Add(itemType);
        //Debug.Log("item was already in hashset: " + !success);
    }
}
