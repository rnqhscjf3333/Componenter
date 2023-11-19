using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public ComponentManager componentScript;
    private InputScript inputScript; // �Է�
    private Rigidbody2D rigid; // ������ٵ�
    private Animator animator; // �ִϸ�����

    public int jumpPower;
    public bool isGrounded;
    public int jumpCount;
    public ParticleSystem jumpDust;
    public Transform jumpPivot;//������ Ž���ϴ� ����

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
        if (!isGrounded && jumpCount < componentScript.JumpCompoent && rigid.velocity.y > 0) animator.SetBool("isJumping", true);//���� ���� ����ī��Ʈ�� �������� �������λ��·� ����
        else animator.SetBool("isJumping", false);
        animator.SetBool("isGrounded", isGrounded);

        Debug.DrawRay(jumpPivot.position, Vector2.down* jumpScope, new Color(0, 1, 0));    //�Ʒ��κ���
        Collider2D rayHit = Physics2D.OverlapCircle(jumpPivot.position, 0.1f, 1 << LayerMask.NameToLayer("Ground"));
        //RaycastHit2D rayHit = Physics2D.CapsuleCast(jumpPivot.position, jumpPivot.position+Vector3.down, 5f, Vector2.down, jumpScope, LayerMask.GetMask("Ground")) ;

        if (inputScript.jump)//����
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

        if (rayHit != null)//����
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
 
        if (rayHit == null)//����
        {
            isGrounded = false;
            jumpCount = Mathf.Min(jumpCount, componentScript.JumpCompoent - 1);
        }

    }
}
