using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosterInput : InputScript
{
    public Transform Sight; //raycast �� ��ġ
    public float AttackScope; //���ݹ���

    public int health;
    float hitcul = 0;
    public float Angry;
    public Transform playerTrans;
    public ComponentManager componentScript;
    Rigidbody2D rigid;

    RaycastHit2D GroundrayHit;
    RaycastHit2D GroundrayHit2;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        if (move ==0)
            move = 1;
    }

    // Update is called once per frame
    void Update()
    {
        hitcul -= Time.deltaTime;
        if (canMove)
        {
            Debug.DrawRay(Sight.position, Vector3.down*3, new Color(0, 1, 0)); //���� �Ʒ��� ��
            GroundrayHit = Physics2D.Raycast(Sight.position, Vector3.down*3, 3, LayerMask.GetMask("Ground"));


            Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), new Vector3(move , 0, 0), new Color(1, 1, 1)); //����Ž��2
            GroundrayHit2 = Physics2D.Raycast(transform.position + new Vector3(0, 1f, 0), new Vector3(move, 0, 0), 0.5f, LayerMask.GetMask("Ground"));
            if (rigid.velocity.y ==0 && !GroundrayHit || GroundrayHit2)
            {
                move *= -1;
            }

            Debug.DrawRay(Sight.position, new Vector3(move * 5, 0, 0), new Color(0, 0, 1)); //�÷��̾�Ž��
            RaycastHit2D PlayerrayHit = Physics2D.Raycast(Sight.position, new Vector3(move * 5, 0, 0), 1, LayerMask.GetMask("Player"));
            if (PlayerrayHit && componentScript.AttackComponent != "")
            {
                Angry = 3f;
                playerTrans = PlayerrayHit.transform;
            }

            Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), new Vector3(move * AttackScope, 0, 0), new Color(1, 0, 0)); //����Ž��
            RaycastHit2D AttackrayHit = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), new Vector3(move * AttackScope, 0, 0), 1, LayerMask.GetMask("Player"));
            if (AttackrayHit)
            {
                attack = true;
            }

            if (Angry > 0)
            {
                Angry -= Time.deltaTime;
                if (move != 0)
                {
                    if (playerTrans.position.x < transform.position.x)
                    {
                        move = -1;
                    }
                    else
                        move = 1;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack")
        {
            if (health > 0 && hitcul < 0)
            {
                health -= 1;
                GetComponent<AttackScript>().CancelInvoke("EndAttack");
                if (health <= 0)
                {
                    GetComponent<Animator>().SetTrigger("isDead");
                    GetComponent<Animator>().SetBool("isDeading", true);
                    Dead();
                }
                else
                {
                    GetComponent<Animator>().SetTrigger("isHit");
                    hitcul = 0.5f;
                }
            }
        }
    }
    void Dead()
    {
        move = 0;
        canMove = false;
    }


}
