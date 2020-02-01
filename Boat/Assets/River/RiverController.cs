using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverController : MonoBehaviour
{
    public class hole
    {
        public Vector2Int loc = Vector2Int.zero;
        public float width = 0.0f;

        public hole(Vector2Int LOC, float WIDTH)
        {
            loc = LOC;
            width = WIDTH;
        }

        public void adjust(Vector2Int size)
        {
            if (loc.y <= width)
            {
                loc.y += Random.Range(0, 2);
            }
            else if (loc.y >= size.y - 1 - width)
            {
                loc.y -= Random.Range(0, 2);
            }
            else
            {
                loc.y += Random.Range(-1, 2);
            }
        }
    }

    [SerializeField] private Vector2Int size = Vector2Int.zero;
    [SerializeField] private GameObject wallBlueprint = null;
    [SerializeField] private float sectionScale = 1.0f;
    private GameObject[][] wall;
    private hole frontHole = null;

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

        frontHole = new hole(new Vector2Int(size.x - 1, size.y / 2), 3.0f);

        moveRiver(Vector3.left * (size.x * sectionScale));
    }

    public void moveRiver(Vector3 vec)
    {
        transform.position += vec;

        while (transform.position.x > sectionScale)
        {
            shiftLeft();
        }

        while (transform.position.x <= -sectionScale)
        {
            shiftRight();
        }
    }

    private void shiftLeft()
    {
        transform.position -= transform.right * sectionScale;
        frontHole.adjust(size);

        for (int i = size.x - 1; i > 0; --i)
        {
            for (int j = 0; j < size.y; ++j)
            {
                wall[i][j].SetActive(wall[i - 1][j].activeSelf);
            }
        }

        for (int j = 0; j < size.y; ++j)
        {
            wall[0][j].SetActive(true);
        }
    }

    private void shiftRight()
    {
        transform.position += transform.right * sectionScale;
        frontHole.adjust(size);

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
            holeymoley(size.x - 1, j, frontHole);
        }
    }

    private void holeymoley(int i, int j, hole h)
    {
        if (System.Math.Abs(i - h.loc.x) + System.Math.Abs(j - h.loc.y) <= h.width)
        {
            wall[i][j].SetActive(false);
        }
    }
}
