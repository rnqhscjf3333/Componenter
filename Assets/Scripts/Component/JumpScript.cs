using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public ComponentManager componentScript;
    private InputScript inputScript; // 입력
    private Rigidbody2D rigid; // 리지드바디
    private Animator animator; // 애니메이터

    public int jumpPower;
    public bool isGrounded;
    public int jumpCount;
    public ParticleSystem jumpDust;
    public Transform jumpPivot;//땅인지 탐지하는 기준

    public float jumpScope = 1.1f;

    public AudioSource JumpSound;

    void Awake()
    {
        inputScript = GetComponent<InputScript>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!isGrounded && jumpCount < componentScript.JumpCompoent && rigid.velocity.y > 0) animator.SetBool("isJumping", true);//땅에 없고 점프카운트가 깎여있으면 점프중인상태로 설정
        else animator.SetBool("isJumping", false);
        animator.SetBool("isGrounded", isGrounded);

        Debug.DrawRay(jumpPivot.position, Vector2.down* jumpScope, new Color(0, 1, 0));    //아래로빔쏨
        Collider2D rayHit = Physics2D.OverlapCircle(jumpPivot.position, 0.1f, 1 << LayerMask.NameToLayer("Ground"));
        //RaycastHit2D rayHit = Physics2D.CapsuleCast(jumpPivot.position, jumpPivot.position+Vector3.down, 5f, Vector2.down, jumpScope, LayerMask.GetMask("Ground")) ;

        if (inputScript.jump)//점프
        {
            if (jumpCount > 0)
            {
                jumpDust.Play();
                JumpSound.Play();
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                rigid.AddForce(Vector2.up * jumpPower);
                jumpCount -= 1;
                animator.SetTrigger("isJump");
            }
            inputScript.jump = false;
        }

        if (rayHit != null)//착지
        {
            if (!isGrounded)
            {
                jumpDust.Play();
            }
            isGrounded = true;
            jumpCount = componentScript.JumpCompoent;


        }
        if (rayHit != null && rayHit.GetComponent<Rigidbody2D>() != null)
        {
            inputScript.GroundSpeed = rayHit.GetComponent<Rigidbody2D>().velocity;
        }
        else
        {
            inputScript.GroundSpeed = new Vector2(0, 0);
        }
 
        if (rayHit == null)//공중
        {
            isGrounded = false;
            jumpCount = Mathf.Min(jumpCount, componentScript.JumpCompoent - 1);
        }

    }
}
