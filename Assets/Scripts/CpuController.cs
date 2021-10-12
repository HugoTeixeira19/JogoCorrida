using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CpuController : PlayerController
{
    // Start is called before the first frame update
    private float velocidade = 0;
    private static bool estaNaSuperficie = false;
    private Controlador controlador;
    private bool desativarCarro = false;

    public GameObject pontoAtualSpawn;

    void Start()
    {
        nomePlayer = "Armstrong";
        controlador = FindObjectOfType<Controlador>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!desativarCarro)
        {
            if (estaNaSuperficie)
            {
                Acelerar();
            }
            else
            {
                EstabilizarCarro();
                transform.Translate(new Vector3((velocidade * Time.deltaTime) / 2, 0, 0));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IgnorarColisaoPlayerETiro(collision);
        if (collision.gameObject.CompareTag("superficie"))
        {
            estaNaSuperficie = true;
            CarroViradoAoContrario();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IgnorarColisaoPlayerETiro(collision);

        if (collision.gameObject.CompareTag("superficie"))
        {
            estaNaSuperficie = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(controlador.FinalizarCorrida(collision, this.gameObject))
        {
            Debug.Log("Terminou a corrida cpu");
            desativarCarro = true;
            velocidade = 0;
        }

        if (collision.gameObject.CompareTag("perigo"))
        {
            controlador.PlayerExplodiu(this.gameObject);
        }
    }

    void Acelerar()
    {
        if (velocidade < 100)
        {
            velocidade = 15.48f;
        }
        GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidade * Time.deltaTime, 0), ForceMode2D.Impulse);
    }

    void EstabilizarCarro()
    {
        if (transform.rotation.eulerAngles.z > 300f)
        {
            transform.Rotate(new Vector3(0, 0, 0.9f));
        } else if(transform.rotation.eulerAngles.z >= 0.2f)
        {
            transform.Rotate(new Vector3(0, 0, -0.8f));
        }
    }

    void CarroViradoAoContrario()
    {
        if (transform.localEulerAngles.z >= 126 && transform.localEulerAngles.z <= 180
            || transform.localEulerAngles.z <= -126 && transform.localEulerAngles.z <= -180)
        {
            controlador.PlayerExplodiu(this.gameObject);
        }
    }

    void IgnorarColisaoPlayerETiro(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("tiro"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
