using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuRiver : MonoBehaviour
{
    [SerializeField] private float leftEdge = -10.0f;
    [SerializeField] private float bendRange = 3.0f;
    [SerializeField] private float widthRange = 0.5f;
    private LineRenderer top = null;
    private LineRenderer bottom = null;

    // Start is called before the first frame update
    void Start()
    {
        //Line renderer
        GameObject topGO = new GameObject("Top Line");
        topGO.transform.SetParent(transform);
        topGO.transform.localPosition = Vector3.zero;
        topGO.transform.localRotation = Quaternion.identity;
        topGO.transform.localScale = Vector3.one;
        top = topGO.AddComponent<LineRenderer>() as LineRenderer;

        GameObject bottomGO = new GameObject("Bottom Line");
        bottomGO.transform.SetParent(transform);
        bottomGO.transform.localPosition = Vector3.zero;
        bottomGO.transform.localRotation = Quaternion.identity;
        bottomGO.transform.localScale = Vector3.one;
        bottom = bottomGO.AddComponent<LineRenderer>() as LineRenderer;

        top.startWidth = bottom.startWidth = 0.1f;
        top.endWidth = bottom.endWidth = 0.1f;

        top.useWorldSpace = bottom.useWorldSpace = false;

        top.positionCount = bottom.positionCount = 20;

        for (int i = 0; i < 20; ++i)
        {
            top.SetPosition(i, new Vector3(i * 1.0f, 0.0f, 0.0f));
        }

        copy(ref bottom, top);
    }

    public void moveRiver(Vector3 vec)
    {
        transform.position += vec;

        while (transform.position.x <= leftEdge - 1.0f)
        {
            //Line renderer
            transform.position += Vector3.right * 1.0f;

            for (int i = 0; i < 20 - 1; ++i)
            {
                top.SetPosition(i, new Vector3(top.GetPosition(i).x, top.GetPosition(i + 1).y));
            }
            top.SetPosition(20 - 1, new Vector3(20 - 1, top.GetPosition(20 - 2).y + Random.Range(-bendRange, bendRange)));

            copy(ref bottom, top);
        }
    }

    private void copy(ref LineRenderer r, LineRenderer c)
    {
        for (int i = 0; i < 20; ++i)
        {
            r.SetPosition(i, c.GetPosition(i) - Vector3.up);
        }
    }
}
