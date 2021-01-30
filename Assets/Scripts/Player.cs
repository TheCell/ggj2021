using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _collectedItems = 0;
    private UIManager _comUIManager;

    // Start is called before the first frame update
    void Start()
    {
        _comUIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCollectible(int iD)
    {
        _collectedItems++;
        
        switch (iD)
        {
            case 0:
                _comUIManager.DiplayItemOne();
                break;
            case 1:
                _comUIManager.DisplayItemTwo();
                break;
            case 2:
                _comUIManager.DisplayItemThree();
                break;
            case 3:
                _comUIManager.DisplayItemFour();
                break;
            case 4:
                _comUIManager.DisplayItemFive();
                break;
            case 5:
                _comUIManager.DisplayItemSix();
                break;
            case 6:
                _comUIManager.DisplayItemSeven();
                break;
            default:
                Debug.Log("Default value");
                break;
        }
    }

}
