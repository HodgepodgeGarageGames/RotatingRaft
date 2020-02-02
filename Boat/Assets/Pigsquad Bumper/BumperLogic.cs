using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class BumperLogic : MonoBehaviour
{
    [SerializeField] private VideoPlayer vp = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playVid());
    }

    private IEnumerator playVid()
    {
        vp.Play();

        while (!vp.isPlaying)
            yield return null;

        while (vp.isPlaying)
            yield return null;

        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }
}
