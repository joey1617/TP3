using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private float weight = int.MaxValue;
    [SerializeField] private Transform parentNode = null;
    [SerializeField] private List<Transform> neighbourNode;
    [SerializeField] private bool walkable = true;

    void Start()
    {
        this.resetNode();
        Name();
    }


    /// Reset les value de la Node.
    public void resetNode()
    {
        weight = int.MaxValue;
        parentNode = null;
    }

    public void Name()
    {
        this.name = "Node";
    }

    /// Met un parent à la Node
    public void setParentNode(Transform node)
    {
        this.parentNode = node;
    }

    /// Met le poids de la Note
    public void setWeight(float value)
    {
        this.weight = value;
    }

    /// Met Wakable à la Node.
    public void setWalkable(bool value)
    {
        this.walkable = value;
    }

    /// Ajouter un voisin à la note.
    public void addNeighbourNode(Transform node)
    {
        this.neighbourNode.Add(node);
    }


    /// Prend la node voisin.
    /// Retourne toutes les ndoes voisins.
    public List<Transform> getNeighbourNode()
    {
        List<Transform> result = this.neighbourNode;
        return result;
    }

    /// Prendre le poids
    public float getWeight()
    {
        float result = this.weight;
        return result;

    }

    /// Prend la node parent
    /// Returne la node parent.
    public Transform getParentNode()
    {
        Transform result = this.parentNode;
        return result;
    }

    /// Regard si la node est Wakable
    public bool isWalkable()
    {
        bool result = walkable;
        return result;
    }
}
