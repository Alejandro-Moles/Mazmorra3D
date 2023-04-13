using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamienoSala : MonoBehaviour
{
    public GameObject[] muros, puertas; // 0 -top, 1 - down, 2- left, 3-right
    bool conectada;
    //public bool[] testStatus;


    public void UpdateSala(bool[] status)
    {
        conectada = false;
        for (int i = 0; i < status.Length; i++)
        {
            puertas[i].SetActive(status[i]);
            muros[i].SetActive(!status[i]);
            if (status[i])
            {
                conectada = true;
            }
        }
        if (!conectada)
        {
            Destroy(gameObject);
        }
    }

}