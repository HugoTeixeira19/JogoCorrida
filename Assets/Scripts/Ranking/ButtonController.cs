using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void Proximo()
    {
        if (RankingController.controlador.rankingFaseAtual < 10)
        {
            RankingController.controlador.rankingFaseAtual++;
            AjustarPainel();
        }
    }

    public void Anterior()
    {
        if (RankingController.controlador.rankingFaseAtual > 1)
        {
            RankingController.controlador.rankingFaseAtual--;
            AjustarPainel();
        }
    }

    private void AjustarPainel()
    {
        RankingController.controlador.listaColocacoes = RankingController.controlador.ConsultarDados(RankingController.controlador.rankingFaseAtual);
        
        foreach (GameObject colocacao in RankingController.controlador.textoColocacoes)
        {
            colocacao.SetActive(false);
        }

        RankingController.controlador.PreencherComCondicaoPainel();
    }
}
