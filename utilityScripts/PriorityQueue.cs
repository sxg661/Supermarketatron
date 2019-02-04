using UnityEngine;
using UnityEditor;

public class PriorityQueue<T>
{
    int queueSize;

    Index[] heap;

    /// <summary>
    /// initialises prioty queue
    /// </summary>
    /// <param name="queueSize">length of queue</param>
    public PriorityQueue(int queueSize){
        this.queueSize = queueSize;
        heap = new Index[queueSize];
    }

    /// <summary>
    /// stores the data and priority of a queue index
    /// </summary>
    private struct Index
    {
        public int priority;
        public T data;
    }

    int maxOccupiedIndex = -1;

    private int getAboveNode(int index)
    {
        return int.Parse( (Mathf.Ceil((index / 2f) - 1)).ToString() );
    }

    private int[] getBelowNodes(int index)
    {
        return new int[] { ((index + 1) * 2) - 1, (index + 1) * 2 };
    }

    /// <summary>
    /// adds the item to the appropriate point of the priority queue
    /// : lower number = higher priority
    /// </summary>
    /// <param name="data">the data to add</param>
    /// <param name="priority">priority of item to add</param>
    public void add(T data, int priority)
    {
        Index newIndex = new Index();
        newIndex.priority = priority;
        newIndex.data = data;
        
        if (++maxOccupiedIndex > (queueSize - 1))
        {
            Debug.LogError("search priority queue size limit cannot be exceeded exceeded");
            return;
        }

        heap[maxOccupiedIndex] = newIndex;
        upwardHeapify(maxOccupiedIndex);

    }

    public bool hasElements()
    {
        return maxOccupiedIndex > -1;
    }
    
    public T take()
    {
        Index returnItem = heap[0];

        //Debug.Log(maxOccupiedIndex);
        heap[0] = heap[maxOccupiedIndex--];

        if (maxOccupiedIndex != -1)
            downwardHeapify(0);

        return returnItem.data;
    }

    public int TopPriority()
    {
        if (maxOccupiedIndex == -1)
        {
            return -1;
        }

        return heap[maxOccupiedIndex].priority;
    }

    private void upwardHeapify(int indexToShift)
    {
        if (indexToShift == 0)
            return;

        int parent = getAboveNode(indexToShift);

        if(heap[indexToShift].priority < heap[parent].priority)
        {
            Index t = heap[parent];
            heap[parent] = heap[indexToShift];
            //Debug.Log("swapping " + t.priority + " with " + heap[indexToShift].priority);
            heap[indexToShift] = t;
            upwardHeapify(parent);
        }
    }

    private void downwardHeapify(int indexToShift)
    {
        if (indexToShift == maxOccupiedIndex)
            return;

        int[] children = getBelowNodes(indexToShift);

        int child = 0;

        while(child < children.Length && children[child] <= maxOccupiedIndex)
        {
            if(heap[indexToShift].priority > heap[child].priority)
            {
                //Debug.Log(heap[indexToShift].priority + " " + heap[child].priority);
                Index t = heap[children[child]];
                heap[children[child]] = heap[indexToShift];
                heap[indexToShift] = t;
                downwardHeapify(children[child]);
                return;
            }
            child++;
        }
    }

    public void display()
    {
        for (int i = 0; i <= maxOccupiedIndex; i++)
        {
            Debug.Log("item " + i + ": " + heap[i].priority);
        }
    }




}