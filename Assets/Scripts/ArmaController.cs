using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaController : MonoBehaviour
{
    public Vector2 alvo;
    public GameObject balas;
    public GameObject canoArma;
    public GameObject mira;

    public float fireRate;
    public float nextFire;
    private int maxTiro = 3;

    // Update is called once per frame
    void Update()
    {
        alvo = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        transform.right = mira.transform.position;
    }

    public int GetMaxTiro
    {
        get { return maxTiro; }
    }

    public void AddMunicao()
    {
        if(GetMaxTiro == 3)
        {
            maxTiro = 3;
        } else
        {
            maxTiro += 1;
        }
    }

    public void Atirou()
    {
        this.maxTiro--;
    }
}
