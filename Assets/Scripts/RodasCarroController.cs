using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodasCarroController : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("superficie"))
        {
            if (player.GetComponent<car>() != null)
            {
                player.GetComponent<car>().estaNaSuperficie = true;
            } else
            {
                player.GetComponent<CpuController>().estaNaSuperficie = true;
            }
        }

        IgnorarColisaoPlayerETiro(collision);
    }

    void IgnorarColisaoPlayerETiro(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("cpu"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        }

        if(collision.gameObject.CompareTag("tiro"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("superficie"))
        {
            if (player.GetComponent<car>() != null)
            {
                player.GetComponent<car>().estaNaSuperficie = false;
            } else
            {
                player.GetComponent<CpuController>().estaNaSuperficie = false;
            }
        }

        IgnorarColisaoPlayerETiro(collision);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
