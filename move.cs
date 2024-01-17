using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    Rigidbody2D rigidbody2D;                    Animator animator;
    private float horizaltal;                    BoxCollider2D boxCollider;
    private float speed = 150.0f;                float scale = 1f;
    public bool doublejump=true;               public LayerMask ground;
    public Transform _transform;                   public float radius = 0.5f;
    public float radius_wall_slide;
   public bool wallslide = false;                   public Transform transfrom_wall_slide;
   public bool contact_grounded = false;
    void Start()
    {
       rigidbody2D = GetComponent<Rigidbody2D>();
        speed = 200.0f;
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        doublejump = true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {   
         horizaltal = Input.GetAxis("Horizontal");
        if (horizaltal >= 0)
        {
            scale = 1;
        }
        else if (horizaltal < 0)
        {
            scale = -1;
        }
       if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
        {
            rigidbody2D.velocity = new Vector2(speed*horizaltal*Time.deltaTime  ,rigidbody2D.velocity.y);
            transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        }
      
        animator.SetFloat("run", Mathf.Abs(horizaltal));
    }
    void Update()
    {
        contact_grounded = Physics2D.OverlapCircle(_transform.position, radius, ground);
        wallslide = Physics2D.OverlapCircle(transfrom_wall_slide.position, radius_wall_slide, ground);
        if (contact_grounded)
        {
            doublejump = false;
            animator.SetBool("doublejump", doublejump);
            wallslide = false;
        }
       else if (contact_grounded==false)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.Space))
        {  
            if (contact_grounded||doublejump==false)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 15f);
                doublejump = !doublejump;
                animator.SetBool("doublejump", doublejump);
            }

        }
        animator.SetBool("jump", !contact_grounded);
        if (wallslide && !contact_grounded&&horizaltal!=0)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,Mathf.Clamp(rigidbody2D.velocity.y,-2f,-3f));
            doublejump = false;
        }
        if (wallslide&&horizaltal==0)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x *- transform.localScale.x*1.01f, rigidbody2D.velocity.y);
        }
        animator.SetBool("walljump", wallslide);
    }

    private void OnDrawGizmos()
    {
       
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_transform.position, radius);
        Gizmos.DrawWireSphere(transfrom_wall_slide.position, radius_wall_slide);
    }
}
