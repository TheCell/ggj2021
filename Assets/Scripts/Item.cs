using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemTypes itemType;

    [SerializeField]
    private AudioClip _FXClip; 

    void Start()
    {
        if (PersistenceSingleton.Instance.FoundItems.Contains(itemType))
        {
            Destroy(this.gameObject);
        }
    }

    public void PickUp()
    {
        Player player = GameObject.Find("Detective").GetComponent<Player>();
        player.AddCollectible(itemType);
        PersistenceSingleton.Instance.AddNewItem(itemType);

        AudioSource.PlayClipAtPoint(_FXClip, transform.position, 1.0f);

        Destroy(this.gameObject);
    }

}
