using UnityEngine;

public class Persistence_ItemDemo : MonoBehaviour
{
    [SerializeField]
    private ItemTypes itemType;

    public ItemTypes ItemType
    {
        get { return itemType; }
    }
}
