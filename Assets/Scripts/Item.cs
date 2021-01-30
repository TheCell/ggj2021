using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int _itemID = 0;

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
        // here we need AudioSource PickUp jingle
        Destroy(this.gameObject);

    }

    



    

}
