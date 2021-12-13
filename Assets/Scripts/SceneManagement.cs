/*
 *                    KNIGHT IN CASTLE
 *                    Author: Negar Saeidi
 *                    Student number : 101261396
 *                    Date last modified: 12/12/2021
 *                    Rivision history: 1st version : SCENE CONTROLLER, TRANSITION BETWEEN SCENES
 *                 
 *                    
 *                    
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
   public void OnStartButton()
    {
        SceneManager.LoadScene("Main");
    }
    public void OnTutorialButton()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void OnBackButton()
    {
        SceneManager.LoadScene("Start");
    }
    public void OnSLoseButton()
    {
        SceneManager.LoadScene("Result");
    }
    public void OnMenuButton()
    {
        SceneManager.LoadScene("Start");
    }

}
