using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CpuController : MonoBehaviour
{
    // Start is called before the first frame update
    private float velocidade = 0;
    private static bool estaNaSuperficie = false;
    private Controlador controlador;

    void Start()
    {
        controlador = FindObjectOfType<Controlador>();
    }

    // Update is called once per frame
    void Update()
    {
        if(estaNaSuperficie)
        {
            Acelerar();
        }
        else
        {
            EstabilizarCarro();
            transform.Translate(new Vector3((velocidade * Time.deltaTime) / 2, 0, 0));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("tiro"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("superficie"))
        {
            Debug.Log("Colidiu");
            estaNaSuperficie = true;
            CarroViradoAoContrario();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("superficie"))
        {
            Debug.Log("Não colidiu");
            estaNaSuperficie = false;
        }
    }

    void Acelerar()
    {
        if (velocidade < 100)
        {
            velocidade = 15.48f;
        }
        GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidade * Time.deltaTime, 0), ForceMode2D.Impulse);
        Debug.Log("Acelerando!!");
    }

    void EstabilizarCarro()
    {
        Debug.Log($"Valor da rotação: {transform.rotation.eulerAngles.z}");
        if (transform.rotation.eulerAngles.z > 300f)
        {
            Debug.Log("Rotacionar para esquerda");
            transform.Rotate(new Vector3(0, 0, 0.9f));
        } else if(transform.rotation.eulerAngles.z >= 0.2f)
        {
            Debug.Log("Rotacionar para direita");
            transform.Rotate(new Vector3(0, 0, -0.8f));
        }
    }

    void CarroViradoAoContrario()
    {
        if (transform.rotation.eulerAngles.z >= 170 && transform.rotation.eulerAngles.z <= 180
            || transform.rotation.eulerAngles.z <= -151 && transform.rotation.eulerAngles.z <= -180)
        {
            Debug.Log("Destruiu o carro");
        }
    }
}
