/*
 *                    KNIGHT IN CASTLE
 *                    Author: Negar Saeidi
 *                    Student number : 101261396
 *                    Date last modified: 12/12/2021
 *                    Rivision history: 1st version :    LOADS THE GAME SCORES IN THE RESULT SCENE
 *                 
 *                    
 *                    
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setResults : MonoBehaviour
{
    public Text time, potion,enemy;
    private int jars,enemyCount;
    string timer;
    void Start()
    {
        if(PlayerPrefs.HasKey("time"))
        {
           timer= PlayerPrefs.GetString("time");
        }

        if (PlayerPrefs.HasKey("jars"))
        {
            jars= PlayerPrefs.GetInt("jars");
        }
        if (PlayerPrefs.HasKey("enemy"))
        {
            enemyCount = PlayerPrefs.GetInt("enemy");
        }


        time.text = timer;
        potion.text = jars.ToString();
        enemy.text = enemyCount.ToString();
    }

  
   
}
