using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//ArrayList
using System.Collections;
using System.Linq.Expressions;

//Extension
namespace SailHeCSharpClassLib
{
    using size_t = UInt32;
    using VertexKey = Int32;//顶点的键类型
    using WeightType = Int32;//边的权值类型
    using Vector = ArrayList;
    using EdgesType = LinkedList<IndexEdge>;

    //顶点数据类型
    public class VertexValue
    {
        //VertexKey ID;
        char data = '0';
    };
    //权边(没有出发点信息的带权值的边) 排序时按照权值排序 等权值者按目标点id排序
    public class IndexEdge : IComparable
    {
        public VertexKey targetID = (VertexKey)int.MaxValue;//边的对象顶点ID
        public int weight = -1;//不存在负值称长度 存在负值称权值
        public IndexEdge(VertexKey targetID, int weight)
        {
            this.weight = weight;
            this.targetID = targetID;
        }
        //可用于: Dijkstra的优先队列优化 跳表的实现
        public static bool operator <(IndexEdge lhs, IndexEdge rhs)
        {
            return lhs.weight == rhs.weight ? lhs.targetID < rhs.targetID
                : lhs.weight < rhs.weight;
        }
        public static bool operator >(IndexEdge lhs, IndexEdge rhs)
        {
            return lhs.weight == rhs.weight ? lhs.targetID > rhs.targetID
                : lhs.weight > rhs.weight;
        }
        //为了便于list的最短路径计算时的初始化
        // user-defined conversion from Fraction to double
        public static implicit operator int(IndexEdge rhs)
        {
            return (int)rhs.weight;
        }

        override
        public string ToString()
        {
            return "->" + this.targetID + "; ";
        }

        int IComparable.CompareTo(object obj)
        {
            IndexEdge lhs = this;
            IndexEdge rhs = (IndexEdge)obj;
            /*
             * return lhs.weight == rhs.weight ? lhs.targetID - rhs.targetID
                : lhs.weight - rhs.weight;
             */
            return lhs.weight - rhs.weight;
        }
    }
    /*有向边(边<=>直接关系)的定义  tips: ownerID targetID有(Relation)关系应该称作ownerID targetID连通*/
    public class Edge : IndexEdge
    {
        public VertexKey ownerID;//边的拥有者ID
        public Edge(VertexKey ownerID, VertexKey targetID, WeightType weight) : base(targetID, weight)
        {
            this.ownerID = ownerID;
        }
        public Edge(ref Edge rhs) : this(rhs.ownerID, rhs.targetID, rhs.weight) { }
    };/*有向边<ownerID, targetID>*/

    public class PriorityQueue<T>
    {
        IComparer<T> comparer;
        T[] heap;

        public int Count { get; private set; }

        public PriorityQueue() : this(null) { }
        public PriorityQueue(int capacity) : this(capacity, null) { }
        public PriorityQueue(IComparer<T> comparer) : this(16, comparer) { }

        public PriorityQueue(int capacity, IComparer<T> comparer)
        {
            this.comparer = (comparer == null) ? Comparer<T>.Default : comparer;
            this.heap = new T[capacity];
        }

        public void Push(T v)
        {
            if (Count >= heap.Length) Array.Resize(ref heap, Count * 2);
            heap[Count] = v;
            SiftUp(Count++);
        }

        public T Pop()
        {
            var v = Top();
            heap[0] = heap[--Count];
            if (Count > 0) SiftDown(0);
            return v;
        }

        public T Top()
        {
            if (Count > 0) return heap[0];
            throw new InvalidOperationException("优先队列为空");
        }

        void SiftUp(int n)
        {
            var v = heap[n];
            for (var n2 = n / 2; n > 0 && comparer.Compare(v, heap[n2]) > 0; n = n2, n2 /= 2)
                heap[n] = heap[n2];
            heap[n] = v;
        }

        void SiftDown(int n)
        {
            var v = heap[n];
            for (var n2 = n * 2; n2 < Count; n = n2, n2 *= 2)
            {
                if (n2 + 1 < Count && comparer.Compare(heap[n2 + 1], heap[n2]) > 0) n2++;
                if (comparer.Compare(v, heap[n2]) >= 0) break;
                heap[n] = heap[n2];
            }
            heap[n] = v;
        }
    }

    public class Graph
    {
        int vertexNum;
        //邻接表(链表解决冲突的Hash表)
        List<EdgesType> edgeData;

        public Graph(int vertexNum)
        {
            this.vertexNum = vertexNum;
            initList(out edgeData, vertexNum, () => new EdgesType());
        }

        public static void initList(out Vector list, int cap, Object initEle)
        {
            list = new Vector();
            for (int i = 0; i < cap; ++i)
            {
                list.Add(initEle);
            }
        }
        public static void initList<T>(out List<T> list, int cap, Func<T> initFun)
        {
            list = new List<T>();
            for (int i = 0; i < cap; ++i)
            {
                list.Add(initFun());
            }
        }
        public static void initList<T>(out List<T> list, int cap, T initEle)
        {
            list = new List<T>();
            for (int i = 0; i < cap; ++i)
            {
                list.Add(initEle);
            }
        }

        public static IndexEdge listFind(EdgesType edges, VertexKey key)
        {
            foreach (IndexEdge ele in edges)
            {
                if (ele.targetID == key)
                {
                    return ele;
                }
            }
            return null;
        }

        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        //插入 无向边
        public void insertEdgeUndirected(Edge e)
        {
            insertEdge(new Edge(ref e));
            Swap(ref e.ownerID, ref e.targetID);
            insertEdge(new Edge(ref e));
            Swap(ref e.ownerID, ref e.targetID);
        }
        public void insertEdge(Edge e)
        {
            IndexEdge it = listFind(edgeData[e.ownerID], e.targetID);
            if (it == null)
            {
                edgeData[e.ownerID].AddLast(e);
            }
            else
            {
                //已存在: 边数不增加 直接更新就行
                it.weight = e.weight;
            }
        }
        public void deleteEdge(VertexKey ownerID, VertexKey targetID)
        {
            IndexEdge it = listFind(edgeData[ownerID], targetID);
            if (it == null)
            {
                //do nothig
            }
            else
            {
                edgeData[ownerID].Remove(it);
            }
        }
        public LinkedList<IndexEdge> getEdgeList(VertexKey v)
        {
            return edgeData[v];
        }

        //输出所有边
        public void print()
        {
            for (int i = 0; i < 10; ++i)
            {
                int prCnt = 0;
                foreach (IndexEdge indexEdge in this.getEdgeList(i))
                {
                    ++prCnt;
                    Console.Write(i.ToString() + indexEdge.ToString());
                }
                Console.Write(prCnt == 0 ? "" : Environment.NewLine);
            }
        }

        public bool Dijkstra(VertexKey origin, out List<WeightType> dist, out List<VertexKey> predecessor)
        {
            //最短距离估计dist优先队列 每个顶点入队一次 松弛一次
            //static priority_queue<IndexEdge> q;
            //C# 不允许使用 static 修饰符来声明方法内部的变量。SortedSet
            PriorityQueue<IndexEdge> q = new PriorityQueue<IndexEdge>();
            VertexKey v = -1;
            //dist.assign(vertexNum, INF);
            initList(out dist, vertexNum, int.MaxValue);
            //predecessor.assign(vertexNum, -1);
            initList(out predecessor, vertexNum, -1);
            dist[origin] = 0;
            //q.push({ origin, dist[origin] });//{targetID, weight}
            q.Push(new IndexEdge(origin, dist[origin]));

            //核心算法
            while (q.Count != 0)
            {
                //v = q.top().targetID; q.pop();
                v = q.Pop().targetID;
                LinkedList<IndexEdge> edges = getEdgeList(v);
                //对图中由v出发的每条边<v, w>的顶点w进行拓展松弛操作
                foreach (IndexEdge element in edges)
                {
                    //松弛操作: 先松弛过的顶点不会被后松弛的优化 若还能只可能是负权边的情况
                    if (dist[v] + element.weight < dist[element.targetID])
                    {
                        if (element.weight < 0)
                            return false;//错误:有负权边
                        dist[element.targetID] = dist[v] + element.weight;//更新w的最短路径距离估计
                        predecessor[element.targetID] = v;//更新w的最短路径前驱结点
                        q.Push(new IndexEdge(element.targetID, dist[element.targetID]));
                    }
                }
            }
            return true;
        }
    }
}
