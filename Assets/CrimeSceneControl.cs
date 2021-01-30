using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrimeSceneControl : MonoBehaviour
{

    public Vector3 destination;
    public GameObject player;
    public SpriteRenderer spriteRenderer;

    private GameObject interactionTarget;

    // Start is called before the first frame update
    void Start()
    {
        destination = player.transform.position;

        PlayerInput playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            InputActionMap inputActionMap = null;
            var actionMaps = playerInput.actions.actionMaps;
            foreach (var map in actionMaps)
            {
                if (map.name.Equals("Player"))
                {
                    inputActionMap = map;
                }
            }
            if (inputActionMap != null)
            {
                var actions = inputActionMap.actions;

                var action = Array.Find(actions.ToArray(), ele => ele.name.Equals("MoveTo"));
                if (action == null)
                {
                    Debug.Log("MoveTo was not found!");
                }
                else
                {
                    action.started += MoveTo;
                }
                
            } else
            {
                Debug.Log("The player input map does not exist!");
            }

        } else
        {
            Debug.Log("Did not find a player input in this object!");
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate is called on a fixed timestep
    private void FixedUpdate()
    {
        // Moves the player transform position towards the destination at a given speed
        player.transform.position = Vector3.MoveTowards(player.transform.position, destination, 0.1f);
    }

    public void MoveTo(InputAction.CallbackContext context)
    {
       
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        RaycastHit2D hit2d = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit2d.collider != null)
        {
            Debug.Log("You clicked on 2d " + hit2d.collider.gameObject.name);
            interactionTarget = hit2d.collider.gameObject;
        } else
        {
            Debug.Log("The point  " + mousePos2D + " is empty");
     
        }


        // Convert the Mouse current position in the screen to the current position in the world
        destination = mouseWorldPos;
        // The z value is not useful here, we set to 0
        destination.z = 0;

        // If the destination is to the left, flip the sprite to the left, and vice versa
        // Depending on the animations we have, this might get replaced
        if (destination.x < player.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("You collided with " + collision.gameObject.name);

        if (collision.gameObject == interactionTarget) { 
            Dialogue otherDialogue = collision.gameObject.GetComponent<Dialogue>();
            if (otherDialogue != null)
            {
                otherDialogue.speakTo();
            }
        }
    }

}