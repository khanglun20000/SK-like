using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Attack : MonoBehaviour
{
    public Transform attackSpot;
    public Vector2 attackDamage;
    public float attackTrigger, timeBwAttack = 2f, attackDur, attackRange; // attackTrigger: range to trigger attack, attackDur: time to finish attack anim, attackRange: range of damage
    private float nextAttack;
    public LayerMask whatIsPlayer;
    public Animator animator;
    public Transform bulletPf;

    private EnemyController EC;
    private PlayerBehaviour player;

    public enum AttackType
    {
        Melee,
        Ranged
    }
    public AttackType attackType;
    // Start is called before the first frame update
    void Start()
    {
        EC = GetComponent<EnemyController>();
        player = FindObjectOfType<PlayerBehaviour>();
        nextAttack = timeBwAttack;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackType == AttackType.Ranged)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            attackSpot.transform.up = direction;
        }
        if (EC.canAttack)
        {
            if (Vector2.Distance(attackSpot.position, player.transform.position) < attackTrigger && Time.time > nextAttack)
            {
                switch (attackType)
                {
                    case AttackType.Melee:
                        StartCoroutine(MeleeAttack());
                        break;
                    case AttackType.Ranged:
                        StartCoroutine(RangedAttack());
                        break;
                }
                nextAttack = Time.time + timeBwAttack;
            }
        }
    }
    IEnumerator MeleeAttack()
    {
        EC.isAttacking = true;
        int damage = Mathf.RoundToInt(Random.Range(attackDamage.x, attackDamage.y));
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isAttacking", true);
        animator.SetBool("isMoving", false);
        yield return new WaitForSeconds(0.08f);
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackSpot.position, attackRange, whatIsPlayer);
        foreach (Collider2D player in hit)
        {
            player.transform.GetComponent<PlayerBehaviour>().TakeDamage(damage);
        }
        yield return new WaitForSeconds(attackDur);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isMoving", true);
        EC.isAttacking = false;
    }
    IEnumerator RangedAttack()
    {
        EC.isAttacking = true;
        int damage = Mathf.RoundToInt(Random.Range(attackDamage.x, attackDamage.y));
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isAttacking", true);
        animator.SetBool("isMoving", false);
        yield return new WaitForSeconds(0.08f);
        Instantiate(bulletPf, attackSpot.position, attackSpot.rotation, transform);
        yield return new WaitForSeconds(attackDur);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isMoving", true);
        EC.isAttacking = false;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackSpot.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackSpot.position, attackTrigger);
    }
}
