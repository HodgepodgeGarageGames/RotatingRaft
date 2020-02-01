using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverControllerBetter : MonoBehaviour
{
    public class Hole
    {
        public Vector2Int loc = Vector2Int.zero;
        public int radLength = 0;

        public Hole(Vector2Int LOC, int RADLENGTH, GameObject[][] wall)
        {
            loc = LOC;
            radLength = RADLENGTH;

            carve(wall);
        }

        private void carve (GameObject[][] wall)
        {
            for (int i = loc.x - radLength; i <= loc.x + radLength; ++i)
            {
                for (int j = loc.y - radLength; j <= loc.y + radLength; ++j)
                {
                    wall[i][j].SetActive(false);
                }
            }
        }

        public void move (GameObject[][] wall, int y)
        {
            if (y > loc.y)
            {
                for (int i = loc.x - radLength; i <= loc.x + radLength; ++i)
                {
                    for (int j = loc.y - radLength; j <= loc.y + radLength; ++j)
                    {
                        wall[i][j].SetActive(false);
                    }
                }
            }
        }
    }

    [SerializeField] private Vector2Int size = Vector2Int.zero;
    [SerializeField] private GameObject wallBlueprint = null;
    [SerializeField] private float sectionScale = 1.0f;
    private GameObject[][] wall;
    private Hole hole = null;

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
            }
        }

        hole = new Hole(new Vector2Int (size.x / 2, size.y / 2), 3, wall);
    }

    public void moveRiver(Vector3 vec)
    {
        transform.position += vec;

        while (transform.position.x <= -sectionScale)
        {
            shiftRight();
        }
    }

    private void shiftRight()
    {
        transform.position += transform.right * sectionScale;

        for (int i = 0; i < size.x - 1; ++i)
        {
            for (int j = 0; j < size.y; ++j)
            {
                wall[i][j].SetActive(wall[i + 1][j].activeSelf);
            }
        }

        for (int j = 0; j < size.y; ++j)
        {
            wall[size.x - 1][j].SetActive(true);
            

        }
    }
}
