using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverTiros : MonoBehaviour
{

    public float velocidade = 55f;
    public int danoTiro = 20;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 5);
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("corrente"))
        {
            if (collision.gameObject.GetComponent<HingeJoint2D>() != null)
            {
                collision.gameObject.GetComponent<HingeJoint2D>().breakForce = (collision.gameObject.GetComponent<HingeJoint2D>().breakForce > 0) ? 0 : 0;
            }
        }

        if (collision.gameObject.CompareTag("superficie"))
        {
            Debug.Log("Colidiu com a superficie");
            Destroy(this.gameObject);
        }
    }
}
