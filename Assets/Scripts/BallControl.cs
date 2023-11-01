using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallControl : MonoBehaviour
{
    public int level;
    public int radius;
    public CircleCollider2D collider2D;
    Rigidbody2D rigid;
    BoxCollider2D box;
    public bool dont_move, falled, done;
    int Speed;
    public bool onGame;
    public bool madeByManager;
    bool canComb;

    public AudioSource audio;
    //bool move;
    //public sprite spr;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        collider2D = GetComponent<CircleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        Speed = 20;
        // falled = false;
        rigid.gravityScale = 2;
        // if(falled) box.enabled = false;
        //onGame = false;
        // madeByManager = true;
        canComb = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!falled)
        {
            collider2D.enabled = false;
            Check_Wall();
            //move = true;
            if (!dont_move)
            {
                Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, -Camera.main.transform.position.z));

                transform.position = Vector3.MoveTowards(transform.position, new Vector2(point.x, 4.5f), Speed * Time.deltaTime);

            }
            transform.position = new Vector2(transform.position.x, 4f);
            rigid.gravityScale = 0;


        }
        else
        {
            box.enabled = false;
            rigid.gravityScale = 2;
            collider2D.enabled = true;
            //dont_move = true;
            checkGameOver();
        }

        
    }

    private void Check_Wall() {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, -Camera.main.transform.position.z));
        Vector2 pointA = new Vector2(transform.position.x - transform.localScale.x * 0.55f, transform.position.y);
        Vector2 pointB = new Vector2(transform.position.x + transform.localScale.x * 0.55f, transform.position.y);
        Debug.DrawLine(pointA, pointA, Color.red);
        Debug.DrawLine(pointB, pointB, Color.red);
        if (Physics2D.OverlapArea(pointA, pointA) && point.x < transform.position.x)
        {
            Speed = 0;
        }
        else {
            if (Physics2D.OverlapArea(pointB, pointB) && point.x > transform.position.x)
            {
                Speed = 0;
            }
            else
                Speed = 30;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);

        if (!done && collision.transform.tag != "Wall")
        {
            //GameManager.there_ball = true;
            done = true;
        }
        if (collision.transform.tag == "Ball")
        {
            
            if (level == collision.transform.GetComponent<BallControl>().level && canComb == true)
            {

                canComb = false;

                if (transform.position.x < collision.transform.position.x)
                {
                    GameManager.left = gameObject;
                }
                else if (transform.position.x > collision.transform.position.x)
                {
                    GameManager.right = gameObject;
                }
                else
                {
                    if (transform.position.y < collision.transform.position.y)
                    {
                        GameManager.left = gameObject;
                    }
                    else if (transform.position.y > collision.transform.position.y)
                    {
                        GameManager.right = gameObject;
                    }
                }

                GameManager.next_level = level + 1;
                if (GameManager.ball_level < level + 1) {
                    GameManager.updateRank(level);
                }


                gameObject.SetActive(false);
                GameManager.push = true;
            }
            done = true;
            falled = true;
            onGame = true;
            dont_move = true;
        }
        else if (collision.transform.tag == "Ground")
        {
            if (falled == false)
                audio.Play();
            done = true;
            falled = true;
            onGame = true;
            dont_move = true;
            
        }
    }

    void checkGameOver(){
        if (onGame && transform.position.y >= 3.9f) {
            SceneManager.LoadScene("3GameoverScene");
        }
    }
}
