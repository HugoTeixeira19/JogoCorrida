using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    public GameObject _buttonAcelerar;
    btnAcelerar _componentAcelerar;
    public GameObject _buttonFreiar;
    btnFreiar _componentFreiar;

    [SerializeField]
    private WheelJoint2D wheel;
    [SerializeField]
    private WheelJoint2D wheelFront;
    JointMotor2D jointMotor;

    // variável que valida se o veículo está na superficie
    public bool estaNaSuperficie = false;
    public GameObject teleporte;
    private Controlador controlador;
    public bool destruirVeiculo = false;
    public bool desativarCarro = false;

    [Header("Variaveis de poderes")]
    public bool estaAtivoTurbo = false;
    private float timerTurbo = 5f;


    // Update is called once per frame

    private void Start()
    {
        
        //_componentAcelerar = _buttonAcelerar.GetComponent<btnAcelerar>();
        //_componentFreiar =  _buttonFreiar.GetComponent<btnFreiar>();


        controlador = FindObjectOfType<Controlador>();
    }
    void Update()
    {
        if (!desativarCarro)
        {
            if (_buttonAcelerar == null && _buttonFreiar == null)
            {
                foreach (GameObject botao in FindObjectsOfType<GameObject>())
                {
                    if (botao.GetComponent<btnAcelerar>() != null)
                    {
                        _buttonAcelerar = botao;
                    }

                    if (botao.GetComponent<btnFreiar>() != null)
                    {
                        _buttonFreiar = botao;
                    }
                }
            }
            else if (_componentAcelerar == null && _componentFreiar == null)
            {
                _componentAcelerar = _buttonAcelerar.GetComponent<btnAcelerar>();
                _componentFreiar = _buttonFreiar.GetComponent<btnFreiar>();
            }
            else
            {
                wheel.useMotor = _componentAcelerar.isMotorOn;
                wheelFront.useMotor = _componentAcelerar.isMotorOn;
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

    void IgnorarColisaoPlayerETiro(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("cpu"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
        if(collision.gameObject.CompareTag("tiro"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PegarMoedas(collision);
        PegarMunicao(collision);
    }

    private void FixedUpdate()
    {
        if (_componentAcelerar != null && _componentFreiar != null)
        {
            if(estaAtivoTurbo)
            {
                jointMotor.maxMotorTorque = 10;
                jointMotor.motorSpeed = -(_componentAcelerar.speed);
                wheel.motor = jointMotor;
                wheelFront.motor = jointMotor;
            }
            else if (_componentAcelerar.isPressing)
            {
                jointMotor.maxMotorTorque = 10;
                jointMotor.motorSpeed = -(_componentAcelerar.speed);
                wheel.motor = jointMotor;
                wheelFront.motor = jointMotor;
            }
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

    void PegarMunicao(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Debug.Log("Pegou a municão");
            GetComponentInChildren<ArmaController>().AddMunicao();
            Destroy(collision.gameObject);
        }
    }

    public float GetTimerTurbo
    {
        get { return timerTurbo; }
        set { timerTurbo = value; }
    }

    public void ResetarTimerTurbo()
    {
        this.timerTurbo = 5f;
    }
    
}
