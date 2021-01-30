using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _itemSprites;

    [SerializeField]
    private AudioClip[] _FXSounds;
    
 

    public void DiplayItemOne()
    {
        _itemSprites[0].SetActive(true);
        _itemSprites[0].GetComponent<AudioSource>().Play();
    }

    public void DisplayItemTwo()
    {
        _itemSprites[1].SetActive(true);
        _itemSprites[1].GetComponent<AudioSource>().Play();
    }

    public void DisplayItemThree()
    {
        _itemSprites[2].SetActive(true);
        _itemSprites[2].GetComponent<AudioSource>().Play();
    }

    public void DisplayItemFour()
    {
        _itemSprites[3].SetActive(true);
        _itemSprites[3].GetComponent<AudioSource>().Play();
    }

    public void DisplayItemFive()
    {
        _itemSprites[4].SetActive(true);
        _itemSprites[4].GetComponent<AudioSource>().Play();
    }

    public void DisplayItemSix()
    {
        _itemSprites[5].SetActive(true);
        _itemSprites[5].GetComponent<AudioSource>().Play();
    }

    public void DisplayItemSeven()
    {
        _itemSprites[6].SetActive(true);
        _itemSprites[6].GetComponent<AudioSource>().Play();
    }
}
