using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private bool updateEveryFrame = false;

    private void Start()
    {
        TiltSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if(updateEveryFrame) TiltSprite();
    }

    private void TiltSprite()
    {
        transform.LookAt(Camera.main.transform);

        transform.rotation = Quaternion.Euler(-transform.rotation.eulerAngles.x, 0f, -transform.rotation.eulerAngles.z);
    }
}
