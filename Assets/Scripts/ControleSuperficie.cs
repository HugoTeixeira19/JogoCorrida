using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleSuperficie : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("corrente"))
        {
            Destroy(collision.gameObject, 10);
        }
    }
}
