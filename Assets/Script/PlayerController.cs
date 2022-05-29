using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] bool isRight;
    [SerializeField] bool grounded;
    [SerializeField] bool isDead;
    [SerializeField] Vector3 boxSize;
    [SerializeField] LayerMask groundLayer;
    int jumpTimes = 1;

    float horizontal;

    // Start is called before the first frame update
    void Start()
    {
        if(anim == null) anim = GetComponent<Animator>();
        if(rb == null) rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;

        horizontal = Input.GetAxis("Horizontal");
        Vector2 moveDir = new Vector2(horizontal * speed, rb.velocity.y);
        rb.velocity = moveDir;
        anim.SetFloat("speed", horizontal);
        if(horizontal > 0.0f && isRight)
        {
            Flip();
        }
        else if(horizontal < 0.0f && !isRight)
        {
            Flip();
        }
        BeginJump();
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

        isRight = !isRight;

    }

    void BeginJump()
    {
        grounded = isGrounded();
        anim.SetBool("inAir", !isGrounded());
        if(Input.GetButtonDown("Jump") && isGrounded())
        {
            Jumping();
            AnimTrigger("jump");
        }
        else if(Input.GetButtonDown("Jump") && !isGrounded() && jumpTimes > 0)
        {
            jumpTimes = 0;
            Jumping();
            AnimTrigger("doubleJump");
        }
        if (isGrounded()) jumpTimes = 1;
    }

    private void Jumping()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    bool isGrounded()
    {
        Vector2 groundCheckerSize = new Vector2(boxSize.x, boxSize.y);
        return Physics2D.BoxCast(transform.position, groundCheckerSize, 0.05f, Vector2.down, 0.1f, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Color color = Color.red;
        Gizmos.color = color;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Trap"))
        {
            AnimTrigger("hit");
            Jumping();
        }
    }

    private void AnimTrigger(string name)
    {
        anim.SetTrigger(name);
    }
}
