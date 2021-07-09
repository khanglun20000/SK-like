using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public ParticleSystem Particle;
    public bool isAttacked;
    private void Update()
    {
        if (isAttacked)
        {
            Instantiate(Particle, transform.position + new Vector3(0, 0.5f, -3), Quaternion.identity);
            Destroy(gameObject, 0.05f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 8 || collision.gameObject.layer == 17) && !collision.gameObject.CompareTag("VoidArrow"))
        {
            Instantiate(Particle, transform.position + new Vector3(0, 0.5f, -3), Quaternion.identity);
            Destroy(gameObject,0.05f);
            Destroy(collision.gameObject);
        }
    }
}
