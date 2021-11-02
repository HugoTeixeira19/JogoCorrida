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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BoxCollider2D>() != null && collision.gameObject.GetComponent<BoxCollider2D>().IsTouching(this.GetComponent<Collider2D>()))
        {
            Debug.Log("Entrou no método de colisão com o jogador, logo destruir veiculo");
            if (collision.gameObject.GetComponent<car>() != null)
            {
                collision.gameObject.GetComponent<car>().destruirVeiculo = true;
            }
            else if (collision.gameObject.GetComponent<CpuController>() != null)
            {
                collision.gameObject.GetComponent<CpuController>().destruirVeiculo = true;
            }
        }
    }

    private void DestruirObjeto(Collision2D collision, string tag, float segundos)
    {
        if(collision.gameObject.CompareTag(tag)) {
            if(collision.gameObject.layer == 9)
            {
                if(FindObjectOfType<CpuController>().GetComponentInChildren<ArmaControllerCpu>().objetosAlvos.Contains(collision.gameObject))
                {
                    FindObjectOfType<CpuController>().GetComponentInChildren<ArmaControllerCpu>().objetosAlvos.Remove(collision.gameObject);
                }
            }
            Destroy(collision.gameObject, segundos);
        }
    }
}
