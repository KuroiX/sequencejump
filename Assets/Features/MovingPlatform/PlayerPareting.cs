using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPareting : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;

        other.transform.parent = transform;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;

        other.transform.parent = null;
    }
}
