using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int row = 5;
    public int column = 5;
    public float padding = 3f;
    public Transform nodePrefab;

    public List<Transform> grid = new List<Transform>();

    void Start()
    {
        this.generateGrid();
        this.generateNeighbours();
    }

    /// Generer le Grid avec les Nodes.
    private void generateGrid()
    {

        int counter = 0;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Transform node = Instantiate(nodePrefab, new Vector3((j * padding) + gameObject.transform.position.x, gameObject.transform.position.y, (i * padding) + gameObject.transform.position.z), Quaternion.identity);
                node.name = "node (" + counter + ")";
                grid.Add(node);
                counter++;
            }
        }


    }

    /// Générer des voisins pour chaque Node.
    private void generateNeighbours()
    {
        for (int i = 0; i < grid.Count; i++)
        {
            Node currentNode = grid[i].GetComponent<Node>();
            int index = i + 1;
           
            if (index % column == 1)
            {              
                if (i + column < column * row)
                {
                    currentNode.addNeighbourNode(grid[i + column]);  
                }

                if (i - column >= 0)
                {
                    currentNode.addNeighbourNode(grid[i - column]);  
                }
                currentNode.addNeighbourNode(grid[i + 1]);    
            }
           
            else if (index % column == 0)
            {             
                if (i + column < column * row)
                {
                    currentNode.addNeighbourNode(grid[i + column]); 
                }

                if (i - column >= 0)
                {
                    currentNode.addNeighbourNode(grid[i - column]); 
                }
                currentNode.addNeighbourNode(grid[i - 1]);   
            }

            else
            {
                if (i + column < column * row)
                {
                    currentNode.addNeighbourNode(grid[i + column]);
                }

                if (i - column >= 0)
                {
                    currentNode.addNeighbourNode(grid[i - column]);
                }
                currentNode.addNeighbourNode(grid[i + 1]); 
                currentNode.addNeighbourNode(grid[i - 1]);
            }

        }
    }
}
