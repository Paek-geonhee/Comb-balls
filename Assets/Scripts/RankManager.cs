using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankManager : MonoBehaviour
{
    public Text myScore;
    int score;
    int level;
    // Start is called before the first frame update
    void Start()
    {
        myScore.text = "³» Á¡¼ö : " + (int)GameManager.ball_level + " / " + (int)GameManager.score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToLobby() {
        SceneManager.LoadScene("1LobyScene");
    }
}
