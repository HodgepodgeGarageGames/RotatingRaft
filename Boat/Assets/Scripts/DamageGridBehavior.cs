using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGridBehavior : MonoBehaviour
{
    public GameObject   tileObject;
    public int gridWidth = 16;
    public int gridHeight = 16;
    public float gridSpacing = 0.32f;
    Transform[,] tileGrid;
    // Start is called before the first frame update
    void Start()
    {
        tileGrid = new Transform[gridWidth, gridHeight];
        for (int i=0; i != gridHeight; ++i) {
            for (int j=0; j != gridWidth; ++j) {
                var pos = new Vector3(
                    -gridWidth*gridSpacing/2 + gridSpacing/2 + j*gridSpacing,
                    -gridWidth*gridSpacing/2 + gridSpacing/2 + i*gridSpacing,
                    0
                );
                var obj = Instantiate(tileObject, pos, Quaternion.identity, transform);
                obj.name = $"Damage Tile [{i}, {j}]";
                //checkerboard
                if ((i % 3) != 0 || (j % 3) != 0) {
                    obj.SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
