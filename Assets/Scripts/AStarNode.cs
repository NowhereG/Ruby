using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 格子类型
/// </summary>
public enum E_Node_Type
{
    //可以走的格子
    walk,
    //不可以走的格子
    stop
}
public class AStarNode
{
    //寻路消耗
    public float f;
    //离起点的距离
    public float g;
    //离终点的距离
    public float h;
    //格子坐标
    public int _x;
    public int _y;
    //格子类型
    public E_Node_Type _type;
    public AStarNode father;
    public AStarNode(int x,int y,E_Node_Type type)
    {
        this._x = x;
        this._y = y;
        this._type = type;
    }
}
