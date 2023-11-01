using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BallPair
{
    public GameObject left;
    public GameObject right;
    public Vector2 newPos;

    public BallPair(GameObject l, GameObject r)
    {
        left = l;
        right = r;
        newPos = new Vector2((l.transform.position.x + r.transform.position.x) / 2, (l.transform.position.y + r.transform.position.y) / 2);
    }
}


public class GameManager : MonoBehaviour
{
    static public bool make_ball;
    static public bool there_ball;
    public GameObject[] ball_list;
    public GameObject[] ball_list_gened;
    static public int ball_level;
    public Vector2 start_point;
    GameObject obj;
    GameObject objt;
    static int rollChance;

    public AudioSource audio;

    static public bool push;
    static public int next_level;
    //public GameObject[] ball_level_list;
    static public GameObject left, right;
    Queue<BallPair> que;
    float delay;
    float time;

    static public float score;
    int level;
    float upscore;
    public Text score_text;
    public Text level_text;
    public Text roll_text;

    // Start is called before the first frame update
    void Start()
    {
        rollChance = 3;
        there_ball = true;
        make_ball = true;
        ball_level = 0;
       // makeBall();
        audio = GetComponent<AudioSource>();


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
        if (obj != null)
        {
            if (obj.GetComponent<BallControl>().onGame == true && obj.GetComponent<BallControl>().madeByManager == true)
            {
                //obj = null;
                makeBall();
            }
        }
        else
            makeBall();

        time -= Time.deltaTime;
        if (time < 0) time = 0;

        if (Input.GetMouseButtonDown(0))
        {
            obj.GetComponent<BallControl>().falled = true;
        }

        if (que.Count != 0)
        {
            
            // 하나 꺼내서 위치잡고 레벨보다 1높은거 새로 생성
            BallPair tmp = que.Dequeue();
            Vector3 newposition = tmp.newPos;


            objt = ball_list_gened[next_level];
            upscore += next_level * 2;
            //objt.GetComponent<BallControl>().falled = true;
            //objt.GetComponent<BallControl>().dont_move = false;
            //objt.GetComponent<BallControl>().done = true;
            //objt.GetComponent<BallControl>().onGame = false;
            //objt.GetComponent<BallControl>().madeByManager = false;
            Instantiate(objt, new Vector2(newposition.x, newposition.y), Quaternion.identity);
            // objt = null;

            Destroy(tmp.left);
            Destroy(tmp.right);

            time = delay;
        }

        if (push && left!=null && right != null) {
            audio.Play();
            que.Enqueue(new BallPair(left, right));
            left = null;
            right = null;
            push = false;
        }
        // 충돌된 
        UpdateScore();
        ReRoll();
    }

    void makeBall() {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, -Camera.main.transform.position.z));
        int rand = (int)Random.Range(0f, (float)ball_level);
        obj = Instantiate(ball_list[rand], new Vector2(point.x, start_point.y), Quaternion.identity);
    }


    void UpdateScore()
    {
        if (upscore > 0)
        {
            upscore -= 0.1f;
            score += 0.1f;
        }
        score_text.text = "점수 : " + (int)score;
        level_text.text = "단계 : " + (int)ball_level;
        roll_text.text = "리롤 : " + (int)rollChance;
    }

    void ReRoll() {
        if (Input.GetMouseButtonDown(1)) {
            if (rollChance > 0) {
                rollChance--;
                Destroy(obj);
                makeBall();
            }
        }
    }

    static public void updateRank(int level) {
        ball_level = level + 1;
        rollChance++;
    }
}
