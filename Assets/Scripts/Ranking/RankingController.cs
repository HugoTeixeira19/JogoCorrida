using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingController : MonoBehaviour
{
    public static RankingController controlador;

    public List<Ranking> listaColocacoes { get; set; }
    private static string path = ".\\Assets\\Configure\\details.csv";
    private static StreamWriter escrita;
    public int rankingFaseAtual = 1;

    [Header("Componentes da tela")]
    public GameObject[] textoColocacoes = new GameObject[5];
    public TextMeshProUGUI tituloFase;
    public TextMeshProUGUI respostaSemResultados;
    private void Awake()
    {
        if(controlador == null)
        {
            controlador = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Ranking")
        {
            listaColocacoes = ConsultarDados(rankingFaseAtual);
            PreencherComCondicaoPainel();
        }
    }

    public void PreencherComCondicaoPainel()
    {
        if (listaColocacoes.Count > 0)
        {
            respostaSemResultados.enabled = false;
            PreencherPainelRanking();
        }
        else
        {
            respostaSemResultados.enabled = true;
        }
        tituloFase.SetText($"Fase {rankingFaseAtual}");
    }

    public List<Ranking> ConsultarDados(int fase)
    {
        try
        {
            List<Ranking> listaColocacoes = new List<Ranking>();
            using (StreamReader sr = new StreamReader(path))
            {
                string linha;

                while((linha = sr.ReadLine()) != null)
                {
                    Ranking rank = new Ranking()
                    {
                        fase = int.Parse(linha.Split(';')[0]),
                        nome = linha.Split(';')[1],
                        tempoFinalizacao = linha.Split(';')[2]
                    };
                    
                    if (rank.fase == fase)
                    {
                        listaColocacoes.Add(rank);
                    }
                }
            }

            return listaColocacoes;
            

        } catch(Exception e)
        {
            Debug.LogError($"Erro ao tentar recuperar dados: {e.Message}");
        }

        return null;
    }

    public void PreencherPainelRanking()
    {
        List<string> temposListados = new List<string>();
        foreach (Ranking rank in listaColocacoes)
        {
            string minutos = rank.tempoFinalizacao.Trim().Substring(0, 2);
            string segundos = rank.tempoFinalizacao.Trim().Substring(3, 2);

            temposListados.Add($"{minutos}{segundos} {listaColocacoes.IndexOf(rank)}");
        }

        temposListados.Sort();
        temposListados.Reverse();

        for(int i = 0; i < temposListados.Count; i++)
        {
            if(i < 5)
            {
                textoColocacoes[i].SetActive(true);
                textoColocacoes[i].GetComponentsInChildren<TextMeshProUGUI>()[1].SetText(listaColocacoes[int.Parse(temposListados[i].Substring(5))].nome);
                textoColocacoes[i].GetComponentsInChildren<TextMeshProUGUI>()[2].SetText($"{temposListados[i].Substring(0, 2)}:{temposListados[i].Substring(2, 2)}");
            }
        }
    }

    public void GravarDados(Ranking rank)
    {
        string[] aux = File.ReadAllLines(path);
        string[] valores = new string[3];

        using (escrita = new StreamWriter(path, true))
        {
            if (aux.Length < 1)
            {
                escrita.Write($"{rank.fase};{rank.nome};{rank.tempoFinalizacao}");
                escrita.Close();
            }
            else
            {
                escrita.WriteLine();
                escrita.Write($"{rank.fase};{rank.nome};{rank.tempoFinalizacao}");
                escrita.Close();
            }
        }
    }

    public void CriarArquivo(Ranking rank)
    {
        if (!File.Exists(path))
        {
            escrita = File.CreateText(path);
            escrita.Close();
            GravarDados(rank);
        } else
        {
            GravarDados(rank);
        }
    }
}
