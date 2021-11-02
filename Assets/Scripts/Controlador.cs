using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controlador : MonoBehaviour
{
    public static Controlador controlador;
    public GameObject pontoAtualSpawn;
    private car playerController;
    public GameObject prefabCarroPlayer;
    public GameObject prefabCarroCPU;
    
    public GameObject telaFimCorrida;
    public GameObject panelColocacoes;
    public Image iconeMoeda;

    public float timer;
    public TextMeshProUGUI textoTempo;
    public TextMeshProUGUI textoQtdMoedas;
    public TextMeshProUGUI textoTotalMoedas;
    public TextMeshProUGUI textoQtdAtualBalas;

    private mainCameraController mainCamera;
    public List<GameObject> carrosChegaram;

    private int qtdAtualMoedas = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(controlador == null)
        {
            controlador = FindObjectOfType<Controlador>();
        }
        playerController = FindObjectOfType<car>();
        mainCamera = FindObjectOfType<mainCameraController>();

        timer = 300f;
        telaFimCorrida.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        textoTempo.text = DisplayTime(timer);
    }

    private void FixedUpdate()
    {
        AtualizarMoedas();
        if (playerController != null)
        {
            textoQtdAtualBalas.SetText(playerController.GetComponentInChildren<ArmaController>().GetMaxTiro.ToString());
        }
    }

    void AcabouTempo()
    {
        if (timer == 0)
        {
            Debug.Log("Perdeu tudo!!");
        }
    }

    public void PlayerExplodiu(GameObject player)
    {
        if (player.GetComponent<car>() != null)
        {
            mainCamera.estaAtivo = false;
            Destroy(player);
        }
        else
        {
            Destroy(player);
            controlador.StartCoroutine(controlador.RespawnCpu());
        }
    }

    public bool FinalizarCorrida(Collider2D collision, GameObject carro)
    {
        if (collision.gameObject.CompareTag("chegada"))
        {

            CapturarTempoTerminoCorrida(carro);

            controlador.carrosChegaram.Add(carro);
            if (controlador.carrosChegaram.Count > 1)
            {
                controlador.telaFimCorrida.SetActive(true);
                AtualizarAtributosPainelFimCorrida();
                LimparComponentesTela();
            }
            return true;
        }
        return false;
    }

    private void CapturarTempoTerminoCorrida(GameObject carro)
    {
        if (carro.GetComponent<carController>() != null)
        {
            carro.GetComponent<carController>().tempoFinalizacaoCorrida = DisplayTime(timer);
        }
        else
        {
            carro.GetComponent<CpuController>().tempoFinalizacaoCorrida = DisplayTime(timer);
        }
    }

    private IEnumerator RespawnCpu()
    {
        yield return new WaitForSeconds(2);

        Instantiate(prefabCarroCPU, pontoAtualSpawn.transform.position, pontoAtualSpawn.transform.rotation);
    }

    string DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void AtualizarMoedas()
    {
        textoQtdMoedas.text = this.qtdAtualMoedas.ToString();
    }

    public void AtualizarColocacoes()
    {
        TextMeshProUGUI[] colocacoes = panelColocacoes.GetComponentsInChildren<TextMeshProUGUI>();
        GameObject[] posicoes = new GameObject[3];

        posicoes[0] = colocacoes[1].gameObject;
        posicoes[1] = colocacoes[4].gameObject;
        posicoes[2] = colocacoes[7].gameObject;

        switch(carrosChegaram.Count)
        {
            case 1:
                colocacoes[2].text = carrosChegaram[0].GetComponent<PlayerController>().nomePlayer;
                colocacoes[3].text = carrosChegaram[0].GetComponent<PlayerController>().tempoFinalizacaoCorrida;
                    
                posicoes[1].SetActive(false);
                posicoes[2].SetActive(false);
                break;
            case 2:
                colocacoes[2].text = carrosChegaram[0].GetComponent<PlayerController>().nomePlayer;
                colocacoes[3].text = carrosChegaram[0].GetComponent<PlayerController>().tempoFinalizacaoCorrida;
                colocacoes[5].text = carrosChegaram[1].GetComponent<PlayerController>().nomePlayer;
                colocacoes[6].text = carrosChegaram[1].GetComponent<PlayerController>().tempoFinalizacaoCorrida;

                posicoes[2].SetActive(false);
                break;
            case 3:
                colocacoes[2].text = carrosChegaram[0].GetComponent<PlayerController>().nomePlayer;
                colocacoes[3].text = carrosChegaram[0].GetComponent<PlayerController>().tempoFinalizacaoCorrida;
                colocacoes[5].text = carrosChegaram[1].GetComponent<PlayerController>().nomePlayer;
                colocacoes[6].text = carrosChegaram[1].GetComponent<PlayerController>().tempoFinalizacaoCorrida;
                colocacoes[7].text = carrosChegaram[2].GetComponent<PlayerController>().nomePlayer;
                colocacoes[8].text = carrosChegaram[2].GetComponent<PlayerController>().tempoFinalizacaoCorrida;
                break;
            
        }
    }

    public void AtualizarAtributosPainelFimCorrida()
    {
        textoTotalMoedas.text = qtdAtualMoedas.ToString();
        AtualizarColocacoes();
    }

    public void LimparComponentesTela()
    {
        textoTempo.enabled = false;
        textoQtdMoedas.enabled = false;
        iconeMoeda.enabled = false;
    }

    public int AdicionarMoedas
    {
        set
        {
            this.qtdAtualMoedas += value;
        }
    }
}
