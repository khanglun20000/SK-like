using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;

    private float moveHorizontal, moveVertical;

    private Transform c_weapon;

    private Vector3 cursorPos;

    private bool facingRight = true;

    public Transform DropPoint;

    public bool isMouseVertical;

    public bool canMove = true;

    public Animator animator;

    public float speed;
    
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        moveHorizontal = Input.GetAxis("Horizontal");   
        moveVertical = Input.GetAxis("Vertical");
        moveInput = new Vector2(moveHorizontal,moveVertical);
}
private void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("CurrentWeapon") != null)
        {
            c_weapon = GameObject.FindGameObjectWithTag("CurrentWeapon").transform;
        }
        if (canMove)
        {
           
            //Move
            Move();
            if (Mathf.Abs(moveHorizontal) >= 0.01f || Mathf.Abs(moveVertical) > 0.01f)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            //Flip
            if (facingRight == false && cursorPos.x >= transform.position.x)
            {
                Flip();
            }
            else if (facingRight == true && cursorPos.x < transform.position.x)
            {
                Flip();
            }

            if (cursorPos.x >= transform.position.x && cursorPos.x >= transform.position.x + 2.5f || cursorPos.x < transform.position.x && cursorPos.x <= transform.position.x - 2.5f)
            {
                isMouseVertical = false;
            }
            else
            {
                isMouseVertical = true;
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        if (facingRight)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            if (GameObject.FindGameObjectWithTag("CurrentWeapon") != null)
            {
                if (c_weapon.GetComponent<Weapon>().d_weapondata.type != Type.Bow)
                {
                    c_weapon.GetComponent<SpriteRenderer>().flipY = false;
                }
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            if (GameObject.FindGameObjectWithTag("CurrentWeapon") != null)
            {
                if (c_weapon.GetComponent<Weapon>().d_weapondata.type != Type.Bow)
                {
                    c_weapon.GetComponent<SpriteRenderer>().flipY = true;
                }
            }
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
    }
    private void Move()
    {
        rb.MovePosition(rb.position + (moveInput * speed * Time.deltaTime));  
    }
}


