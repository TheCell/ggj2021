using UnityEngine;

public class Persistence_CollectItemDemo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.transform.name);
        var itemDemo = collision.transform.GetComponent<Persistence_ItemDemo>();
        if (itemDemo != null)
        {
            PersistenceSingleton.Instance.AddNewItem(itemDemo.ItemType);
            //Debug.Log("added to persistence" + itemDemo.ItemType);
            Destroy(collision.transform.gameObject);
        }
    }
}
