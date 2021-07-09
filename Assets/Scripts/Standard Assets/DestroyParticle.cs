using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        float time = gameObject.GetComponent<ParticleSystem>().main.startLifetime.constantMax;
        Destroy(gameObject, time);   
    }
}
