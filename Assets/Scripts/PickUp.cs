using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //RaycastHit hitInfo;
            //Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            //if (Physics.Raycast(rayOrigin, out hitInfo))
            if(hit.collider != null)
            {
                Debug.Log("Hit: " + hit.transform.name);
                var item = hit.transform.GetComponent<Item>();
                if(item != null)
                {
                    Debug.Log(item);
                    item.PickUp();
                }
            }
        }
    }
       
}
