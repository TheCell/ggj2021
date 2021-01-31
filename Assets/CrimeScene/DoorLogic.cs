using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DoorLogic : MonoBehaviour
{
    [SerializeField]
    private int destinationSceneId;

    private PlayerInput playerInput;
    private InputActionMap inputActionMap;

    private InputAction moveToAction;

    // Start is called before the first frame update
    void Start()
    {
        SetupInputAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        float click = context.ReadValue<float>();
        var clickPressed = click > 0.1f ? true : false;
        if (!clickPressed)
        {
            return;
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        
        RaycastHit2D[] hit2dList = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
        RaycastHit2D doorHit = Array.Find(hit2dList, hit => hit.collider.gameObject == this.gameObject);
        if (doorHit.collider != null)
        {
            goThroughDoor();
        } 

    }

    private void goThroughDoor()
    {
        SceneManager.LoadScene(destinationSceneId);
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
                if (map.name.Equals("Player"))
                {
                    inputActionMap = map;
                }
            }
        } else
        {
            Debug.Log("Did not find object of type PlayerInput!");
        }

        if (inputActionMap != null)
        {
            var actions = inputActionMap.actions;
            foreach (var action in actions)
            {
                if (action.name.Equals("MoveTo"))
                {
                    moveToAction = action;
                    moveToAction.canceled += OnClick;
                }
            }
        } else 
        {
            Debug.Log("Did not find inputActionMap Player!");
        }
    }


    private void OnDestroy()
    {
        moveToAction.canceled -= OnClick;
    }

}
