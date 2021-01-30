using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    private bool buttonClicked = false;

    public void Start()
    {

    }

    public void Update()
    {

    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        if (!buttonClicked)
        {
            return;
        }

        // this will only work for BoxCollider 3d for now. It's too late to get shit done otherwise
        // also you have to move the mouse a tiny bit while pressing left mouse button down
        Vector3 mousePos = context.ReadValue<Vector2>();
        mousePos.z = 0;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out hit))
        {
            var item = hit.transform.GetComponent<Item>();
            if (item != null)
            {
                item.PickUp();
            }
        }
    }

    public void Click(InputAction.CallbackContext context)
    {
        buttonClicked = !buttonClicked;
    }
}
