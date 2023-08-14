using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.XR.CoreUtils;
using UnityEngine;

public class AddColor : MonoBehaviour
{
    [SerializeField]
    GameObject colorPreset;
    List<GameObject> colorList = new List<GameObject>();
    List<GameObject> quadList = new List<GameObject>();
    private ColorQueue colQueue = new ColorQueue();
    // Start is called before the first frame update
    void Start()
    {
        colorPreset.GetChildGameObjects(colorList);
        foreach (GameObject Color in colorList)
        {
            if (Color.name == "Color")
            {
                List<GameObject> childList = new List<GameObject>();
                //Color.GetChildGameObjects(childList);
                GameObject quad = Color.GetNamedChild("Quad");
                //GameObject quad = childList[0];
                quadList.Add(quad);
            }
        }
        
        for (int i = quadList.Count - 1; i >= 0; i--)
        {
            colQueue.Enqueue(quadList[i].GetComponent<ColorQuad>().curPoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyColor(ColorQuad cq_preview)
    {
        colQueue.Enqueue(cq_preview.curPoint);
        colQueue.Dequeue();
        Vector2[] colArray = colQueue.Array();
        for (int i = 0; i < quadList.Count; i++)
        {
            quadList[quadList.Count-1-i].GetComponent<ColorQuad>().curPoint = colArray[i];
        }
    }

    public class ColorQueue
    {
        private Queue<Vector2> _queue = new Queue<Vector2>();

        public void Enqueue(Vector2 item) //ť �ֱ�
        {
            _queue.Enqueue(item);
        }

        public Vector2 Dequeue() //ť ���� (ó������ �� �����Ͱ� ����)
        {
            return _queue.Dequeue();
        }

        public Vector2 Peek() //�� �տ� ������ ��������
        {
            return _queue.Peek();
        }

        public Vector2[] Array() //ť �迭�� ��������
        {
            return _queue.ToArray();
        }   

        public int Count //ť ���� ��������
        {
            get { return _queue.Count; }
        }
    }
}
