/*
 *                    KNIGHT IN CASTLE
 *                    Author: Negar Saeidi
 *                    Student number : 101261396
 *                    Date last modified: 12/12/2021
 *                    Rivision history: 1st version :   ROTATING PLATFORM BEHAVIOUR, ROTATES THE PLATFORM 0.5F EACH FRAME
 *                 
 *                    
 *                    
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 0.5f));
    }
}
