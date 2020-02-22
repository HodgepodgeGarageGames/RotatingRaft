using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private GameObject instructions = null;
    [SerializeField] private GameObject startButton = null;
    [SerializeField] private AButton[] abuttons = new AButton[4];
    [SerializeField] private TitleToon[] toon = new TitleToon[4];
    [SerializeField] private Text hiscore = null;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("High Score"))
        {
            GlobalGameData.high_score = PlayerPrefs.GetInt("High Score");
            hiscore.text = GlobalGameData.high_score + "s";
        }
        else
        {
            GlobalGameData.high_score = 0;
            hiscore.text = "0s";
        }

        GlobalGameData.playersIn[0] = false;
        GlobalGameData.playersIn[1] = false;
        GlobalGameData.playersIn[2] = false;
        GlobalGameData.playersIn[3] = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalGameData.playersIn[0] ||
            GlobalGameData.playersIn[1] ||
            GlobalGameData.playersIn[2] ||
            GlobalGameData.playersIn[3])
        {
            instructions.SetActive(true);
        }
        else
        {
            instructions.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void GetGameStarted()
    {
        StartCoroutine(getgamestarted());
    }

    private IEnumerator getgamestarted()
    {
        for (int i = 0; i < 4; ++i)
        {
            abuttons[i].gameObject.SetActive(false);

            if (toon[i].gameObject.activeSelf)
            {
                toon[i].GetComponent<Animator>().SetBool("Go", true);
                toon[i].playStartSound();
            }
        }

        startButton.SetActive(false);
        
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("RiverTest", LoadSceneMode.Single);
    }
}
