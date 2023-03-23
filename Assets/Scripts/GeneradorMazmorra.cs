using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GeneradorMazmorra : MonoBehaviour
{
  
    public class Celda
    {
        public bool visitada = false;
        public bool[] status = new bool[4]; 
    }

    public Vector2Int size;
    public int posicionInicio = 0;
    public GameObject sala;
    public Vector2 dimension;
    List<Celda> tablero;



    private void Start()
    {
        GeneradorLaberinto();
    }


    void GenerarMazmorra()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                var nuevaSala = Instantiate(sala, new Vector3(i * dimension.x, 0, j * dimension.y), Quaternion.identity, transform).GetComponent<ComportamienoSala>();
                nuevaSala.UpdateSala(tablero[i + j * size.x].status);
            }
        }
    }

    //Metodo Generador de laberinto
    void GeneradorLaberinto()
    {
        tablero = new List<Celda>();

        for(int i = 0;i < size.x; i++)
        {
            for(int j = 0;j<size.y; j++)
            {
                tablero.Add(new Celda());
            }
        }

        int celdaActual = posicionInicio;

        Stack<int> camino = new Stack<int>();

        int num = 0;

        while(num < 100)
        {
            num++;
            tablero[celdaActual].visitada = true;

            //comprobar celdas vecinas
            List<int> vecinos = CompruebaVecinos(celdaActual);

            #region No hay Casillas Alrededor
            //comprobamos si no hay casillas alrededor
            if (vecinos.Count <= 0)
            {
                //si no hay mas camino que recorrer
                if(camino.Count <= 0)
                {
                    break; //Salimos
                }
                //retrocedemos en el camino
                else
                {
                    celdaActual = camino.Pop();
                }
            }
            #endregion

            #region Si hay Casillas Alrededor
            //si hay casillas alrededor
            if (vecinos.Count > 0)
            {
                camino.Push(celdaActual); //añadimos la celda al camino

                int nuevaCelda = vecinos[Random.Range(0,vecinos.Count)];//Asignamos la nueva aleatoriamente

                #region Puertas de abajo y derecha
                //derecha o abajo
                if (nuevaCelda > celdaActual)
                {
                    if(nuevaCelda -1 == celdaActual) //derecha
                    {
                        tablero[celdaActual].status[3] = true; //colocamos la puerta derecha
                        celdaActual = nuevaCelda;
                        tablero[celdaActual].status[2] = true; //colocamos la puerta izquierda en la nueva celda
                    }

                    else //vecino de abajo
                    {
                        tablero[celdaActual].status[1] = true; //colocamos la puerta abajo
                        celdaActual = nuevaCelda;
                        tablero[celdaActual].status[0] = true; //colocamos la puerta arriba en la nueva celda
                    }
                }
                #endregion

                #region Puertas de arriba e izquierda
                else //vecino de arriba o izquierda
                {
                    if (nuevaCelda + 1 == celdaActual) //izquierda
                    {
                        tablero[celdaActual].status[2] = true; //colocamos la puerta izquierda
                        celdaActual = nuevaCelda;
                        tablero[celdaActual].status[3] = true; //colocamos la puerta derecha en la nueva celda
                    }

                    else //vecino de arriba
                    {
                        tablero[celdaActual].status[0] = true; //colocamos la puerta arriba
                        celdaActual = nuevaCelda;
                        tablero[celdaActual].status[1] = true; //colocamos la puerta abajo en la nueva celda
                    }
                }
                #endregion
            }
            #endregion
        }
        GenerarMazmorra();
    }

    List<int> CompruebaVecinos(int celda)
    {
        List<int> vecinos = new List<int>();

        //comprobamos vecino de arriba
        if(celda - size.x >= 0 && !tablero[celda - size.x].visitada)
        {
            vecinos.Add(celda - size.x);
        }

        //comprobamos el de abajo
        if(celda + size.x < tablero.Count && !tablero[celda + size.x].visitada)
        {
            vecinos.Add(celda + size.x);
        }

        //comprobamos el de la izquierda
        if (celda % size.x != 0 && !tablero[celda - 1].visitada)
        {
            vecinos.Add(celda % size.x);
        }

        //comprobamos el de la derecha
        if ((celda+1) % size.x != 0 && !tablero[celda + 1].visitada)
        {
            vecinos.Add((celda + 1) % size.x);
        }

        return vecinos;
    }
}
