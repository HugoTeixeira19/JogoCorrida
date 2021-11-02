using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaControllerCpu : MonoBehaviour
{
    public List<GameObject> objetosAlvos;
    public float distanciaMinima = 90f;
    public float distanciaObjeto;

    private float fireRate = 2;
    private float nextFire;

    public GameObject balas;
    public GameObject canoArma;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject objeto in GameObject.FindGameObjectsWithTag("corrente"))
        {
            if(objeto.layer == 9)
            {
                objetosAlvos.Add(objeto);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (objetosAlvos.Count > 0)
        {
            distanciaObjeto = Vector2.Distance(transform.position, objetosAlvos[0].transform.position);

            if (distanciaObjeto <= distanciaMinima)
            {
                if (objetosAlvos[0] != null)
                {
                    if (objetosAlvos[0].GetComponent<HingeJoint2D>() != null)
                    {
                        if (objetosAlvos[0].GetComponent<HingeJoint2D>().breakForce > 0)
                        {
                            transform.right = new Vector3(objetosAlvos[0].transform.position.x - transform.position.x,
                                                            objetosAlvos[0].transform.position.y - transform.position.y, 0);

                            if (Time.time > nextFire)
                            {
                                nextFire = Time.time + fireRate;
                                Instantiate(balas, new Vector3(canoArma.transform.position.x, canoArma.transform.position.y, 0),
                                    canoArma.transform.rotation);
                            }
                        }
                    }
                }
            }
        }
    }
}
