using System.Collections.Generic;

public sealed class PersistenceSingleton
{
    private static PersistenceSingleton instance = null;

    public static List<ItemTypes> FoundItems
    {
        get { return foundItems;  }
    }
    private static List<ItemTypes> foundItems = new List<ItemTypes>();

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
        foundItems.Add(itemType);
    }
}
