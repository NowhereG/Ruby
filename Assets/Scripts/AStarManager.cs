using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager
{
    //单例模式
    private static AStarManager _instance;
    public static AStarManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AStarManager();
            }
            return _instance;
        }
    }

    //地图的宽高
    private int mapW;
    private int mapH;
    //地图相关所有格子的容器
    public AStarNode[,] nodes;
    //开启列表
    private List<AStarNode> openList = new List<AStarNode>();
    //关闭列表
    private List<AStarNode> closeList = new List<AStarNode>();
    /// <summary>
    /// 初始化地图
    /// </summary>
    /// <param name="w"></param>
    /// <param name="h"></param>
    public void InitMapInfo(int w, int h)
    {

        mapW = w;
        mapH = h;
        //声明容器可以装多少个格子
        nodes = new AStarNode[w, h];
        //根据宽高创建格子
        //阻挡的问题我们可以随机阻挡
        //因为我们现在没有地图相关的数据
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                //以后真正的项目中，这些主档信息应该是从地图配置文件中读取出来的，不应该是随机的
                nodes[i, j] = new AStarNode(i, j, Random.Range(0, 100) < 20 ? E_Node_Type.stop : E_Node_Type.walk);
            }
        }

    }
    /// <summary>
    /// 寻路方法 提供给外部使用
    /// </summary>
    /// <param name="startPos">起点</param>
    /// <param name="endPos">终点</param>
    /// <returns></returns>
    public List<AStarNode> FindPath(Vector2 startPos, Vector2 endPos)
    {

        //实际项目中 传入的点往往是坐标系中的位置
        //我们这里省略换算的不走 直接认为他是传进来的格子

        //首先判断传入的两个点 是否合法
        //如果不合法应该直接返回null 意味着不能寻路
        //1.首先 要在地图范围内
        if (startPos.x < 0 || startPos.x >= mapW || startPos.y < 0 || startPos.y >= mapH || endPos.x < 0 || endPos.x >= mapW || endPos.y < 0 || endPos.y >= mapH)
        {
            Debug.Log("开始或者结束点在地图格子外面");
            return null;
        }
        //应该得到起点和终点对应的格子
        AStarNode start = nodes[(int)startPos.x, (int)startPos.y];
        AStarNode end = nodes[(int)endPos.x, (int)endPos.y];
        //2.要不是阻挡
        if (start._type == E_Node_Type.stop || end._type == E_Node_Type.stop)
        {
            Debug.Log("开始或者结束点是阻挡");
            return null;
        }
        //清空上一次的相关数据，避免他们影响这一次的寻路计算
        //多次调用寻路算法时，可能openlist和closelist存放其他路径的数据，需要先清空
        //清空关闭和开启列表
        openList.Clear();
        closeList.Clear();

        //多次调用寻路算法时，可能start存放其他路径起点的信息，需要先清空
        start.father = null;
        start.f = 0;
        start.g = 0;
        start.h = 0;
        closeList.Add(start);
        while (true)
        {
            //从起点开始 找周围的点 并放入开启列表中
            //左上    x-1 y-1
            FindNearlyToOpenList(start._x - 1, start._y - 1, 1.4f, start, end);
            //上      x   y-1
            FindNearlyToOpenList(start._x, start._y - 1, 1, start, end);
            //右上    x+1 y-1
            FindNearlyToOpenList(start._x + 1, start._y, 1.4f, start, end);
            //左      x-1 y
            FindNearlyToOpenList(start._x - 1, start._y, 1, start, end);
            //右      x+1 y
            FindNearlyToOpenList(start._x + 1, start._y, 1, start, end);
            //左下    x-1 y+1
            FindNearlyToOpenList(start._x - 1, start._y + 1, 1.4f, start, end);
            //下      x   y+1
            FindNearlyToOpenList(start._x, start._y + 1, 1, start, end);
            //右下    x+1 y+1
            FindNearlyToOpenList(start._x + 1, start._y + 1, 1.4f, start, end);

            //死路的判断 开启列表为空都还没有找的终点 就认为是死路
            if (openList.Count == 0)
            {
                Debug.Log("死路");
                return null;
            }
            //选出开启列表中 寻路消耗最小的点
            openList.Sort(SortOpenList);
            //记录这个点 他是新的起点，进行下一次寻路计算
            start = openList[0];
            //放入关闭列表，然后再从开启列表中删除
            closeList.Add(openList[0]);
            openList.RemoveAt(0);
            //如果这个点已经是终点了 那么得到最终结果返回出去
            if (start == end)
            {
                //找完了 找到路径 返回路径
                List<AStarNode> path = new List<AStarNode>();
                path.Add(end);
                while (end.father != null)
                {
                    path.Add(end.father);
                    end = end.father;
                }
                path.Reverse();
                return path;
            }
            //如果这个点不是终点 那么继续寻路
        }
    }
    private int SortOpenList(AStarNode a, AStarNode b)
    {
        if (a.f >= b.f)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
    /// <summary>
    /// 把临近的点放入开启列表中
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void FindNearlyToOpenList(int x, int y, float g, AStarNode father, AStarNode end)
    {

        //判断这些点 是否是边界 是否是阻挡 是否已经在开启列表或关闭列表中 如果都不是 才放入开启列表
        if (x < 0 || x >= mapW || y < 0 || y >= mapH)
        {
            return;
        }
        AStarNode node = nodes[x, y];
        if (node == null || node._type == E_Node_Type.stop || openList.Contains(node) || closeList.Contains(node))
        {
            return;
        }

        //记录父对象
        node.father = father;
        //计算g 离起点的距离
        node.g = father.g + g;
        //计算h 离终点的距离
        node.h = Mathf.Abs(end._x - node._x) + Mathf.Abs(end._y - node._y);
        //计算f值
        node.f = node.g + node.h;
        //如果通过上面的合法验证 就存到开启列表中
        openList.Add(node);
    }
}
