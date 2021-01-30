using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int _itemID = 0;
    

    public void PickUp()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.AddCollectible(_itemID);
        
        Destroy(this.gameObject);
    }

}
