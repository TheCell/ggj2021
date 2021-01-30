using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject DragDropItemPrefab;
    private static HashSet<TypeObjectData> spawnedObjectsHashSet = new HashSet<TypeObjectData>();
    private static List<TypeObjectData> spawnedObjects = new List<TypeObjectData>();

    public void Start()
    {
        if (DragDropItemPrefab == null)
        {
            return;
        }

        var items = new List<ItemTypes>();
        items.AddRange(PersistenceSingleton.Instance.FoundItems);

        for(int i = 0; i < items.Count; i++)
        {
            var typeObjectData = new TypeObjectData();
            typeObjectData.itemType = items[i];

            if (!spawnedObjectsHashSet.Contains(typeObjectData))
            {
                var dragDropItem = Instantiate<GameObject>(DragDropItemPrefab);
                dragDropItem.transform.SetParent(null);
                var dragDropItemComponent = dragDropItem.GetComponent<DragDropItem>();
                dragDropItemComponent.ItemType = items[i];

                typeObjectData.gameObject = dragDropItem;
                spawnedObjectsHashSet.Add(typeObjectData);
                spawnedObjects.Add(typeObjectData);
                DontDestroyOnLoad(dragDropItem);
            }
        }

        foreach (TypeObjectData typeObjectData in spawnedObjects)
        {
            typeObjectData.gameObject.SetActive(true);
        }
    }

    public void Update()
    {
        
    }

    public void OnDestroy()
    {
        foreach(TypeObjectData typeObjectData in spawnedObjects)
        {
            if (typeObjectData.gameObject != null)
            {
                typeObjectData.gameObject.SetActive(false);
            }
        }
    }
}
