using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    private Controlador controlador;
    float velocidade = 0;
    public static bool estaNaSuperficie = false;
    
    

    void Start()
    {
        controlador = FindObjectOfType<Controlador>();
    }

    void Update()
    {
        if (estaNaSuperficie)
        {
            Acelerar();
            Desacelerar();
        }
        else
        {
            EstabilizarCarro();
            transform.Translate(new Vector3((velocidade * Time.deltaTime) / 2, 0, 0));
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
        if (collision.gameObject.CompareTag("chegada"))
        {
            velocidade = 0;
            this.enabled = false;
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
            //transform.Translate(new Vector3(velocidade * Time.deltaTime, 0, 0));
            GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidade * Time.deltaTime, 0), ForceMode2D.Impulse);
        }
        else
        {
            if (velocidade > 0)
            {
                velocidade -= 5.0f;
            }
        }
    }

    void Desacelerar()
    {
        if(Input.GetKey(KeyCode.DownArrow))
        {
            if(velocidade > 0)
            {
                velocidade -= 0.6f;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidade * Time.deltaTime, 0), ForceMode2D.Impulse);
            }
        }
    }

    void EstabilizarCarro()
    {        
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 0, -0.8f));
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, 0, 0.9f));
        }
    }

    void CarroViradoAoContrario()
    {
        if (transform.rotation.eulerAngles.z >= 170 && transform.rotation.eulerAngles.z <= 180 
            || transform.rotation.eulerAngles.z >= -151 && transform.rotation.eulerAngles.z <= -180)
        {
            controlador.PlayerExplodiu(this);
        }
    }
}
