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

        public void Enqueue(Vector2 item) //큐 넣기
        {
            _queue.Enqueue(item);
        }

        public Vector2 Dequeue() //큐 빼기 (처음으로 들어간 데이터가 나옴)
        {
            return _queue.Dequeue();
        }

        public Vector2 Peek() //맨 앞에 데이터 가져오기
        {
            return _queue.Peek();
        }

        public Vector2[] Array() //큐 배열로 가져오기
        {
            return _queue.ToArray();
        }   

        public int Count //큐 길이 가져오기
        {
            get { return _queue.Count; }
        }
    }
}
