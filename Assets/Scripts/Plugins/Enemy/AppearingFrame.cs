using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingFrame : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator.Play("Ani_AppearingFrame");
        Destroy(gameObject, 1.5f);
    }
}
