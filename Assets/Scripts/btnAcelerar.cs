using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class btnAcelerar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    
    //variavel de comunicação com o player
    public GameObject player;

    public float speed;
    public bool isMotorOn;
    float sensibilidade = 200f;
    public bool isPressing;
    // Start is called before the first frame update

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (!player.GetComponent<car>().estaAtivoTurbo)
            {
                if (isPressing && speed < 5000)
                {
                    isMotorOn = true;
                    speed += Time.deltaTime * sensibilidade * 5;
                }
                else if (isPressing && speed >= 5000)
                {
                    speed = 5000;
                }
                else
                {
                    isMotorOn = false;
                    speed = 200;
                }
            }
            else
            {
                car Player = player.GetComponent<car>();
                Debug.Log($"Timer: {Player.GetTimerTurbo}");
                isMotorOn = true;
                if (Player.GetTimerTurbo > 0)
                {
                    Player.GetTimerTurbo -= Time.deltaTime;
                    speed = 10000;
                }
                else
                {
                    Player.estaAtivoTurbo = false;
                    isMotorOn = false;
                    Player.ResetarTimerTurbo();
                }
            }

            if (!player.GetComponent<car>().estaNaSuperficie)
            {
                Debug.Log("Não está colidindo");
                player.transform.Rotate(0, 0, 0.8f, Space.Self);
            }
        } else
        {
            foreach (GameObject carro in FindObjectsOfType<GameObject>())
            {
                if (carro.GetComponent<car>() != null)
                {
                    player = carro;
                }
            }
        }

    }
}
