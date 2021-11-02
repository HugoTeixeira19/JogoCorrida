using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CpuController : PlayerController
{
    // Start is called before the first frame update
    private float speed = 0;
    private Controlador controlador;
    private bool desativarCarro = false;

    public GameObject pontoAtualSpawn;

    [SerializeField]
    private WheelJoint2D wheel;
    [SerializeField]
    private WheelJoint2D wheelFront;
    JointMotor2D jointMotor;

    float sensibilidade = 200f;

    // variável que valida se o veículo está na superficie
    public bool estaNaSuperficie = false;
    public GameObject teleporte;
    public bool destruirVeiculo = false;

    void Start()
    {
        nomePlayer = "Armstrong";
        controlador = FindObjectOfType<Controlador>();
        speed = 100;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        wheel.useMotor = estaNaSuperficie;
        wheelFront.useMotor = estaNaSuperficie;

        if (!desativarCarro)
        {
            if (estaNaSuperficie)
            {
                if (speed < 5000)
                {
                    speed += Time.deltaTime * sensibilidade * 5;
                }
                else if (speed >= 5000)
                {
                    speed = 5000;
                }
                else
                {
                    speed = 200;
                }

                jointMotor.maxMotorTorque = 10;
                jointMotor.motorSpeed = -(speed);
                wheel.motor = jointMotor;
                wheelFront.motor = jointMotor;
            }
            else
            {
                EstabilizarCarro();
            }

            if (destruirVeiculo)
            {
                controlador.PlayerExplodiu(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IgnorarColisaoPlayerETiro(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IgnorarColisaoPlayerETiro(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        IgnorarColisaoPlayerETiro(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(controlador.FinalizarCorrida(collision, this.gameObject))
        {
            Debug.Log("Terminou a corrida cpu");
            desativarCarro = true;
            speed = 0;
        }

        if (collision.gameObject.CompareTag("perigo"))
        {
            controlador.PlayerExplodiu(this.gameObject);
        }
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

    void IgnorarColisaoPlayerETiro(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }

        if(collision.gameObject.CompareTag("tiro"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }

    public void Teleporte()
    {
        transform.position = teleporte.transform.position;
    }
}
