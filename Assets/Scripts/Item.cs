using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int _itemID = 0;
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
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.AddCollectible(_itemID);

        switch (_itemID)
        {
            case 0:
                AudioSource.PlayClipAtPoint(_FXClip[0], transform.position, 1.0f);
                break;
            case 1:
                AudioSource.PlayClipAtPoint(_FXClip[1], transform.position, 1.0f);
                break;
            case 2:
                AudioSource.PlayClipAtPoint(_FXClip[2], transform.position, 1.0f);
                break;
            case 3:
                AudioSource.PlayClipAtPoint(_FXClip[3], transform.position, 1.0f);
                break;
            case 4:
                AudioSource.PlayClipAtPoint(_FXClip[4], transform.position, 1.0f);
                break;
            case 5:
                AudioSource.PlayClipAtPoint(_FXClip[5], transform.position, 1.0f);
                break;
            case 6:
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
