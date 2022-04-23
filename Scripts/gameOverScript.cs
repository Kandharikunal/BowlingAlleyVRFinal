using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class gameOverScript : MonoBehaviour
{
    public ScoreDB scoredb;
    public TextMeshProUGUI gameOverUIText;
    // Start is called before the first frame update
    void Start()
    {
        performTask();
    }

    // Update is called once per frame
    void Update()
    {
        performTask();
    }

    // string[] ReadData()
    // {
        
    //     string[] lines = System.IO.File.ReadAllLines(@"Assets/database/singleScore.txt");
    //     return lines;
    // }
    void performTask()
    {
        // string[] data = ReadData();
        // string writingData=data[0];
        string writingData=scoredb.lastScore.ToString();
        // data=data[0];
        gameOverUIText.text=writingData;
    }
}
