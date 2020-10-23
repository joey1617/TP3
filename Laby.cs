using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laby : MonoBehaviour
{

    GameObject[,] block = new GameObject[5,5];
    public GameObject CubeCreator;
    int[,] Grid = new int [5, 5];
    bool continuer = false;


    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int h = 0; h < 5; h++)
            {
                if (i == 0 && h == 0)
                {
                    Grid[i, h] = 1;
                    block[i,h] = (GameObject.Instantiate(CubeCreator, new Vector3(i*2, h*2, 0),transform.rotation));
                    block[i,h].GetComponent<Renderer>().material.color = Color.cyan;
                }

                else if ((i == 0 && h == 1) || (i == 1 && h == 1) || (i == 3 && h == 1) || (i == 1 && h == 3) || (i == 2 && h == 3) || (i == 3 && h == 3))
                {
                    Grid[i, h] = 2;
                    block[i,h] = (GameObject.Instantiate(CubeCreator, new Vector3(i * 2, h * 2, 0), transform.rotation));
                    block[i,h].GetComponent<Renderer>().material.color = Color.gray;

                }

                else if (i == 1 && h == 4)
                {
                    Grid[i, h] = 3;
                    block[i,h] = (GameObject.Instantiate(CubeCreator, new Vector3(i * 2, h * 2, 0), transform.rotation));
                    block[i,h].GetComponent<Renderer>().material.color = Color.red;
                }

                else
                {
                    Grid[i, h] = 0;

                    block[i,h] = (GameObject.Instantiate(CubeCreator, new Vector3(i * 2, h * 2, 0), transform.rotation));
                    block[i,h].GetComponent<Renderer>().material.color = Color.white;
                }
                
            }
        }

        StartCoroutine(ExampleCoroutine());
    }

    // Update is called once per frame
    void Update()
    {       
    }


    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(1);

        while (continuer != true)
        {

            for (int i = 0; i < 5; i++)
            {
                for (int h = 0; h < 5; h++)
                {
                    Color blue = block[i, h].GetComponent<Renderer>().material.color;

                    //Regarde si le cube cyan est sur la case de la fin
                    if (blue == Color.cyan && i == 1 && h == 4)
                    {
                        continuer = true;
                        yield return new WaitForSeconds(1);
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }

                    //Regarde si le joueur est dans le coin de la map vers le bas
                    else if (blue == Color.cyan && h == 4 && i <= 4)
                    {
                        block[i, h].GetComponent<Renderer>().material.color = Color.white;
                        block[i - 1, h].GetComponent<Renderer>().material.color = Color.cyan;
                        Debug.Log(i + ":" + h);
                        yield return new WaitForSeconds(1);
                    }

                    //Deplacement du joueur
                    else if (blue == Color.cyan)
                    {

                        if (block[i, h + 1].GetComponent<Renderer>().material.color == Color.white)
                        {

                            block[i, h].GetComponent<Renderer>().material.color = Color.white;
                            block[i, h + 1].GetComponent<Renderer>().material.color = Color.cyan;
                            Debug.Log(i + ":" + h);
                            yield return new WaitForSeconds(1);
                        }

                        else if (block[i + 1, h].GetComponent<Renderer>().material.color == Color.white)
                        {
                            block[i, h].GetComponent<Renderer>().material.color = Color.white;
                            block[i + 1, h].GetComponent<Renderer>().material.color = Color.cyan;
                            Debug.Log(i + ":" + h);
                            yield return new WaitForSeconds(1);
                        }

                        else if (block[i - 1, h].GetComponent<Renderer>().material.color == Color.white)
                        {
                            block[i, h].GetComponent<Renderer>().material.color = Color.white;
                            block[i - 1, h].GetComponent<Renderer>().material.color = Color.cyan;
                            Debug.Log(i + ":" + h);
                            yield return new WaitForSeconds(1);
                        }                      

                        else
                        {
                            Debug.Log("Erreur");
                        }
                    }

                    else
                    {
                        
                    }

                }

            }
        }
        
    }

   
}
