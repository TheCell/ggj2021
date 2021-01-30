using UnityEngine;
using UnityEngine.InputSystem;

public class DragDropItem : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputActionMap inputActionMap;
    private bool clickPressed = false;
    private Transform objectTransformToDrag;

    public void Start()
    {
        // I'm SURE this can be done MUCH better
        playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null)
        {
            var actionMaps = playerInput.actions.actionMaps;
            foreach (var map in actionMaps)
            {
                if (map.name.Equals("UI"))
                {
                    inputActionMap = map;
                }
            }
        }

        if (inputActionMap != null)
        {
            var actions = inputActionMap.actions;
            foreach (var action in actions)
            {
                if (action.name.Equals("Click"))
                {
                    action.performed += OnClick;
                }
                else if (action.name.Equals("Point"))
                {
                    action.performed += OnDrag;
                }
            }
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        objectTransformToDrag = null;
        clickPressed = !clickPressed;
    }

    public void OnDrag(InputAction.CallbackContext context)
    {
        if (!clickPressed)
        {
            return;
        }

        Vector3 mousePos = context.ReadValue<Vector2>();

        if (objectTransformToDrag == null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit))
            {
                objectTransformToDrag = hit.transform;
            }
        }
        else
        {
            var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = objectTransformToDrag.position.z;
            objectTransformToDrag.position = worldPos;
        }
    }
}
