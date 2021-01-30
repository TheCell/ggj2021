using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _itemSprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DiplayItemOne()
    {
        _itemSprites[0].SetActive(true);
    }

    public void DisplayItemTwo()
    {
        _itemSprites[1].SetActive(true);
    }

    public void DisplayItemThree()
    {
        _itemSprites[2].SetActive(true);
    }

    public void DisplayItemFour()
    {
        _itemSprites[3].SetActive(true);
    }

    public void DisplayItemFive()
    {
        _itemSprites[4].SetActive(true);
    }

    public void DisplayItemSix()
    {
        _itemSprites[5].SetActive(true);
    }

    public void DisplayItemSeven()
    {
        _itemSprites[6].SetActive(true);
    }
}
