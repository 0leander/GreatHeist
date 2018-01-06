﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRmovement : MonoBehaviour {

    public Transform vrCam;
    public float toggleAngle = 30.0f;
    public float speed = 3.0f;
    private CharacterController cc;
    public GameObject[] invetory;
    public GameObject[] invetorySlots;
    private int inventoryCount;
    private bool gaze = false;

    public bool moveForward;
    public MovementMethod wayToMove;

    public enum MovementMethod {
        LookWalk,
        Autowalk,
        Teleport
    }

    // Use this for initialization
    void Start() {
        cc = GetComponent<CharacterController>();
        invetory = new GameObject[4];
        inventoryCount = 0;
    }

    public void teleport(GameObject g) {
        this.transform.position = g.transform.position;
    }

    // Update is called once per frame
    void Update() {

        if (wayToMove == MovementMethod.LookWalk)   //Just for the lols
        {
            if (vrCam.eulerAngles.x >= toggleAngle && vrCam.eulerAngles.x <= 90.0f)
            {
                moveForward = true;
            }
            else {
                moveForward = false;
            }

            if (moveForward) {
                Vector3 forward = vrCam.TransformDirection(Vector3.forward);

                cc.SimpleMove(forward * speed);
            }
        }

        if (wayToMove == MovementMethod.Autowalk)
        {
            if (Input.GetButtonDown("Fire1") && !gaze)
            {
                moveForward = !moveForward;
                Debug.Log(gaze);
            }

            if (moveForward)
            {
                Vector3 forward = vrCam.TransformDirection(Vector3.forward);

                cc.SimpleMove(forward * speed);
            }

            
        }
    }

    public void gazeAt() {
        Debug.Log("Gazin");
        gaze = true;
    }

    public void stopGaze() {
        Debug.Log("stop Gazin");
        gaze = false;
    }

    public void PickUp(GameObject item) {
        if (inventoryCount < invetory.Length)
        {
            invetory[inventoryCount] = item;
            invetorySlots[inventoryCount].SetActive(true);
            inventoryCount++;
            item.SetActive(false);

            Debug.Log("stop Gazin");
            gaze = false;
        }
        else {
            Debug.Log("INVENTORY FULL");
        }
    }

    public void DropOff(GameObject car) {
        //Move all Objects to car
        foreach (GameObject item in invetory) {
            item.transform.position = car.transform.position;
            item.SetActive(true);
        }

        //Reset Inventory in Canvas
        foreach (GameObject item in invetorySlots)
        {
            if (item.activeSelf) {
                item.SetActive(false);
            }
        }

        inventoryCount = 0;
    }
}
