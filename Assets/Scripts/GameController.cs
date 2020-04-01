using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] players;

    public Text HudPrincipal;

    public Text hudVoltas;

    public int voltaAtual = 0;

    public int maximoVoltas = 5;


    private float timer;

    private float delay = 1f;

    private int contador = 4;

          
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;

        foreach (GameObject m_gameObject in players)
        {
            m_gameObject.GetComponent<PlayerController>().enabled = false;

        }
        hudVoltas.text = string.Format("{0}/{1}", voltaAtual, maximoVoltas);
    }

    // Update is called once per frame
    void Update()
    {
        if (contador >= 0)
        {
            if(delay < (Time.time - timer))
            {
                timer = Time.time;
                contador--;
            }

            switch (contador)
            {
                case 4:
                    HudPrincipal.text = "Super Coutry Race";
                    break;
                case 3:
                    HudPrincipal.text = "Ready!";
                    break;
                case 2:
                    HudPrincipal.text = "Set!";
                    break;
                case 1:
                    HudPrincipal.text = "GO!";

                foreach(GameObject m_gameObject in players)
                {
                        m_gameObject.GetComponent<PlayerController>().enabled = true;
                }
                    break;
                case 0:

                    HudPrincipal.text = "";
                    break;
                default:
                    break;
            }
        }
    }


    public void UpdateVoltas(int voltaPlayer) {
        if(voltaPlayer > voltaAtual)
        {
            voltaAtual = voltaPlayer;
            hudVoltas.text = string.Format("{0}/{1}", voltaAtual, maximoVoltas);
        }

    }

    /// <summary>
    /// Funcao que exibe quem foi o ganhador da partida
    /// </summary>
    /// <param name="winner">GameOgbject do ganhador da partida</param>
    
    public void FinishRace(GameObject winner)
    {
        foreach (GameObject m_gameObject in players)
        {
            if (m_gameObject == winner)
                m_gameObject.GetComponent<PlayerController>().enabled = false;
            else
                m_gameObject.SetActive(false);


        }

        HudPrincipal.text = string.Format("PlayerController {0} ganhou!!!", winner.GetComponent<PlayerController>().playerID);
                  

    }

}
