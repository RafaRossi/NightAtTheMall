using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public List<CustomInput> inputs = new List<CustomInput>();

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.isPaused)
        {

        }
    }
}

[System.Serializable]
public  class CustomInput
{
    public Dictionary<string, string> inputKey = new Dictionary<string, string>();
}
