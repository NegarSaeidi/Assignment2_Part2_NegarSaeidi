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