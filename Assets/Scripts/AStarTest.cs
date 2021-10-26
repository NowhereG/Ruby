using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarTest : MonoBehaviour
{
    //左上角第一个立方体的位置
    public int beginX = -3;
    public int beginY = 5;
    //之后每一个立方体之间的偏移位置
    public int offsetX = 2;
    public int offsetY = 2;
    //地图格子的宽高
    public int mapW = 5;
    public int mapH = 5;

    public Material red;
    public Material yellow;
    public Material green;
    public Material white;
    //开始点 给他一个为负的坐标点
    private Vector2 beginPos = Vector2.right * -1;
    //终点
    private Vector2 endPos;

    private Dictionary<string, GameObject> cubes = new Dictionary<string, GameObject>();

    private List<AStarNode> list = new List<AStarNode>();
    // Start is called before the first frame update
    void Start()
    {
        AStarManager.Instance.InitMapInfo(mapW, mapH);
        for (int i = 0; i < mapW; i++)
        {
            for (int j = 0; j < mapH; j++)
            {
                //创建立方体
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = new Vector3(beginX + i * offsetX, beginY + j * offsetY, 0);

                //得到格子 判断他是不是阻挡
                AStarNode node = AStarManager.Instance.nodes[i, j];
                //给创建的cube重命名
                obj.name = node._x + "_" + node._y;
                //将创建的cube放入字典中，方便后面使用
                cubes.Add(obj.name, obj);
                if (node._type == E_Node_Type.stop)
                {
                    //Debug.Log(node._type.ToString());
                    //改变材质
                    //obj.GetComponent<MeshRenderer>().material = red;
                    //改变材质的颜色
                    obj.GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            //存储射线碰撞信息
            RaycastHit raycastHit;
            //在鼠标位置发射射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //射线检测
            if (Physics.Raycast(ray, out raycastHit, 1000))
            {
                //得到点击到的立方体 才能直到是第几行第几列的
                
                //如果当前没有开始点
                if (beginPos == Vector2.right * -1)
                {
                    //情路上一次的路径 将上次的路径取消高亮显示
                    if (list != null)
                    {
                        foreach (AStarNode item in list)
                        {
                            cubes[item._x + "_" + item._y].GetComponent<MeshRenderer>().material.color = Color.white;
                        }
                    }

                    //记录开始点
                    string[] strs = raycastHit.collider.name.Split('_');
                    //beginPos.x = int.Parse(strs[0]);
                    //beginPos.y = int.Parse(strs[1]);
                    beginPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));

                    raycastHit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                }
                else//有起点了 就要记录终点然后开始寻路
                {
                    //记录终点
                    string[] strs = raycastHit.collider.name.Split('_');
                    endPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
                    //进行寻路
                    list = AStarManager.Instance.FindPath(beginPos, endPos);

                    cubes[(int)beginPos.x + "_" + (int)beginPos.y].GetComponent<MeshRenderer>().material.color = Color.white;
                    //如果不为空 证明找到了
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            cubes[list[i]._x + "_" + list[i]._y].GetComponent<MeshRenderer>().material.color = Color.green;
                        }
                    }
                    //清除开始点 把它变成初始值。
                    beginPos = Vector2.right * -1;
                }
            }
        }
    }
}
