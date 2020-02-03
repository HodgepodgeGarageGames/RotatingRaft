using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageGridBehavior : MonoBehaviour
{
    public GameObject   tileObject;
    public int numberOfTilesToDamage = 5;
    public float fractionToDie = 0.33f;
    public float redamageGraceSecs = 1;
    public int gridWidth = 16;
    public int gridHeight = 16;
    public float gridSpacing = 0.32f;
    public GameObject   gameOverObject;
    private float lastDamageTime = 0;
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
                tileGrid[i, j] = obj.transform;
                //checkerboard
                if ((i % 3) != 0 || (j % 3) != 0) {
                    obj.SetActive(false);
                }
            }
        }
    }

    public Transform getClosestDamagedTile(Vector3 pos, float dist)
    {
        Transform closest = null;

        for (int i = 0; i < gridWidth; ++i)
        {
            for (int j = 0; j < gridHeight; ++j)
            {
                if (tileGrid[i,j].gameObject.activeSelf)
                {
                    float mag = (tileGrid[i, j].position - pos).magnitude;

                    if (closest == null)
                    {
                        if (mag <= dist)
                            closest = tileGrid[i, j];
                    }
                    else
                    {
                        if (mag < (closest.position - pos).magnitude && mag <= dist)
                            closest = tileGrid[i, j];
                    }
                }
            }
        }

        return closest;
    }

    public void repairTile(Transform tile)
    {
        for (int i = 0; i < gridWidth; ++i)
        {
            for (int j = 0; j < gridHeight; ++j)
            {
                if (tileGrid[i, j] == tile)
                {
                    tile.gameObject.SetActive(false);
                    return;
                }
            }
        }
    }

    public void IncurDamage()
    {
        if (Time.fixedTime - lastDamageTime < redamageGraceSecs) {
            Debug.Log("Not damaging due to grace period");
            return;
        }
        lastDamageTime = Time.fixedTime;

        int breakNum = numberOfTilesToDamage;

        List<Transform> undamaged = FindUndamagedTiles();
        undamaged = Shuffle(undamaged);

        int count = undamaged.Count;
        for (int i=0; i < breakNum && i < count; ++i) {
            undamaged[0].gameObject.SetActive(true);
            undamaged.RemoveAt(0);
        }

        if (count - breakNum < tileGrid.Length*(1-fractionToDie)) {
            StartCoroutine(gameover());
        }

    }

    public IEnumerator gameover()
    {
        gameOverObject.SetActive(true);

        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }

    public List<Transform> FindUndamagedTiles()
    {
        var list = new List<Transform>();
        foreach (Transform t in tileGrid) {
            if (!t.gameObject.activeSelf) list.Add(t);
        }
        return list;
    }

    public List<Transform> Shuffle(List<Transform> l)
    {
        var ret = new List<Transform>();
        int count = l.Count;
        while (count-- != 0) {
            int rand = (int)(Random.value*count);
            ret.Add(l[rand]);
            l.RemoveAt(rand);
        }

        return ret;
    }
}
