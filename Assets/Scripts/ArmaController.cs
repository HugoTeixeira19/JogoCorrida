using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaController : MonoBehaviour
{
    public Vector2 alvo;
    public GameObject balas;
    public GameObject canoArma;

    public float fireRate;
    public float nextFire;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        alvo = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.right = new Vector3(alvo.x, alvo.y, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(balas, new Vector3(canoArma.transform.position.x, canoArma.transform.position.y, 0),
                canoArma.transform.rotation);
        }
    }
}
