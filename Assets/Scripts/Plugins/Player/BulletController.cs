using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage, speed, timeBounce;
    public float force = 0.5f;
    public BulletData bulletData;
    public SwappingWeapon p_swapping;

    protected Rigidbody2D rb2d;
    protected Color spriteColor;
    private float delayTime=0, startDelayTime=0;
    protected bool isForced = false, isCollided = false;
    protected LayerMask whatIsEnemy;

    // Start is called before the first frame update
    virtual public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        p_swapping = FindObjectOfType<SwappingWeapon>();
        startDelayTime = GameObject.FindGameObjectWithTag("CurrentWeapon").GetComponent<Weapon>().d_weapondata.delayTime;
        delayTime = startDelayTime;
        transform.SetParent(FindObjectOfType<WeaponHolder1>().transform);
    }

    // Update is called once per frame
    virtual public void Update()
    {
        if (!isForced)
        {
            if (delayTime < 0)
            {
                transform.SetParent(null);
                rb2d.AddForce(transform.up * speed * 100 * Time.deltaTime, ForceMode2D.Impulse);
                isForced = true;
                delayTime = startDelayTime;
                p_swapping.canSwap = true;
            }
            else
            {
                delayTime -= Time.deltaTime;
                transform.localPosition = Vector3.zero;
                p_swapping.canSwap = false;
            }
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 14)
        {
            if (!isCollided)
            {
                isCollided = true;
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.layer == 14)
        {
            int critChance = Random.Range(1, 101);
            if (critChance > bulletData.critChance)
            {
                collision.transform.GetComponent<EnemyController>().TakeDamage(damage);
            }
            else
            {
                collision.transform.GetComponent<EnemyController>().TakeDamage(damage * 2);
            }
            rb2d.velocity = Vector2.zero;
            rb2d.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(collision.transform);
            GetComponentInChildren<SpriteRenderer>().sortingOrder = -30;
        }
    }
}
