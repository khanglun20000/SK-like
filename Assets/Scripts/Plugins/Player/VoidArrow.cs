using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidArrow : Arrow
{
    private Transform Void;
    // Start is called before the first frame update
    public override void Start()
    {
        pls = GetComponents<PolygonCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        p_swapping = FindObjectOfType<SwappingWeapon>();
        startDelayTime = 0.25f;
        delayTime = startDelayTime;
        transform.SetParent(FindObjectOfType<WeaponHolder1>().transform);
        spriteColor = sprite.GetComponent<SpriteRenderer>().color;
        transform.localEulerAngles = new Vector3(0, 0, 0);
        Void = GameObject.FindGameObjectWithTag("Void").transform;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 14)
        {
            if (!isCollided)
            {
                Invoke(nameof(CreateVoid), 0.1f);
            }
        }
        else
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    void CreateVoid()
    {
        isCollided = true;
        Void.position = transform.position;
        VoidController.isActive = true;
        Destroy(gameObject);
    }
}
