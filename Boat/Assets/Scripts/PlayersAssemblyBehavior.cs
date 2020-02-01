using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersAssemblyBehavior : MonoBehaviour
{
    public GameObject               playerPrefab;
    private List<Transform>         players = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        // Create players
        var obj = Instantiate(playerPrefab, new Vector3(-1,0,0), Quaternion.identity, transform);
        players.Add(obj.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Straighten()
    {
        foreach (var p in players) {
            //p.Rotate(0,0,0);
            p.rotation = Quaternion.identity;
        }
    }
}
