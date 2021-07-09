using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerMovement playerMovement;

    public float speed, startDodgeTime, startDodgeCD;
    private float dodgeTime, dodgeCD;

    public Animator animator;

    private bool isDodging;

    private Vector2 mousePosition, mouseDir;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dodgeTime = startDodgeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (dodgeCD <= 0)
        {
            Dodge();
        }
        else
        {
            dodgeCD -= Time.deltaTime;
        }
    }

    private void Dodge()
    {
        if (!isDodging)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isDodging = true;
                playerMovement.canMove = false;
                mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mouseDir = mousePosition - (Vector2)transform.position;
            }
        }
        else
        {
            if (dodgeTime <= 0)
            {
                dodgeCD = startDodgeCD;
                dodgeTime = startDodgeTime;
                rb.velocity = Vector2.zero;
                playerMovement.canMove = true;
                isDodging = false;
                animator.SetBool("isDodging", false);
            }
            else
            {
                dodgeTime -= Time.deltaTime;
                rb.velocity = -mouseDir.normalized * speed;
                if (playerMovement.isMouseVertical == false)
                {
                    animator.SetBool("isDodging", true);
                }
            }
        }
    }
}
