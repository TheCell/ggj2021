using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _collectedItems = 0;
    private UIManager _comUIManager;

    // Start is called before the first frame update
    void Start()
    {
        _comUIManager = GameObject.Find("InventoryCanvas").GetComponent<UIManager>();
        
        foreach(ItemTypes item in PersistenceSingleton.Instance.FoundItems)
        {
            AddCollectible(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCollectible(ItemTypes itemType)
    {
        _collectedItems++;
        
        switch (itemType)
        {
            case ItemTypes.Item_CigaretteButt:
                _comUIManager.DiplayItemOne();
                break;
            case ItemTypes.Item_IDCard:
                _comUIManager.DisplayItemTwo();
                break;
            case ItemTypes.Item_Invoice:
                _comUIManager.DisplayItemThree();
                break;
            case ItemTypes.Item_Jewelry:
                _comUIManager.DisplayItemFour();
                break;
            case ItemTypes.Item_KODrops:
                _comUIManager.DisplayItemFive();
                break;
            case ItemTypes.Item_Rosin:
                _comUIManager.DisplayItemSix();
                break;
            case ItemTypes.Item_Warderobenumber:
                _comUIManager.DisplayItemSeven();
                break;
            default:
                break;
        }
    }

}
