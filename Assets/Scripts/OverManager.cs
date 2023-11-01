using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverManager : MonoBehaviour
{

    public Text score;
    public Text level;
    // Start is called before the first frame update
    void Start()
    {
        score.text = "���� : " + (int)GameManager.score;
        level.text = "�ܰ� : " + (int)GameManager.ball_level;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToGame() {
        SceneManager.LoadScene("2GameScene");
    }

    public void Quit() {
        Application.Quit();
    }

    public void MoveToLobby() {
        SceneManager.LoadScene("1LobyScene");
    }
}
