using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public enum Type
    {
        Player,
        Ranged
    }
    public Type type;
    public GameObject sprite;
    public int damage, speed, timeBounce;
    public float force = 0.5f;
    public BulletData arrowData;

    protected Rigidbody2D rb2d;
    protected Color spriteColor;
    protected float delayTime, startDelayTime;
    protected PolygonCollider2D[] pls;
    protected bool isForced = false, isCollided=false;

    //player only
    protected SwappingWeapon p_swapping;
    protected LayerMask whatIsEnemy;

    //ranged only

    // Start is called before the first frame update
    virtual public void Start()
    {
        switch (type)
        {
            case Type.Player:
                p_swapping = FindObjectOfType<SwappingWeapon>();
                startDelayTime = GameObject.FindGameObjectWithTag("CurrentWeapon").GetComponent<Weapon>().d_weapondata.delayTime;
                transform.SetParent(FindObjectOfType<WeaponHolder1>().transform);
                break;
            case Type.Ranged:

                break;
        }
        pls = GetComponents<PolygonCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        delayTime = startDelayTime;
        spriteColor = sprite.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    virtual public void Update()
    {
        if (!isForced)
        {
            if (delayTime < 0)
            {
                foreach (PolygonCollider2D pl in pls)
                {
                    pl.enabled = true;
                }
                transform.SetParent(null);
                rb2d.AddForce(transform.up * speed * 100 * Time.deltaTime, ForceMode2D.Impulse);
                isForced = true;
                delayTime = startDelayTime;
                if (type == Type.Player)
                {
                    p_swapping.canSwap = true;
                }
            }
            else
            {
                delayTime -= Time.deltaTime;
                transform.localPosition = Vector3.zero;
                foreach (PolygonCollider2D pl in pls)
                {
                    pl.enabled = false;
                }
                if(type == Type.Player)
                {
                    p_swapping.canSwap = false;
                }
            }
        }
    }
    private void Fade()
    {
        spriteColor *= (float)Random.Range(45f,70f)/100f;
        spriteColor.a = 1;
        sprite.GetComponent<SpriteRenderer>().color = spriteColor;
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 14 && collision.gameObject.layer != 9)
        {
            switch (type)
            {
                case Type.Player:
                    if (!isCollided)
                    {
                        isCollided = true;
                        Invoke(nameof(Fade), 5f);
                        Destroy(gameObject, 30f);
                        sprite.GetComponent<SpriteRenderer>().sortingOrder = -30;
                        foreach (PolygonCollider2D pl in pls)
                        {
                            pl.enabled = false;
                        }
                    }
                    break;
                case Type.Ranged:
                    Destroy(gameObject);
                    break;
            }
        }
        else if(collision.gameObject.layer == 14)
        {
            int critChance = Random.Range(1, 101);
            if (critChance > arrowData.critChance)
            {
                collision.transform.GetComponent<EnemyController>().TakeDamage(damage);
            }
            else
            {
                collision.transform.GetComponent<EnemyController>().TakeDamage(damage*2);
            }
            rb2d.velocity = Vector2.zero;
            rb2d.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(collision.transform);
            Fade();
            foreach (PolygonCollider2D pl in pls)
            {
                pl.enabled = false;
            }
            GetComponentInChildren<SpriteRenderer>().sortingOrder = -30;
        }
        else if(collision.gameObject.layer == 9)
        {
            int critChance = Random.Range(1, 101);
            if (critChance > arrowData.critChance)
            {
                collision.transform.GetComponent<PlayerBehaviour>().TakeDamage(damage);
            }
            else
            {
                collision.transform.GetComponent<PlayerBehaviour>().TakeDamage(damage * 2);
            }
            Destroy(gameObject);
        }
    }
}
