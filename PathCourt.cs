using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCourt : MonoBehaviour
{
    public GameObject[] nodes;

    /// Trouver le plus court chemin et retourner une liste
    public List<Transform> findShortestPath(Transform start, Transform end)
    {

        nodes = GameObject.FindGameObjectsWithTag("Node");

        List<Transform> result = new List<Transform>();
        Transform node = DijkstrasAlgo(start, end);

        while (node != null)
        {
            Debug.Log("TROUVER!!");
            result.Add(node);
            Node currentNode = node.GetComponent<Node>();
            node = currentNode.getParentNode();
        }

        result.Reverse();
        return result;
    }

    /// Dijkstra Algorithm
    private Transform DijkstrasAlgo(Transform start, Transform end)
    {
        double startTime = Time.realtimeSinceStartup;

        // Nodes inexplorer
        List<Transform> inexplorer = new List<Transform>();

        foreach (GameObject obj in nodes)
        {
            Node n = obj.GetComponent<Node>();
            if (n.isWalkable())
            {
                n.resetNode();
                inexplorer.Add(obj.transform);
            }
        }

        //Met le poid de la première Node du chemin à 0
        Node startNode = start.GetComponent<Node>();
        startNode.setWeight(0);

        while (inexplorer.Count > 0)
        {
            //Trie tous les chemins du plus petit au plus grand.
            inexplorer.Sort((x, y) => x.GetComponent<Node>().getWeight().CompareTo(y.GetComponent<Node>().getWeight()));

            //On prend la plus petite
            Transform current = inexplorer[0];
       
            inexplorer.Remove(current);

            Node currentNode = current.GetComponent<Node>();
            List<Transform> neighbours = currentNode.getNeighbourNode();
            foreach (Transform neighNode in neighbours)
            {
                Node node = neighNode.GetComponent<Node>();

                if (inexplorer.Contains(neighNode) && node.isWalkable())
                {
                    float distance = Vector3.Distance(neighNode.position, current.position);
                    distance = currentNode.getWeight() + distance;

                    if (distance < node.getWeight())
                    {
                        node.setWeight(distance);
                        node.setParentNode(current);
                    }
                }
            }

        }

        double endTime = (Time.realtimeSinceStartup - startTime);       
        return end;
    }
}
