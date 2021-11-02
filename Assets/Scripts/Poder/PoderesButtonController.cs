using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoderesButtonController : MonoBehaviour
{
    private GameObject player;
    private PoderesController[] poderes = new PoderesController[2];

    void Start()
    {
        player = FindObjectOfType<car>().gameObject;
        poderes[0] = new PoderesController();
        poderes[1] = new PoderesController();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            switch (this.name)
            {
                case "PoderMedio":
                    if (player.GetComponent<car>().estaAtivoTurbo)
                    {
                        this.GetComponent<Button>().interactable = false;
                    }
                    else if (!player.GetComponent<car>().estaAtivoTurbo && poderes[1].qtdPoderDisponivel > 0)
                    {
                        this.GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        this.GetComponent<Button>().interactable = false;
                    }
                    break;

                case "PoderForte":
                    this.GetComponent<Button>().interactable = (poderes[0].qtdPoderDisponivel > 0) ? true : false;
                    break;

                case "Atirar":
                    this.GetComponent<Button>().interactable = (player.GetComponentInChildren<ArmaController>().GetMaxTiro > 0) ? true : false;
                    break;
            }
        } else
        {
            this.GetComponent<Button>().interactable = false;
            FindObjectOfType<btnAcelerar>().GetComponent<Button>().interactable = false;
            FindObjectOfType<btnFreiar>().GetComponent<Button>().interactable = false;
        }
    }

    public void Teleporte()
    {
        Debug.Log($"Qtd turbo {poderes[0].qtdPoderDisponivel}");
        if (poderes[0].qtdPoderDisponivel > 0)
        {
            poderes[0].Teleporte(player);
        }
    }

    public void Turbo()
    {
        if(poderes[1].qtdPoderDisponivel > 0)
        {
            if (!player.GetComponent<car>().estaAtivoTurbo)
            {
                poderes[1].Turbo(player);
            } else
            {
                this.GetComponent<Button>().interactable = false;
            }
        } else
        {
            this.GetComponent<Button>().interactable = false;
        }
    }

    public void Atirar()
    {
        if (player.GetComponentInChildren<ArmaController>().GetMaxTiro > 0)
        {
            this.GetComponent<Button>().interactable = true;
            poderes[0].Atirar(player);
        }
    }
}
