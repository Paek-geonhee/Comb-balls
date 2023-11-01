using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobyManager : MonoBehaviour
{
    public GameObject lookstory;
    public GameObject nostory;
    public GameObject panel;
    public Image loading;
    float time;
    public float maxtime;


    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        loading.fillAmount = time / maxtime;
        if (time > maxtime)
        {
            time = 0;
        }
    }

    public void GameStart() {
        SceneManager.LoadScene("2GameScene");
    }

    public void LookRank() {
        SceneManager.LoadScene("4RankingScene");
    }

    public void Setting() {
        ;
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void OnStory() {
        panel.SetActive(true);
        nostory.SetActive(true);
        lookstory.SetActive(false);
    }

    public void OffStory() {
        panel.SetActive(false);
        nostory.SetActive(false);
        lookstory.SetActive(true);
    }
}
