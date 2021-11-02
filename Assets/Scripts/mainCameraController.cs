using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCameraController : MonoBehaviour
{

    public float altura;
    public GameObject carPersonagem;
    public bool estaAtivo = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (estaAtivo)
        {
            float x = carPersonagem.transform.position.x;
            float z = carPersonagem.transform.position.z - 50.0f;
            altura = carPersonagem.transform.position.y;

            transform.position = new Vector3(x + 5.0f, altura, z);
        }
    }
}
