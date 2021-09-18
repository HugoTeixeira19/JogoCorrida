using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    float velocidade = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Acelerar();
        Desacelerar();
    }

    void Acelerar()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocidade += (Input.GetAxis("Horizontal") / 5);
            transform.Translate(new Vector3(velocidade * Time.deltaTime, 0, 0));
            Debug.Log("Acelerando!!");
        }
        else
        {
            if (velocidade > 0)
            {
                velocidade -= 0.2f;
                transform.Translate(new Vector3(velocidade * Time.deltaTime, 0, 0));
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
                transform.Translate(new Vector3(velocidade * Time.deltaTime, 0, 0));
            }
        }
    }
}
