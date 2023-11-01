using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BallManager : MonoBehaviour
{
    static public bool push;
    static public int next_level;
    public GameObject[] ball_level;
    static public GameObject left, right;
    Queue<BallPair> que;
    float delay;
    float time;

    float score;
    int level;
    float upscore;
    public Text score_text;
    public Text level_text;

    // Start is called before the first frame update
    void Start()
    {
        delay = 100f;
        time = delay;
        push = false;
        que = new Queue<BallPair>();
        //level = 0;
        score = 0f;
        upscore = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0) time = 0;
        Debug.Log("������ ��");
        Debug.Log(que.Count);
        if (que.Count != 0) {
            
            Debug.Log("������");
            // �ϳ� ������ ��ġ��� �������� 1������ ���� ����
            BallPair tmp = que.Dequeue();
            Vector3 newposition = tmp.newPos;
            Destroy(tmp.left);
            Destroy(tmp.right);

            GameObject objt = Instantiate(ball_level[next_level]);
            upscore = next_level * 2;
            objt.transform.position = newposition;
            objt.GetComponent<BallControl>().falled = true;
            objt.GetComponent<BallControl>().dont_move = false;
            objt.GetComponent<BallControl>().done = true;
            objt.GetComponent<BallControl>().onGame = false;
            objt.GetComponent<BallControl>().madeByManager = false;
            objt = null;
            //obj.GetComponent<BallControl>().collider2D.enabled = true;
            //ball_level[next_level].transform.position = tmp.newPos;
            
            

            //ball_level[next_level].GetComponent<BallControl>().falled = true;
            //ball_level[next_level].GetComponent<BallControl>().dont_move = false;
            //ball_level[next_level].GetComponent<BallControl>().done = true;
            //ball_level[next_level].GetComponent<BallControl>().collider2D.enabled = true;
            //Instantiate(ball_level[next_level]);


            
            time = delay;
        }

        if (push)
        {
            Debug.Log("������~");
            Debug.Log("Left" + left);
            Debug.Log("Right"+ right);
            que.Enqueue(new BallPair(left,right));
            push = false;
            //time = delay;
        }
        // �浹�� 
        UpdateScore();
    }

    void UpdateScore() {
        if (upscore > 0) {
            upscore -= 0.01f;
            score += 0.01f;

            
        }
        score_text.text = "���� : " + (int)score;
        level_text.text = "�ܰ� : " + (int)GameManager.ball_level;
    }
}
