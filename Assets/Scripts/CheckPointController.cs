using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsavel por controllar as voltas do jogador.
/// </summary>
/// 

public class CheckPointController : MonoBehaviour
{
    public GameController gameController;

    private Transform[] checkpoints;

    private int maxCheckPoints = 0;
    private int checkpointAtual = 0;

    private int voltaAtual = 0;

    private int maximoVoltas;

    private void Awake()
    {
        GameObject checkpointContainer = GameObject.FindGameObjectWithTag("CheckpointsContainer");

        checkpoints = new Transform[checkpointContainer.transform.childCount];
        for (int i = 0; i < checkpointContainer.transform.childCount; i++)
        {
            checkpoints[i] = checkpointContainer.transform.GetChild(i);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        maximoVoltas = gameController.maximoVoltas;
        maxCheckPoints = checkpoints.Length - 1;
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Checkpoint")) {
            if (other.transform == checkpoints[checkpointAtual])  {
                if (checkpointAtual < maxCheckPoints)  {
                    if (checkpointAtual == 0) {   
                    
                        if (voltaAtual == maximoVoltas) {
                            gameController.FinishRace(gameObject);
                        }
                        else
                        {
                            voltaAtual++;
                            gameController.UpdateVoltas(voltaAtual);
                        }
                    }
                    checkpointAtual++;
                }
            
                   else
                  {
                     checkpointAtual = 0;

                   }
            }
        }
    }
}
