using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRiver : MonoBehaviour
{
    public float riverLength = 10.0f;
    private List<Vector2> riverCenter = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        riverCenter.Add(Vector2.zero);
        riverCenter.Add(Vector2.right * riverLength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
