using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager
{
    //����ģʽ
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

    //��ͼ�Ŀ��
    private int mapW;
    private int mapH;
    //��ͼ������и��ӵ�����
    public AStarNode[,] nodes;
    //�����б�
    private List<AStarNode> openList = new List<AStarNode>();
    //�ر��б�
    private List<AStarNode> closeList = new List<AStarNode>();
    /// <summary>
    /// ��ʼ����ͼ
    /// </summary>
    /// <param name="w"></param>
    /// <param name="h"></param>
    public void InitMapInfo(int w, int h)
    {

        mapW = w;
        mapH = h;
        //������������װ���ٸ�����
        nodes = new AStarNode[w, h];
        //���ݿ�ߴ�������
        //�赲���������ǿ�������赲
        //��Ϊ��������û�е�ͼ��ص�����
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                //�Ժ���������Ŀ�У���Щ������ϢӦ���Ǵӵ�ͼ�����ļ��ж�ȡ�����ģ���Ӧ���������
                nodes[i, j] = new AStarNode(i, j, Random.Range(0, 100) < 20 ? E_Node_Type.stop : E_Node_Type.walk);
            }
        }

    }
    /// <summary>
    /// Ѱ·���� �ṩ���ⲿʹ��
    /// </summary>
    /// <param name="startPos">���</param>
    /// <param name="endPos">�յ�</param>
    /// <returns></returns>
    public List<AStarNode> FindPath(Vector2 startPos, Vector2 endPos)
    {

        //ʵ����Ŀ�� ����ĵ�����������ϵ�е�λ��
        //��������ʡ�Ի���Ĳ��� ֱ����Ϊ���Ǵ������ĸ���

        //�����жϴ���������� �Ƿ�Ϸ�
        //������Ϸ�Ӧ��ֱ�ӷ���null ��ζ�Ų���Ѱ·
        //1.���� Ҫ�ڵ�ͼ��Χ��
        if (startPos.x < 0 || startPos.x >= mapW || startPos.y < 0 || startPos.y >= mapH || endPos.x < 0 || endPos.x >= mapW || endPos.y < 0 || endPos.y >= mapH)
        {
            Debug.Log("��ʼ���߽������ڵ�ͼ��������");
            return null;
        }
        //Ӧ�õõ������յ��Ӧ�ĸ���
        AStarNode start = nodes[(int)startPos.x, (int)startPos.y];
        AStarNode end = nodes[(int)endPos.x, (int)endPos.y];
        //2.Ҫ�����赲
        if (start._type == E_Node_Type.stop || end._type == E_Node_Type.stop)
        {
            Debug.Log("��ʼ���߽��������赲");
            return null;
        }
        //�����һ�ε�������ݣ���������Ӱ����һ�ε�Ѱ·����
        //��ε���Ѱ·�㷨ʱ������openlist��closelist�������·�������ݣ���Ҫ�����
        //��չرպͿ����б�
        openList.Clear();
        closeList.Clear();

        //��ε���Ѱ·�㷨ʱ������start�������·��������Ϣ����Ҫ�����
        start.father = null;
        start.f = 0;
        start.g = 0;
        start.h = 0;
        closeList.Add(start);
        while (true)
        {
            //����㿪ʼ ����Χ�ĵ� �����뿪���б���
            //����    x-1 y-1
            FindNearlyToOpenList(start._x - 1, start._y - 1, 1.4f, start, end);
            //��      x   y-1
            FindNearlyToOpenList(start._x, start._y - 1, 1, start, end);
            //����    x+1 y-1
            FindNearlyToOpenList(start._x + 1, start._y, 1.4f, start, end);
            //��      x-1 y
            FindNearlyToOpenList(start._x - 1, start._y, 1, start, end);
            //��      x+1 y
            FindNearlyToOpenList(start._x + 1, start._y, 1, start, end);
            //����    x-1 y+1
            FindNearlyToOpenList(start._x - 1, start._y + 1, 1.4f, start, end);
            //��      x   y+1
            FindNearlyToOpenList(start._x, start._y + 1, 1, start, end);
            //����    x+1 y+1
            FindNearlyToOpenList(start._x + 1, start._y + 1, 1.4f, start, end);

            //��·���ж� �����б�Ϊ�ն���û���ҵ��յ� ����Ϊ����·
            if (openList.Count == 0)
            {
                Debug.Log("��·");
                return null;
            }
            //ѡ�������б��� Ѱ·������С�ĵ�
            openList.Sort(SortOpenList);
            //��¼����� �����µ���㣬������һ��Ѱ·����
            start = openList[0];
            //����ر��б�Ȼ���ٴӿ����б���ɾ��
            closeList.Add(openList[0]);
            openList.RemoveAt(0);
            //���������Ѿ����յ��� ��ô�õ����ս�����س�ȥ
            if (start == end)
            {
                //������ �ҵ�·�� ����·��
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
            //�������㲻���յ� ��ô����Ѱ·
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
    /// ���ٽ��ĵ���뿪���б���
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void FindNearlyToOpenList(int x, int y, float g, AStarNode father, AStarNode end)
    {

        //�ж���Щ�� �Ƿ��Ǳ߽� �Ƿ����赲 �Ƿ��Ѿ��ڿ����б��ر��б��� ��������� �ŷ��뿪���б�
        if (x < 0 || x >= mapW || y < 0 || y >= mapH)
        {
            return;
        }
        AStarNode node = nodes[x, y];
        if (node == null || node._type == E_Node_Type.stop || openList.Contains(node) || closeList.Contains(node))
        {
            return;
        }

        //��¼������
        node.father = father;
        //����g �����ľ���
        node.g = father.g + g;
        //����h ���յ�ľ���
        node.h = Mathf.Abs(end._x - node._x) + Mathf.Abs(end._y - node._y);
        //����fֵ
        node.f = node.g + node.h;
        //���ͨ������ĺϷ���֤ �ʹ浽�����б���
        openList.Add(node);
    }
}
