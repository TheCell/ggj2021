using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrimeSceneControl : MonoBehaviour
{

    public Vector3 destination;
    public GameObject player;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        destination = player.transform.position;
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
        if (context.started)
        {

            // Convert the Mouse current position in the screen to the current position in the world
            destination = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            // The z value is not useful here, we set to 0
            destination.z = 0;
            //print("MoveTo called at mouse position " + Mouse.current.position.ReadValue());
            //print("MoveTo called at destination position " + destination);

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
    }

}