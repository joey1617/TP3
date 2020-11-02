using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laby : MonoBehaviour
{
    GameObject[,] block = new GameObject[5, 5];
    public GameObject CubeCreator;
    int[,] Grid = new int[5, 5];
    bool fini = false;
    int h;
    int w;


    void Start()
    {
        h = 0;
        w = 0;

        for (int i = 0; i < 5; i++)
        {
            for (int h = 0; h < 5; h++)
            {
                if (i == 0 && h == 0)
                {
                    Grid[i, h] = 1;
                    block[i, h] = (GameObject.Instantiate(CubeCreator, new Vector3(i * 2, h * 2, 0), transform.rotation));
                    block[i, h].GetComponent<Renderer>().material.color = Color.cyan;
                }

                else if ((i == 0 && h == 1) || (i == 1 && h == 1) || (i == 3 && h == 1) || (i == 1 && h == 3) || (i == 2 && h == 3) || (i == 3 && h == 3))
                {
                    Grid[i, h] = 2;
                    block[i, h] = (GameObject.Instantiate(CubeCreator, new Vector3(i * 2, h * 2, 0), transform.rotation));
                    block[i, h].GetComponent<Renderer>().material.color = Color.gray;

                }

                else if (i == 1 && h == 4)
                {
                    Grid[i, h] = 3;
                    block[i, h] = (GameObject.Instantiate(CubeCreator, new Vector3(i * 2, h * 2, 0), transform.rotation));
                    block[i, h].GetComponent<Renderer>().material.color = Color.red;
                }

                else
                {
                    Grid[i, h] = 0;

                    block[i, h] = (GameObject.Instantiate(CubeCreator, new Vector3(i * 2, h * 2, 0), transform.rotation));
                    block[i, h].GetComponent<Renderer>().material.color = Color.white;
                }

            }
        }

        StartCoroutine(MyCoroutine());

    }

    // Update is called once per frame
    void Update()
    {
       
        
    }


    public int Check(int h, int w)
    {
       
            if (h <= 4 && w == 4)
            {
                if (h == 1 && w == 4)
                {
                    fini = true;
                }

                else if (Grid[h, w] == 0)
                {                   
                    block[h, w].GetComponent<Renderer>().material.color = Color.white;
                    block[h - 1, w].GetComponent<Renderer>().material.color = Color.cyan;
                    h--; ;

                    Debug.Log(h + " , " + w);

                    return Check(h, w);
                }

                return Check(h, w);
            }

            else
            {
                if (Grid[h, w + 1] == 0)
                {
                    block[h, w].GetComponent<Renderer>().material.color = Color.white;
                    block[h, w + 1].GetComponent<Renderer>().material.color = Color.cyan;
                    w++;

                    Debug.Log(h + " , " + w);

                    return Check(h, w);
                }

                else if (Grid[h + 1, w] == 0)
                {
                    block[h, w].GetComponent<Renderer>().material.color = Color.white;
                    block[h + 1, w].GetComponent<Renderer>().material.color = Color.cyan;
                    h++;

                    Debug.Log(h + " , " + w);

                    return Check(h, w);
                }

                else if (Grid[h - 1, w] == 0)
                {
                    block[h, w].GetComponent<Renderer>().material.color = Color.white;
                    block[h - 1, w].GetComponent<Renderer>().material.color = Color.cyan;
                    h--;

                    Debug.Log(h + " , " + w);

                    return Check(h, w);
                }

                else
                {
                    Debug.Log("Bug?");
                    return Check(h, w);
                }

            }

    }

    IEnumerator MyCoroutine()
    {


        if (fini == true)
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        else
        {
            yield return new WaitForSeconds(1);
            Check(h,w);
        }
    }


}
