using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : PlayerController
{
    private Controlador controlador;
    float velocidade = 0;
    public static bool estaNaSuperficie = false;
    private bool desativarCarro = false;


  
    void Start()
    {

        nomePlayer = "Dark Banshir";
        controlador = FindObjectOfType<Controlador>();
    }

    void Update()
   {
        if (!desativarCarro)
       {
            Acelerar();
            Desacelerar();

        }
    }

 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("cpu"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if(collision.gameObject.CompareTag("tiro"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("superficie"))
        {
            estaNaSuperficie = true;
            CarroViradoAoContrario();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("superficie"))
        {
            estaNaSuperficie = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (controlador.FinalizarCorrida(collision, this.gameObject))
        {
            Debug.Log("Entrou no método");
            desativarCarro = true;
            GetComponentInChildren<ArmaController>().enabled = false;
            velocidade = 0;
        }
        PegarMoedas(collision);

        if(collision.gameObject.CompareTag("perigo"))
        {
            controlador.PlayerExplodiu(this.gameObject);
        }
    }

    void Acelerar()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (velocidade < 100)
            {
                velocidade = (Input.GetAxis("Vertical") * 18.0f);
            }
            GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidade * Time.deltaTime, 0), ForceMode2D.Impulse);
            if (!estaNaSuperficie)
            {
                transform.Rotate(new Vector3(0, 0, 0.9f));
            }


        }
    }

    void Desacelerar()
    {
        if(Input.GetKey(KeyCode.DownArrow))
        {
            if(velocidade > 0)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-12f * Time.deltaTime, 0), ForceMode2D.Impulse);
            }
            if (!estaNaSuperficie)
            {
                transform.Rotate(new Vector3(0, 0, -0.8f));
            }
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

    void PegarMoedas(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            controlador.AdicionarMoedas = collision.GetComponent<MoedasScript>().valor;
            Destroy(collision.gameObject);
        }
    }

   
}
