using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��������
/// </summary>
public enum E_Node_Type
{
    //�����ߵĸ���
    walk,
    //�������ߵĸ���
    stop
}
public class AStarNode
{
    //Ѱ·����
    public float f;
    //�����ľ���
    public float g;
    //���յ�ľ���
    public float h;
    //��������
    public int _x;
    public int _y;
    //��������
    public E_Node_Type _type;
    public AStarNode father;
    public AStarNode(int x,int y,E_Node_Type type)
    {
        this._x = x;
        this._y = y;
        this._type = type;
    }
}
