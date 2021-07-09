using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected RoomInstance room;
    protected PlayerBehaviour player;
    protected bool facingRight;
    public Vector2 movementSpeed;
    public Animator animator;
    [HideInInspector]
    public bool isAttacking;
    [HideInInspector]
    public bool canAttack;
    [HideInInspector]
    public bool canMove;

    public int maxHealth;
    public int currentHealth;

    protected bool isForced, isDead, canDie;
    protected float forcedTime;
    protected float changeMovementTime;
    protected Rigidbody2D rb;
    protected Vector2 moveInput;
    protected int movementType;
    
    protected float force;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerBehaviour>();
        room = GetComponentInParent<RoomInstance>();
        currentHealth = maxHealth;
        isForced = false;
        forcedTime = force;
        changeMovementTime = Time.time;
        isDead = false;
        canMove = true;
        canDie = true;
        canAttack = false;
    }


    // Update is called once per frame
    void Update()
    {

        //check if enemy is being pulled back
        if (isForced)
        {
            forcedTime -= Time.time;
        }
        if (forcedTime <= 0)
        {
            isForced = false;
            forcedTime = force;
        }
        //check health <0
        if (currentHealth <= 0 && canDie)
        {
            Die();
            canDie = false;
        }
        //activate enemy when player went in room
        if (room.playerWentIn == true && !isForced && !isDead)
        {
            if (Time.time >= changeMovementTime)
            {
                movementType = Random.Range(0, 16);
                changeMovementTime += Random.Range(0.6f,0.9f);
            }
            Move(movementType);
            FacePlayer();
            // let other script know that player is in room
            canAttack = true;
        }
    }
    protected void Move(int movement)
    {
        switch (movement)
        {
            case 0:
                moveInput = new Vector2(Random.Range(0.5f,1.5f), Random.Range(-0.2f,0.2f));
                break;
            case 1:
                moveInput = new Vector2(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f));
                break;
            case 2:
                moveInput = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(0.5f, 1.5f));
                break;
            case 3:
                moveInput = new Vector2(-Random.Range(0.5f, 1.5f), Random.Range(-0.2f, 0.2f));
                break;
            case 4:
                moveInput = new Vector2(-Random.Range(0.5f, 1.5f), -Random.Range(0.5f, 1.5f));
                break;
            case 5:
                moveInput = new Vector2(Random.Range(-0.2f, 0.2f), -Random.Range(0.5f, 1.5f));
                break;
            case 6:
                moveInput = new Vector2(Random.Range(0.5f, 1.5f), -Random.Range(0.5f, 1.5f));
                break;
            case 7:
                moveInput = new Vector2(-Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f));
                break;
            case 8:
                moveInput = Vector2.zero;
                break;
            case 9:
                moveInput = Vector2.zero;
                break;
            case 10:
                moveInput = Vector2.zero;
                break;
            case 11:
                moveInput = Vector2.zero;
                break;
        }
        if (canMove)
        {
            if (movement < 8 && !isAttacking)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
            rb.MovePosition(rb.position + (moveInput * Time.deltaTime * Random.Range(movementSpeed.x,movementSpeed.y)));
        }
    }
    protected void FacePlayer()
    {
        if (player.transform.position.x < transform.position.x && facingRight == false)
        {
            Flip();
        }
        else if(player.transform.position.x >= transform.position.x && facingRight == true)
        {
            Flip();
        }
    }
    protected void Flip()
    {
        if (!isAttacking)
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }
    }
    protected void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        GetComponentInParent<RoomInstance>().monsCount--;
        Destroy(rb);
        Destroy(gameObject.GetComponent<E_Attack>());
        Destroy(GetComponent<CapsuleCollider2D>());
        canMove = false;
        Destroy(gameObject,1.15f);
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
    public IEnumerator PushedBack(float knockbackDuration, float knockbackPower, Transform obj)
    {
        float timer = 0;
        while (knockbackDuration>timer)
        {
            timer += Time.deltaTime;
            Vector3 dir = (obj.transform.position - transform.position).normalized;
            rb.AddForce(-dir * knockbackPower);
            isForced = true;
        }
        yield return 0;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        isForced = true;
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Vector2 dir = collision.contacts[0].point - (Vector2)transform.position;
            dir = -dir.normalized;
            force = collision.gameObject.GetComponent<Arrow>().force;
            rb.AddForce(dir * force);
        }
    }
    
}
