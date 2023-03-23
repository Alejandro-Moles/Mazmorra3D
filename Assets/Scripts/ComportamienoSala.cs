using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamienoSala : MonoBehaviour
{
    public GameObject[] muros; // 0 - top, 1 - down, 2 - left, 3 - rigth
    public GameObject[] puertas; //mismo orden

    
    public bool[] testStatus;

    private void Start()
    {
        UpdateSala(testStatus);
    }
    
    public void UpdateSala(bool[] status)
    {
        for(int i = 0; i< status.Length; i++)
        {
            puertas[i].SetActive(status[i]);
            muros[i].SetActive(!status[i]);
        }
    }
}
