using UnityEngine;

public class Item : MonoBehaviour
{
    public readonly ItemTypes itemType;

    [SerializeField]
    private AudioClip[] _FXClip; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp()
    {
        Player player = GameObject.Find("Detective").GetComponent<Player>();
        player.AddCollectible(itemType);
        PersistenceSingleton.Instance.AddNewItem(itemType);


        switch (itemType)
        {
            case ItemTypes.Item_CigaretteButt:
                AudioSource.PlayClipAtPoint(_FXClip[0], transform.position, 1.0f);
                break;
            case ItemTypes.Item_IDCard:
                AudioSource.PlayClipAtPoint(_FXClip[1], transform.position, 1.0f);
                break;
            case ItemTypes.Item_Invoice:
                AudioSource.PlayClipAtPoint(_FXClip[2], transform.position, 1.0f);
                break;
            case ItemTypes.Item_Jewelry:
                AudioSource.PlayClipAtPoint(_FXClip[3], transform.position, 1.0f);
                break;
            case ItemTypes.Item_KODrops:
                AudioSource.PlayClipAtPoint(_FXClip[4], transform.position, 1.0f);
                break;
            case ItemTypes.Item_Rosin:
                AudioSource.PlayClipAtPoint(_FXClip[5], transform.position, 1.0f);
                break;
            case ItemTypes.Item_Warderobenumber:
                AudioSource.PlayClipAtPoint(_FXClip[6], transform.position, 1.0f);
                break;
            default:
                Debug.Log("Default value");
                break;
        }
        // here we need AudioSource PickUp jingle
        Destroy(this.gameObject);

    }

    



    

}
