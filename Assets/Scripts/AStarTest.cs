using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarTest : MonoBehaviour
{
    //���Ͻǵ�һ���������λ��
    public int beginX = -3;
    public int beginY = 5;
    //֮��ÿһ��������֮���ƫ��λ��
    public int offsetX = 2;
    public int offsetY = 2;
    //��ͼ���ӵĿ��
    public int mapW = 5;
    public int mapH = 5;

    public Material red;
    public Material yellow;
    public Material green;
    public Material white;
    //��ʼ�� ����һ��Ϊ���������
    private Vector2 beginPos = Vector2.right * -1;
    //�յ�
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
                //����������
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = new Vector3(beginX + i * offsetX, beginY + j * offsetY, 0);

                //�õ����� �ж����ǲ����赲
                AStarNode node = AStarManager.Instance.nodes[i, j];
                //��������cube������
                obj.name = node._x + "_" + node._y;
                //��������cube�����ֵ��У��������ʹ��
                cubes.Add(obj.name, obj);
                if (node._type == E_Node_Type.stop)
                {
                    //Debug.Log(node._type.ToString());
                    //�ı����
                    //obj.GetComponent<MeshRenderer>().material = red;
                    //�ı���ʵ���ɫ
                    obj.GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //����������
        if (Input.GetMouseButtonDown(0))
        {
            //�洢������ײ��Ϣ
            RaycastHit raycastHit;
            //�����λ�÷�������
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //���߼��
            if (Physics.Raycast(ray, out raycastHit, 1000))
            {
                //�õ�������������� ����ֱ���ǵڼ��еڼ��е�
                
                //�����ǰû�п�ʼ��
                if (beginPos == Vector2.right * -1)
                {
                    //��·��һ�ε�·�� ���ϴε�·��ȡ��������ʾ
                    if (list != null)
                    {
                        foreach (AStarNode item in list)
                        {
                            cubes[item._x + "_" + item._y].GetComponent<MeshRenderer>().material.color = Color.white;
                        }
                    }

                    //��¼��ʼ��
                    string[] strs = raycastHit.collider.name.Split('_');
                    //beginPos.x = int.Parse(strs[0]);
                    //beginPos.y = int.Parse(strs[1]);
                    beginPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));

                    raycastHit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                }
                else//������� ��Ҫ��¼�յ�Ȼ��ʼѰ·
                {
                    //��¼�յ�
                    string[] strs = raycastHit.collider.name.Split('_');
                    endPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
                    //����Ѱ·
                    list = AStarManager.Instance.FindPath(beginPos, endPos);

                    cubes[(int)beginPos.x + "_" + (int)beginPos.y].GetComponent<MeshRenderer>().material.color = Color.white;
                    //�����Ϊ�� ֤���ҵ���
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            cubes[list[i]._x + "_" + list[i]._y].GetComponent<MeshRenderer>().material.color = Color.green;
                        }
                    }
                    //�����ʼ�� ������ɳ�ʼֵ��
                    beginPos = Vector2.right * -1;
                }
            }
        }
    }
}
