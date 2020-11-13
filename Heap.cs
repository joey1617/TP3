using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap : MonoBehaviour
{

    /// Classe de la Node pour la Heap
	public class BinaryNode
    {
        Transform node;

        public BinaryNode(Transform node)
        {
            this.node = node;
        }

        public Transform getNode()
        {
            Transform result = this.node;
            return result;
        }

        public float getWeight()
        {
            Node n = node.GetComponent<Node>();
            float result = n.getWeight();
            return result;
        }
    }

    private List<BinaryNode> heap;

    // Generer la heap
    public void createHeap(Transform node)
    {
        // Generer la lsite de la heap
        heap = new List<BinaryNode>();

        //Ajouter la première node dans la Heap
        heap.Add(new BinaryNode(node));
    }



    /// Insérer les node dans la heap
    public void insert(Transform node)
    {
        BinaryNode bNode = new BinaryNode(node);
        heap.Add(bNode);
        this.bubbleUp(heap.Count - 1);
    }

    /// Extract la node la plus petite
    public Transform extract()
    {
        BinaryNode temp = heap[heap.Count - 1];
        heap[heap.Count - 1] = heap[0];
        heap[0] = temp;

        Transform result = heap[heap.Count - 1].getNode();
        heap.RemoveAt(heap.Count - 1);

        this.heapify(0);

        return result;
    }

    /// Regarde si la Heap est vide.
    public bool isEmpty()
    {
        return heap.Count == 0;
    }

    /// Encapsule la node qui pèse le moins
    private void bubbleUp(int index)
    {

        if (index <= 0)
        {
            return;
        }

        int position = index % 2;

        int parent;
        if (position == 0)
        {
            parent = Mathf.FloorToInt((index / 2) - 1);
        }

        else
        {
            parent = Mathf.FloorToInt((index / 2));
        }

        BinaryNode parentNode = heap[parent];
        BinaryNode node = heap[index];
        if (parentNode.getWeight() > node.getWeight())
        {
            BinaryNode temp = heap[index];
            heap[index] = parentNode;
            heap[parent] = temp;

            this.bubbleUp(parent);

        }

    }


    private void heapify(int index)
    {

        int leftIndex = (2 * index) + 1;
        int rightIndex = (2 * index) + 2;
        int smallest = index;

        if (leftIndex <= heap.Count - 1 && heap[leftIndex].getWeight() <= heap[smallest].getWeight())
        {
            smallest = leftIndex;
        }

        if (rightIndex <= heap.Count - 1 && heap[rightIndex].getWeight() <= heap[smallest].getWeight())
        {
            smallest = rightIndex;
        }

        if (smallest != index)
        {
            BinaryNode temp = heap[index];
            heap[index] = heap[smallest];
            heap[smallest] = temp;

            this.heapify(smallest);
        }
    }

    public void displayHeap()
    {
        int counter = 0;
        foreach (BinaryNode bNode in heap)
        {
            print("index " + counter + " : " + bNode.getNode().name + " (Weight: " + bNode.getWeight() + ")");
            counter++;
        }
    }

    public Transform root()
    {
        Transform result = heap[0].getNode();
        return result;
    }
}
