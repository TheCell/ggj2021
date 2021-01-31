using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseGraphicLogic : MonoBehaviour
{

    public Texture2D pointTexture;
    public Vector2 pointHotSpot = Vector2.zero;
    public Texture2D handTexture;
    public Vector2 handHotSpot = Vector2.zero;
    public Texture2D talkTexture;
    public Vector2 talkHotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    private InputAction _uiDragActionHandle;
    private InputAction _playerDragActionHandle;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnDestroy()
    {
    }

    // Update is called once per frame
    void Update()
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
                if (hit.transform.GetComponent<Item>() != null)
                {
                    Cursor.SetCursor(handTexture, handHotSpot, cursorMode);
                    return;
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
                if (hit2d.collider.gameObject.GetComponent<Dialogue>() != null)
                {
                    Cursor.SetCursor(talkTexture, talkHotSpot, cursorMode);
                    return;
                }
                if (hit2d.collider.gameObject.GetComponent<DragDropItem>() != null)
                {
                    Cursor.SetCursor(handTexture, handHotSpot, cursorMode);
                    return;
                }
            }
        }
        Cursor.SetCursor(pointTexture, pointHotSpot, cursorMode);
    }

    
    
}
