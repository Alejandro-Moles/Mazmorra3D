using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GeneradorMazmorra : MonoBehaviour
{
    public class Celda
    {
        public bool visitada = false;
        public bool[] status = new bool[4]; // 0 -top, 1 - down, 2- left, 3-right
    }

    public Vector2Int size;
    public int posicionInicio = 0;
    public GameObject sala;
    public Vector2 dimension; //separacion entre salas
    public int longitud;


    List<Celda> tablero;
    // Start is called before the first frame update
    void Start()
    {
        GeneradorLaberinto();
    }

    // Update is called once per frame

    void GeneraMazmorra()
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

    void GeneradorLaberinto()
    {
        tablero = new List<Celda>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                tablero.Add(new Celda());
            }
        }

        int celdaActual = posicionInicio;

        Stack<int> camino = new Stack<int>(); // pila

        int num = 0;

        while (num < longitud)
        {
            num++;
            tablero[celdaActual].visitada = true;
            //comprobar celdas vecinas
            List<int> vecinos = CompruebaVecinos(celdaActual);

            //comprobamos si no hay vecinos

            if (vecinos.Count == 0)
            {
                //si no hay mas caminos que probar
                if (camino.Count == 0)
                {
                    break; //salimos
                }
                else //retrocedmos
                {
                    celdaActual = camino.Pop(); // popo saca cosas de la pila
                }
            }
            else // si hay vecinos
            {
                camino.Push(celdaActual);
                int nuevaCelda = vecinos[UnityEngine.Random.Range(0, vecinos.Count)]; //asiganamos la nueva celda de forma aleatoria


                if (nuevaCelda > celdaActual) // vecino derecha o abajo
                {
                    if (nuevaCelda - 1 == celdaActual) //derecha 
                    {
                        tablero[celdaActual].status[3] = true; //abrir puerta derecha
                        celdaActual = nuevaCelda;
                        tablero[celdaActual].status[2] = true; //abrir puerta izquierda de la nueva celda
                    }
                    else // abajo
                    {
                        tablero[celdaActual].status[0] = true; //abrir puerta arriba
                        celdaActual = nuevaCelda;
                        tablero[celdaActual].status[1] = true; //abrir puerta abajo de la nueva celda
                    }
                }
                else // vecino izquierda o arriba
                {
                    if (nuevaCelda + 1 == celdaActual) //izquierda
                    {
                        tablero[celdaActual].status[2] = true; //abrir puerta izquierda
                        celdaActual = nuevaCelda;
                        tablero[celdaActual].status[3] = true; //abrir puerta derecha de la nueva celda
                    }
                    else // arriba
                    {
                        tablero[celdaActual].status[1] = true; //abrir puerta abajo
                        celdaActual = nuevaCelda;
                        tablero[celdaActual].status[0] = true; //abrir puerta arriba de la nueva celda
                    }
                }



            }
        }
        GeneraMazmorra();
    }

    List<int> CompruebaVecinos(int celda) //celda que le mandes te manda los vecinos
    {
        List<int> vecinos = new List<int>();

        if (celda - size.x >= 0 && !tablero[celda - size.x].visitada) // -size.x comprueba arriba
        {
            vecinos.Add(celda - size.x);
        }

        if (celda + size.x < tablero.Count && !tablero[celda + size.x].visitada) // -size.x comprueba abajo
        {
            vecinos.Add(celda + size.x);
        }

        if (celda % size.x != 0 && !tablero[celda - 1].visitada) // -size.x comprueba izquierda
        {
            vecinos.Add(celda - 1);
        }

        if ((celda + 1) % size.x != 0 && !tablero[celda + 1].visitada) // -size.x comprueba derecha
        {
            vecinos.Add(celda + 1);
        }

        return vecinos;
    }
}