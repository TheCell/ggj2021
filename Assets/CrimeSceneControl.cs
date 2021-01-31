using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrimeSceneControl : MonoBehaviour
{

    public Vector3 destination;
    public GameObject player;
    public SpriteRenderer spriteRenderer;
    PlayerInput playerInput;

    private GameObject interactionTarget;
    private GameObject currentlyInteractingWith;
    private HashSet<GameObject> collidingWith;

    public DialogueControl dialogueControl;

    public TMP_Text dialogueOption1;
    public TMP_Text dialogueOption2;
    public TMP_Text dialogueOption3;

    private InputAction moveToAction;


    [SerializeField]
    private AudioClip[] _walkFX;
    [SerializeField]
    private AudioSource _audioSourceComponent;
    private bool _isMoving;


    [SerializeField]
    private float topOfScreenScaling;
    [SerializeField]
    private float topOfScreenYValue;
    [SerializeField]
    private float bottomOfScreenScaling;
    [SerializeField]
    private float bottomOfScreenYValue;

    private float currentScaling;

    public float speedAtUnitScale;


    // Start is called before the first frame update
    void Start()
    {
        //_audioSourceComponent = GetComponent<AudioSource>();
        collidingWith = new HashSet<GameObject>();
        destination = player.transform.position;

        playerInput = GetComponent<PlayerInput>();
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

                moveToAction = Array.Find(actions.ToArray(), ele => ele.name.Equals("MoveTo"));
                if (moveToAction == null)
                {
                    Debug.Log("MoveTo was not found!");
                }
                else
                {
                    moveToAction.started += MoveTo;
                }

            } else
            {
                Debug.Log("The player input map does not exist!");
            }

        } else
        {
            Debug.Log("Did not find a player input in this object!");
        }

        dialogueControl = FindObjectOfType<DialogueControl>(true);
        if (dialogueControl == null)
        {
            Debug.Log("Did not find a dialog control object!");
        }
    }

    private void OnDestroy()
    {
        moveToAction.started -= MoveTo;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isMoving == true)
        {
            if (_audioSourceComponent.isPlaying == false)
            {
                _audioSourceComponent.clip = _walkFX[UnityEngine.Random.Range(0, 3)];
                _audioSourceComponent.Play();
            }
            gameObject.GetComponentInChildren<Animator>().SetBool("Walking", true);
        } else
        {
            gameObject.GetComponentInChildren<Animator>().SetBool("Walking", false);
        }

        float currrentScreenYRatio = (player.transform.position.y - bottomOfScreenYValue) / (topOfScreenYValue - bottomOfScreenYValue);
        currentScaling = (currrentScreenYRatio * (topOfScreenScaling - bottomOfScreenScaling) + bottomOfScreenScaling);

        gameObject.transform.localScale = new Vector3(currentScaling, currentScaling, currentScaling);

    }

    public float actualSpeed;
    // FixedUpdate is called on a fixed timestep
    private void FixedUpdate()
    {
        // Moves the player transform position towards the destination at a given speed
        actualSpeed = currentScaling * speedAtUnitScale;
        player.transform.position = Vector3.MoveTowards(player.transform.position, destination, actualSpeed * Time.fixedDeltaTime);

        _isMoving = Vector3.Distance(player.transform.position, destination) > 0.1f;


    }

    public void MoveTo(InputAction.CallbackContext context)
    {
        // Scoped to handle item pickup, cause I'm lazy as f
        {
            // this will only work for BoxCollider 3d for now. It's too late to get shit done otherwise
            // also you have to move the mouse a tiny bit while pressing left mouse button down
            Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
            mouseScreenPos.z = 0;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPos);

            if (Physics.Raycast(ray, out hit))
            {
                var item = hit.transform.GetComponent<Item>();
                if (item != null)
                {
                    item.PickUp();
                    return; // We don't want to move if we clicked on an item.
                }
            }

        }

        // Scoped to handle dialogues and movement, cause I'm lazy as f
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            RaycastHit2D[] hit2dList = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
            foreach (RaycastHit2D hit2d in hit2dList)
            {
                if (hit2d.collider.gameObject != this.gameObject && hit2d.collider.gameObject.name != "WalkableArea") {
                    interactionTarget = hit2d.collider.gameObject;
                }
            }


            // Decide if the destination should be the point that was clicked, or to a npc's origin

            if (interactionTarget != null && interactionTarget.gameObject.GetComponent<Dialogue>() != null)
            {
                //We've clicked on a character with dialogue: walk towards their character.
                destination = interactionTarget.gameObject.transform.position;
            }
            else
            {
                // We've clicked on somethat that does not have some interaction point, so we check if it's a walkable area and set the destination accordingly

                // We want to move only if the ground is in the collider list
                var walkableAreaHit = Array.Find(hit2dList, hit => hit.collider.gameObject.name == "WalkableArea");
                if (walkableAreaHit.collider != null)
                {
                    destination = mouseWorldPos;
                    // The z value is not useful here, we set to 0
                    destination.z = 0;
                }
            }
            // If the destination is to the left, flip the sprite to the left, and vice versa
            // Depending on the animations we have, this might get replaced
            if (mouseWorldPos.x < player.transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            checkAndExecuteDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidingWith.Add(collision.gameObject);
        checkAndExecuteDialogue();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        collidingWith.Remove(collision.gameObject);
        checkAndExecuteDialogue();
    }

    private bool checkAndExecuteDialogue()
    {
        if (interactionTarget == null)
        {
            return false;
        }
        if (collidingWith.Contains(interactionTarget))
        {
            Dialogue otherDialogue = interactionTarget.GetComponent<Dialogue>();
            if (otherDialogue != null)
            {
                dialogueControl.ActivateDialogue(otherDialogue);
                currentlyInteractingWith = interactionTarget;
                playerInput.SwitchCurrentActionMap("UI");
                destination = player.transform.position;
                interactionTarget = null;
                return true;
            }
        }
        return false;
    }

}
