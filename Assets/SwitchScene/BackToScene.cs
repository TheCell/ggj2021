using UnityEngine;
using UnityEngine.InputSystem;

public class BackToScene : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputActionMap inputActionMap;

    public void Start()
    {
        SetupInputAction();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 mousePos2D = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        RaycastHit2D[] hit2dList = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
        foreach (RaycastHit2D hit2d in hit2dList)
        {
            if (hit2d.collider.gameObject == this.gameObject)
            {
                SwitchToBillboardNew.SwitchToScene();
            }
        }
    }

    private void SetupInputAction()
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
            }
        }
    }
}
