using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoderesController : MonoBehaviour
{
    public int qtdPoderDisponivel = 3;

    public void Teleporte(GameObject player)
    {
        if (JogadorPresente(player))
        {
            player.transform.position = player.GetComponent<car>().teleporte.transform.position;
        }
        else
        {
            player.transform.position = player.GetComponent<CpuController>().teleporte.transform.position;
        }
        this.qtdPoderDisponivel--;
    }

    public void Turbo(GameObject player)
    {
        if (JogadorPresente(player))
        {
            player.GetComponent<car>().estaAtivoTurbo = true;
        }
        else
        {
            // arrumar da cpu
        }
        this.qtdPoderDisponivel--;
    }

    public void Atirar(GameObject player)
    {
        ArmaController armaPlayer = player.GetComponentInChildren<ArmaController>();

        if (Time.time > armaPlayer.nextFire)
        {
            armaPlayer.nextFire = Time.time + armaPlayer.fireRate;
            Instantiate(armaPlayer.balas, new Vector3(armaPlayer.canoArma.transform.position.x, armaPlayer.canoArma.transform.position.y, 0),
                armaPlayer.canoArma.transform.rotation);
            armaPlayer.Atirou();
        }
    }

    private bool JogadorPresente(GameObject player)
    {
        if(player.GetComponent<car>() != null)
        {
            return true;
        }
        return false;
    }
}
