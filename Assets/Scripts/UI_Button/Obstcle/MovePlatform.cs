using Assets.PixelHeroes.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Move_Platform : MonoBehaviour
{
    public Transform posA, posB;
    public float speed;
    Vector3 targetPos;
    
    public Rigidbody2D rigidbody2D;
    ComponentManager componentManager;
        
    private void Start()
    {
        targetPos = posB.position;
        rigidbody2D = GetComponent<Rigidbody2D>();
 
    }

    void Update()
    {
        Vector2 velocity = rigidbody2D.velocity;

        float xSpeed = velocity.x;
        float ySpeed = velocity.y;

        rigidbody2D.velocity = targetPos;

        Debug.Log("X 속도: " + xSpeed);
        Debug.Log("Y 속도: " + ySpeed);
        Debug.Log("속도: " + rigidbody2D.velocity);

        if (Vector2.Distance(transform.position, posA.position) < 0.05f)
        {
            targetPos = posB.position;
        }

        if(Vector2.Distance(transform.position, posB.position)<0.05f)
        {
            targetPos = posA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            
            
          
        }
    }

  

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
        }
    }


}
