/*
 *                    KNIGHT IN CASTLE
 *                    Author: Negar Saeidi
 *                    Student number : 101261396
 *                    Date last modified: 12/12/2021
 *                    Rivision history: 1st version :   CONTROLS THE UI INPUT BUTTONS ON THE SCREEN
 *                 
 *                    
 *                    
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UIController : MonoBehaviour
{
    public static bool jumpButton;
    public static bool attackButton;

    public void OnJumpButtonDown()
    {
        jumpButton = true;
    }
    public void OnJumpButtonUp()
    {
        jumpButton = false;
    }

    public void OnAttackButtonDown()
    {
        attackButton = true;
    }
    public void OnAttackButtonUp()
    {
        attackButton = false;
    }
}