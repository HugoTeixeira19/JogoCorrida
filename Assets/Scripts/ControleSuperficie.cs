using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleSuperficie : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestruirObjeto(collision, "corrente", 8);

        DestruirObjeto(collision, "objetos", 10);
    }

    private void DestruirObjeto(Collision2D collision, string tag, float segundos)
    {
        if(collision.gameObject.CompareTag(tag)) {
            Destroy(collision.gameObject, segundos);
        }
    }
}
