using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public ComponentManager componentScript;
    private InputScript inputScript; // 입력
    private Rigidbody2D rigid; // 리지드바디
    private Animator animator; // 애니메이터
    public GameObject[] Canvases;

    public float moveSpeed = 5f;
    public ParticleSystem moveDust;

    void Awake()
    {
        inputScript = GetComponent<InputScript>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (componentScript)
        {
            rigid.velocity =  new Vector2(inputScript.GroundSpeed .x+moveSpeed * inputScript.move * componentScript.MoveComponent, rigid.velocity.y);//속도
            if (inputScript.GroundSpeed.y < 0 && !inputScript.jump)
            {
                rigid.velocity = new Vector2(inputScript.GroundSpeed.x + moveSpeed * inputScript.move * componentScript.MoveComponent, inputScript.GroundSpeed.y);//속도
            }

            if (inputScript.move * componentScript.MoveComponent != 0)
            {
                animator.SetFloat("isWalk", Mathf.Abs(inputScript.move * componentScript.MoveComponent));//애니메이션
                transform.localScale = inputScript.move > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1); //방향전환

                for (int i = 0; i < Canvases.Length; i++)
                {
                    Canvases[i].transform.localScale = inputScript.move > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1); //캔버스는 반대로 방향전환
                }
                if (animator.GetBool("isGrounded")) moveDust.Play(); else moveDust.Stop(); //파티클
            }
            else
            {
                animator.SetFloat("isWalk", 0);
                moveDust.Stop();
            }
        }
        else
        {
            rigid.velocity = new Vector2(moveSpeed * inputScript.move, rigid.velocity.y);//속도
            if (inputScript.move != 0)
            {
                animator.SetFloat("isWalk", Mathf.Abs(inputScript.move));//애니메이션
                transform.localScale = inputScript.move > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1); //방향전환

                for (int i = 0; i < Canvases.Length; i++)
                {
                    Canvases[i].transform.localScale = inputScript.move > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1); //캔버스는 반대로 방향전환
                }
                if (animator.GetBool("isGrounded")) moveDust.Play(); else moveDust.Stop(); //파티클
            }
            else
            {
                animator.SetFloat("isWalk", 0);
                moveDust.Stop();
            }
        }

        
    }
}
