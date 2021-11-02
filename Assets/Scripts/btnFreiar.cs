using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class btnFreiar : MonoBehaviour,  IPointerDownHandler, IPointerUpHandler
{
    // variável de comunicação com o player
    public GameObject player;
    JointMotor2D jointMotor;

    public float speed;
    public bool isMotorOn;
    float sensibilidade = -200f;
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

    // Update is called once per frame
    void Update()
    {
        if (isPressing)
        {
            WheelJoint2D[] rodas = player.GetComponent<car>().GetComponentsInChildren<WheelJoint2D>();

            jointMotor.maxMotorTorque = 20;
            jointMotor.motorSpeed = 0;
            rodas[0].motor = jointMotor;
            rodas[1].motor = jointMotor;
        }

        if (player != null)
        {
            if (!player.GetComponent<car>().estaNaSuperficie)
            {
                player.transform.Rotate(0, 0, -0.8f);
            }
        }
    }
}
