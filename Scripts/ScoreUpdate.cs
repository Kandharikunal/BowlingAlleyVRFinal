using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
public class ScoreUpdate : MonoBehaviour
{

    string fpath;
    // int score = 0;
    // //int cummulitiveScore = 0;
    // GameObject[] pins;
    // GameObject ball1;
    // GameObject ball2;
    // public Text scoreUI;
    // bool flag = true;
    // void Start()
    // {
    //     Debug.Log("Start");
    //     pins = GameObject.FindGameObjectsWithTag("Pins");
    //     //ball1 = GameObject.FindGameObjectWithTag("ball1");
    //     //ball2 = GameObject.FindGameObjectWithTag("ball2");
    //     scoreUI.text = "HI Kunal";
    // }
    // private IEnumerator countPinsDown()
    // {
    //     score = 0;
    //     if (flag)
    //     {
    //         flag = false;
    //         yield return new WaitForSeconds(5);
    //         for (int i = 0; i < pins.Length; i++)
    //         {
    //             if (pins[i].transform.eulerAngles.z > 5 && pins[i].transform.eulerAngles.z < 355)
    //             {
    //                 score++;
    //             }
    //             Debug.Log(score);
    //             scoreUI.text = score.ToString();
    //         }
    //         flag = true;
    //     }
    // }
    // void OnCollisionEnter(Collision col)
    // {
    //     if (col.gameObject.tag == "Pins")
    //     {
    //         StartCoroutine(countPinsDown());            

    //     }
    //     //if (col.gameObject.tag == "ball2")
    //     //{
    //     //    countPinsDown();
    //}
    //}

    public ScoreDB scoredb;
    private Vector3 ballStartPosition;
    GameObject[] pins;
    private Vector3[] pinStartPosition;
    int chance = 0;
    bool flag = true;
    public Text eachScoreUI;
    public Text cummulativeScoreUI;
    int score = 0;
    string allScores = "";
    string cummulativeScore = "";
    public Text totalScoreUI;
    int totalScore = 0;
    int firstChanceScore = 0;
    private AudioSource strike;
    private AudioSource bhaiKyaKarRaha;
    private AudioSource omgWow;
    bool bhaiKyaFlag = false;
    bool strikesoundFlag = true;
    public AudioSource[] sounds;

    
    void Start()
    {
        fpath = Application.persistentDataPath + "/score.txt";
        ballStartPosition = transform.position;
        // Debug.Log(transform.position.y + "Starting");
        pins = GameObject.FindGameObjectsWithTag("Pins");
        // foreach(GameObject pin in pins)
        // {
        //     pinStartPosition.Add(pin.transform.position);
        //     pinsTransform.Add(pin.transform);
        // }
        pinStartPosition = new Vector3[pins.Length];
        for (int i = 0; i < pins.Length; i++)
        {
            pinStartPosition[i] = pins[i].transform.position;
        }
        sounds = GetComponents<AudioSource>();
        strike = sounds[0];
        bhaiKyaKarRaha = sounds[1];
        omgWow = sounds[2];
    }
    //0.85 original 0.2 on alley
    void Update()
    {

        if (flag)
        {
            if (transform.position.y < 0.3f)
            {
                flag = false;
                StartCoroutine(ballThrown());
            }
        }
    }



    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Pins")
        {
            if (strikesoundFlag)
            {
                strikesoundFlag = false;
                strike.Play();
            }

        }
    }
    private IEnumerator ballThrown()
    {
        chance++;
        Debug.Log("Wy");
        yield return new WaitForSeconds(10);
        transform.position = ballStartPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        score = 0;
        for (int i = 0; i < pins.Length; i++)
        {
            if (pins[i].transform.eulerAngles.z > 5 && pins[i].transform.eulerAngles.z < 355)
            {
                score++;
            }
        }


        strikesoundFlag = true;

        if (chance % 2 == 0)
        {
            bhaiKyaFlag = true;
            int secondChanceScore = score - firstChanceScore;
            if (score == 10)
            {
                omgWow.Play();
                allScores += "/ ";
            }
            else
            {
                allScores += secondChanceScore + " ";
            }
            totalScore += score;
            if (totalScore < 10)
            {
                cummulativeScore += "0" + totalScore.ToString() + " ";
            }
            else
            {
                cummulativeScore += totalScore + " ";
            }
            cummulativeScoreUI.text = cummulativeScore;
            resetPins();
        }
        else
        {
            bhaiKyaFlag = true;
            firstChanceScore = score;
            if (firstChanceScore == 10)
            {
                omgWow.Play();
                allScores += "X- ";
                chance++;
                resetPins();
                totalScore += score;
                cummulativeScore += totalScore.ToString() + " ";
                cummulativeScoreUI.text = cummulativeScore;
            }
            else
            {
                allScores += score.ToString();
            }
        }
        if (score == 0 && bhaiKyaFlag == true)
            bhaiKyaKarRaha.Play();
        eachScoreUI.text = allScores;
        Debug.Log(chance.ToString() + " " + score.ToString());
        flag = true;
        strikesoundFlag = true;
        if (chance == 20)
        {
            flag = false;
            totalScoreUI.text = totalScore.ToString();            
            updateDB(totalScore.ToString());
            loadDifferentScene();
        }
    }

    void updateDB(string data){
        // scoredb.LoadDataFromFile();

        using(StreamWriter sw = File.AppendText(fpath))
        {
            sw.WriteLine(data);
        }

        scoredb.hightscores[scoredb.index]=totalScore;
        Debug.Log("Score,updated");
        Debug.Log(scoredb.hightscores[scoredb.index]);
        scoredb.index  = (scoredb.index+1)%10;
        scoredb.lastScore = totalScore;
        // scoredb.SaveToFile();
    }

    void resetPins()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].transform.position = pinStartPosition[i];
            pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            pins[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pins[i].transform.rotation = Quaternion.identity;
        }
    }


    void writeScoreToFile(String fileName)
    {
        // string fileName = @"Assets/database/HighScores.txt";

        if (!File.Exists(fileName))
        {
            using (StreamWriter writer = File.CreateText(fileName))
            {
                writer.WriteLine(totalScore.ToString());
            }
        }
        Debug.Log("writing");

    }
    void resetFile(String fileName)
    {
        // String file = "singleScore.txt"; 

        if (File.Exists(fileName))
            File.Delete(fileName);

        FileStream fs = File.Create(fileName);
    }

    void loadDifferentScene()
    {
        SceneManager.LoadScene(3);
    }
    
}
