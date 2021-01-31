using UnityEngine;
using UnityEngine.InputSystem;

public class DragDropItem : MonoBehaviour
{
    //public ClueTypes clueType = ClueTypes.Item;
    public ItemTypes ItemType
    {
        get { return _itemType; }
        set
        {
            _itemType = value;
            SetImage();
        }
    }

    [SerializeField]
    private ItemTypes _itemType;
    [SerializeField]
    private ItemAndImage[] allItemAndImages;

    private PlayerInput playerInput;
    private InputActionMap inputActionMap;
    private bool clickPressed = false;
    private Transform objectTransformToDrag;
    private SpriteRenderer spriteRenderer;

    public void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        SetupInputAction();
        SetImage();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        objectTransformToDrag = null;
        float click = context.ReadValue<float>();
        clickPressed = click > 0.1f ? true : false;
    }

    public void OnDrag(InputAction.CallbackContext context)
    {
        if (!clickPressed)
        {
            return;
        }

        Vector3 mousePos = context.ReadValue<Vector2>();
        var worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        if (objectTransformToDrag == null)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPos, Vector2.zero);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.GetComponent<DragDropItem>() != null)
                {
                    objectTransformToDrag = hits[i].transform;
                }
            }

            //RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            //if (hit.transform != null && hit.transform.GetComponent<DragDropItem>() != null)
            //{
            //    objectTransformToDrag = hit.transform;
            //}
        }
        else
        {
            worldPos.z = objectTransformToDrag.position.z;
            objectTransformToDrag.position = worldPos;
        }
    }

    private void SetImage()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        bool foundItemImage = false;
        for (int i = 0; i < allItemAndImages.Length; i++)
        {
            var itemAndImage = allItemAndImages[i];
            if (itemAndImage.itemType == _itemType)
            {
                spriteRenderer.sprite = itemAndImage.sprite;
                foundItemImage = true;
            }
        }
        if (!foundItemImage)
        {
            Debug.Log("Did not find a graphic for " + _itemType);
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
                else if (action.name.Equals("Point"))
                {
                    action.performed += OnDrag;
                }
            }
        }
    }
}
