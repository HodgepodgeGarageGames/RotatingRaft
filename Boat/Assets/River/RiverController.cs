using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverController : MonoBehaviour
{
    [SerializeField] private Vector2Int size = Vector2Int.zero;
    [SerializeField] private GameObject wallBlueprint = null;
    [SerializeField] private float sectionScale = 1.0f;
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
                wall[i][j] = Instantiate(new GameObject("Wall " + i + ", " + j), new Vector3(
                (-(size.x / 2) + i + ((size.x % 2 == 0) ? 0.5f : 0.0f)) * sectionScale,
                (-(size.y / 2) + j + ((size.y % 2 == 0) ? 0.5f : 0.0f)) * sectionScale,
                0.0f),
                new Quaternion(), transform);

                GameObject wallImage = Instantiate(wallBlueprint, wall[i][j].transform.position, new Quaternion(), wall[i][j].transform);
                wallImage.transform.localScale = Vector3.one * sectionScale;
                wallImage.SetActive(Random.Range(0, 2) == 0 ? true : false);
            }
        }
    }

    public void moveRiver(Vector3 vec)
    {
        transform.position += vec;
        /*while (transform.position.x > sectionScale)
        {
            shiftLeft();
        }*/
    }

    /*private void shiftLeft()
    {
        transform.position -= transform.right * sectionScale;

        for (int i = 0; i < size.x - 1; ++i)
        {
            for (int j = 0; j < size.y; ++j)
            {
                wall[i][j] = wall[i + 1][j];
                if (wall[i][j])
                    wall[i][j].transform.localPosition -= transform.right * sectionScale;
            }
        }

        for (int j = 0; j < size.y; ++j)
        {
            wall[size.x - 1][j] = createRandom(size.x - 1, j);
        }
    }*/
}
