 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlatform : MonoBehaviour
{
    public float time;
    public Vector2 speed;//처음시간
    float pretime;//시작시간
    public Rigidbody2D rigidbody;

    public bool isFirstStop;//처음에 멈추는지
    Vector2 startPosition;
    bool isStop;

  

    private void Awake()
    {
        pretime = time;
        startPosition = (Vector2)transform.position;
        if (isFirstStop)
        {
            isStop = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isStop)
        {
            rigidbody.velocity = speed;
            pretime -= Time.deltaTime;
            if (pretime < 0)
            {
                pretime = time;
                if (isFirstStop)
                {
                    transform.position = startPosition;
                    isStop = true;
                }
                else
                {
                    speed = -speed;
                }
            }
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isStop = false;
        }
    }
}
