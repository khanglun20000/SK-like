using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator animator;
    public bool isOpened=false;
    public GameObject DroppedWeaponPf;
    public Transform DropPoint;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= 1.6f && !isOpened)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                animator.SetBool("isOpening", true);
                isOpened = true;
                Instantiate(DroppedWeaponPf, DropPoint.position, Quaternion.identity);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
