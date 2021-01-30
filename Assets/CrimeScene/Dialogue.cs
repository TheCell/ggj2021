using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{

    public string question1;
    public string question2;
    public string question3;

    public string response1;
    public string response2;
    public string response3;

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void speakTo()
    {
        print("Option 1: " + question1);
        print("Option 2: " + question2);
        print("Option 3: " + question3);
    }
}
