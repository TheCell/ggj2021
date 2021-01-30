using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject DragDropItemPrefab;

    public void Start()
    {
        if (DragDropItemPrefab == null)
        {
            Debug.LogError("missing DragDropPrefab");
            return;
        }

        var items = new List<ItemTypes>();
        items.AddRange(PersistenceSingleton.Instance.FoundItems);

        for(int i = 0; i < items.Count; i++)
        {
            var dragDropItem = Instantiate<GameObject>(DragDropItemPrefab);
            dragDropItem.transform.SetParent(null);
            var dragDropItemComponent = dragDropItem.GetComponent<DragDropItem>();
            dragDropItemComponent.ItemType = items[i];
        }
    }

    public void Update()
    {
        
    }
}
