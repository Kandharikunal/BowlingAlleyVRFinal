using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightScript : MonoBehaviour
{
    public Light[] lights;
    int range;
    // Start is called before the first frame update
    void Start()
    {
        // lights = GetComponents<Light>();

        range = lights.Length;
        Debug.Log(range);
        
    }

    // Update is called once per frame
    // void Update()
    // {

    // }
    int i = 0;
    // int j = 1;
    bool flag=true;
    void Update()
    {
        StartCoroutine(blinkLight());
    }
    System.Random rnd = new System.Random();

    private IEnumerator blinkLight()
    {
        if(flag)
        {
            flag=false;            
            lights[i].enabled=true;
            yield return new WaitForSeconds(0.1f);
            lights[i].enabled=false;
            i=rnd.Next(4);
            i=(i+1)%range;
            flag=true;
        }
        // sleep(3);
    }
}
