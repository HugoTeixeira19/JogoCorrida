using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Controlador : MonoBehaviour
{
    public static Controlador controlador;
    public GameObject pontoAtualSpawn;
    private carController playerController;
    public GameObject prefabCarro;

    public float timer;
    public Text text;

    private mainCameraController mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        if(controlador == null)
        {
            controlador = FindObjectOfType<Controlador>();
        }
        playerController = FindObjectOfType<carController>();
        mainCamera = FindObjectOfType<mainCameraController>();

        timer = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        DisplayTime(timer);
    }

    void AcabouTempo()
    {
        if (timer == 0)
        {
            Debug.Log("Perdeu tudo!!");
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void PlayerExplodiu(carController player)
    {
        Destroy(player.gameObject);
        mainCamera.estaAtivo = false;
        controlador.StartCoroutine(controlador.RespawnPlayer());
    }

    public int tempoSpawn = 2;
    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(tempoSpawn);
        Debug.Log("Respawn Player");

        mainCamera.carPersonagem = Instantiate(Resources.Load("Prefabs/CarroPrefab"), pontoAtualSpawn.transform.position, pontoAtualSpawn.transform.rotation) as GameObject;
        mainCamera.estaAtivo = true;
    }
}
