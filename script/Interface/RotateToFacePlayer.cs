using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToFacePlayer : MonoBehaviour
{
    private GameObject cam;
    // Update is called once per frame
    void LateUpdate()
    {
        cam = GameObject.FindWithTag("MainCamera");
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
