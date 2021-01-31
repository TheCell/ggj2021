using System.Collections.Generic;
using UnityEngine;

public class ItemCompareField : MonoBehaviour
{
    public bool allEvidenceCorrect = false;

    [SerializeField]
    private List<ItemTypes> itemTypesToSolve = new List<ItemTypes>();
    private List<DragDropItem> currentEvidence = new List<DragDropItem>();
    private SpriteRenderer spriteRenderer;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var dragDropItem = collision.GetComponent<DragDropItem>();
        if (dragDropItem == null)
        {
            return;
        }

        currentEvidence.Add(dragDropItem);
        CompareEvidence();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        var dragDropItem = collision.GetComponent<DragDropItem>();
        if (dragDropItem == null)
        {
            return;
        }

        currentEvidence.Remove(dragDropItem);
        CompareEvidence();
    }

    private void CompareEvidence()
    {
        var hasAllEvidence = true;

        itemTypesToSolve.ForEach((item) =>
        {

            if (!hasAllEvidence)
            {
                return;
            }

            var itemFound = false;
            currentEvidence.ForEach((evidence) =>
            {
                if (evidence.ItemType == item)
                {
                    itemFound = true;
                }
            });

            hasAllEvidence = itemFound;
        });

        if (hasAllEvidence)
        {
            allEvidenceCorrect = true;
            Debug.Log("The evidence is correct!");
        }
        else
        {
            allEvidenceCorrect = false;
        }

        AdjustSprite(allEvidenceCorrect);
    }

    private void AdjustSprite(bool correct)
    {
        if (spriteRenderer == null)
        {
            return;
        }

        spriteRenderer.color = new Color(1, 1, 1);
        if (correct)
        {
            spriteRenderer.color = new Color(0.8f, 1, 0.8f);
        }
    }
}
