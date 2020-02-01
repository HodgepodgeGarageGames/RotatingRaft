using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverController : MonoBehaviour
{
    [SerializeField] private Vector2Int size = Vector2Int.zero;
    [SerializeField] private GameObject wallBlueprint = null;
    private GameObject[][] wall;


    // Start is called before the first frame update
    void Start()
    {
        wall = new GameObject[size.x][];
        for (int i = 0; i < size.x; ++i)
        {
            wall[i] = new GameObject[size.y];
            for (int j = 0; j < size.y; ++j)
            {
                wall[i][j] = Instantiate(wallBlueprint, new Vector3 (), new Quaternion ());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
