using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class scriptKunal : MonoBehaviour
{
    int score = 0;
    //int cummulitiveScore = 0;
    GameObject[] pins;
    GameObject ball1;
    GameObject ball2;
    //public Text scoreUI;
    void Start()
    {
        pins = GameObject.FindGameObjectsWithTag("Pins");
        ball1 = GameObject.FindGameObjectWithTag("ball1");
        ball2 = GameObject.FindGameObjectWithTag("ball2");
        //scoreUI.text = "HI Kunal";
    }
    void countPinsDown()
    {
        score = 0;
        //sleep for 5 sec
        Thread.Sleep(5000);
        for (int i = 0; i < pins.Length; i++)
        {
            if (pins[i].transform.eulerAngles.z > 5 && pins[i].transform.eulerAngles.z < 355)
            {
                score++;
            }
            Debug.Log(score);
            //scoreUI.text = score.ToString();
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ball1")
        {
            countPinsDown();

        }
        if (col.gameObject.tag == "ball2")
        {
            countPinsDown();
        }
    }

}