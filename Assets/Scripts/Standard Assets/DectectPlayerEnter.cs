using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DectectPlayerEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.parent.GetComponent<RoomInstance>().TriggerRoom();
        }
    }
}
