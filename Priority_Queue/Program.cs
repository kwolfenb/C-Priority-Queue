using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueueNamespace
{
    public class Node
    {
        public int priority;
        public string value;
        public Node(int priority, string value)
        {
            this.priority = priority;
            this.value = value;
        }
    }
    class PriorityQueue // lower values are higher priority
    {
        List<Node> values;
        PriorityQueue(List<Node> vals)
        {
            this.values = vals;
        }

        void Enqueue(int priority, string val)
        {
            Node node = new Node(priority, val);
            this.values.Add(node);
            int valIdx = this.values.Count - 1;
            int parentIdx = (valIdx - 1) / 2;
            while (this.values[valIdx].priority < this.values[parentIdx].priority)
            {
                this.values[valIdx] = this.values[parentIdx];
                this.values[parentIdx] = node;
                valIdx = parentIdx;
                parentIdx = (valIdx - 1) / 2;
                if (valIdx == 0) break;
            }
        }

        Node Dequeue()
        {
            if (this.values.Count == 0) return null;
            Node max = this.values[0];
            int count = this.values.Count;
            Node last = this.values[count - 1];
            this.values[0] = last;
            this.values.RemoveAt(count - 1);
            count--;
            int idx = 0;
            int leftIdx = 1;
            int rightIdx = 2;
            while (true)
            {
                bool swap = false;
                if (leftIdx >= count) break;
                if (rightIdx >= count)
                {
                    if (last.priority > this.values[leftIdx].priority)
                    {
                        this.values[idx] = this.values[leftIdx];
                        this.values[leftIdx] = last;
                        swap = true;
                    }
                }
                if (leftIdx < count && rightIdx < count)
                {
                    if (this.values[leftIdx].priority < this.values[rightIdx].priority)
                    {
                        if (last.priority > this.values[leftIdx].priority)
                        {
                            this.values[idx] = this.values[leftIdx];
                            this.values[leftIdx] = last;
                            swap = true;
                        }
                    }
                    else if (this.values[rightIdx].priority < this.values[leftIdx].priority)
                    {
                        if (last.priority > this.values[rightIdx].priority)
                        {
                            this.values[idx] = this.values[rightIdx];
                            this.values[rightIdx] = last;
                            swap = true;
                        }
                    }
                }
                idx = this.values.IndexOf(last);
                rightIdx = (idx * 2) + 2;
                leftIdx = (idx * 2) + 1;
                if (!swap) break;
            }
            return max;
        }
        static void Main()
        {
            var node = new Node(1, "critical"); var node1 = new Node(3, "medium"); var node2 = new Node(4, "medium-low"); var node3 = new Node(5, "low"); var node4 = new Node(2, "high");
            List<Node> list = new List<Node> { node, node1, node2, node3 };
            PriorityQueue q = new PriorityQueue(list);

            q.Enqueue(1, "emergency");
            q.Enqueue(3, "normal");
            q.Enqueue(2, "normal");

            foreach (var x in q.values) Console.WriteLine(x.priority + " " + x.value);
            var max = q.Dequeue();
            Console.WriteLine("removed: " + max.value + " " + max.priority);
            foreach (var x in q.values) Console.WriteLine(x.priority + " " + x.value);

        }
    }
}
