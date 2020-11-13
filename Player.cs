using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Transform node;
    private Transform startNode;
    private Transform endNode;
    private List<Transform> blockPath = new List<Transform>();

    //Start
    private void Start()
    {
        startNode = null;
        endNode = null;
    }

    // Update is called once per frame
    void Update()
    {
        mouseInput();
        Debug.Log("Start: " + startNode);
        Debug.Log("End: " + endNode);

    }

    /// Enregistrer le click de la souris
    private void mouseInput()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            
            this.colorBlockPath();
            this.updateNodeColor();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.transform.name);               
                Renderer rend;
                if (node != null)
                {
                    rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.white;
                }

                node = hit.transform;

                rend = node.GetComponent<Renderer>();
                rend.material.color = Color.gray;

            }
        }
    }

    /// Case Départ
    public void btnStartNode()
    {
        if (node != null)
        {
            Node n = node.GetComponent<Node>();

            if (n.isWalkable())
            {               
                if (startNode == null)
                {
                    Debug.Log("Commencer.");
                    Renderer rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.cyan;
                    startNode = node;
                }
                else
                {
                    // Reverse the color of the previous node
                    Renderer rend = startNode.GetComponent<Renderer>();
                    rend.material.color = Color.white;

                    // Set the new node as blue.
                    rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.cyan;
                    startNode = node;
                }
                                          
                node = null;
            }
        }
    }

    /// Case de la fin
    public void btnEndNode()
    {
        if (node != null)
        {
            Node n = node.GetComponent<Node>();
           
            if (n.isWalkable())
            {
                if (endNode == null)
                {
                    Renderer rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.red;
                }
                else
                {
                    Renderer rend = endNode.GetComponent<Renderer>();
                    rend.material.color = Color.white;

                    rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.red;
                }

                endNode = node;
                node = null;
            }
        }
    }

    /// Trouver le chemin le plus court
    public void btnFindPath()
    {

        Debug.Log("StartNode Set: " + startNode + "   EndNode Set: " + endNode);
        if (startNode != null && endNode != null)
        {
            Debug.Log("123 trouve moi le");
            PathCourt finder = gameObject.GetComponent<PathCourt>();
            List<Transform> paths = finder.findShortestPath(startNode, endNode);

            foreach (Transform path in paths)
            {
                Renderer rend = path.GetComponent<Renderer>();
                rend.material.color = Color.green;
            }
        }
    }

    /// Placer un mur
    public void btnBlockPath()
    {
        if (node != null)
        {
            Renderer rend = node.GetComponent<Renderer>();
            rend.material.color = Color.black;

            Node n = node.GetComponent<Node>();
            n.setWalkable(false);

            blockPath.Add(node);

            if (node == startNode)
            {
                startNode = null;
            }

            if (node == endNode)
            {
                endNode = null;
            }

            node = null;
        }


        // Séléctionner dans le Grid
        UnitSelectionComponent selection = gameObject.GetComponent<UnitSelectionComponent>();
        List<Transform> selected = selection.getSelectedObjects();

        foreach (Transform nd in selected)
        {
            Renderer rend = nd.GetComponent<Renderer>();
            rend.material.color = Color.black;

            Node n = nd.GetComponent<Node>();
            n.setWalkable(false);

            blockPath.Add(nd);

            if (nd == startNode)
            {
                startNode = null;
            }

            if (nd == endNode)
            {
                endNode = null;
            }
        }

        selection.clearSelections();
    }


    /// Recharger la scène
    public void btnRestart()
    {
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
    }

    /// Colorier les cases qui ne sont pas Walkable
    private void colorBlockPath()
    {
        foreach (Transform block in blockPath)
        {
            Renderer rend = block.GetComponent<Renderer>();
            rend.material.color = Color.black;
        }
    }

    /// Colorier la Node départ et fin
    private void updateNodeColor()
    {
        if (startNode != null)
        {
            Renderer rend = startNode.GetComponent<Renderer>();
            rend.material.color = Color.cyan;
        }


        if (endNode != null)
        {
            Renderer rend = endNode.GetComponent<Renderer>();
            rend.material.color = Color.red;
        }
    }

}
