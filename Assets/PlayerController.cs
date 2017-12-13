using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform mainCameraTransform;
	// Use this for initialization
	void Start ()
    {
        mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    
}
