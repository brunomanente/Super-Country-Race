using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public int playerID;
    public GameObject corpo;

    public Transform mira;
    public Rigidbody bomba;

    public AudioSource audioMovimento;
    public AudioClip veiculoParado;
    public AudioClip veiculoAndando;
    public AudioSource audioTiro;

    private Rigidbody rb;
    public float velocidade = 12f;
    public float velocidadeGiro = 180f;
    private float inputValorMovimento;
    private float inputValorGiro;
    public float forcaTiro = 20f;

    private string nomeEixoMovimento;
    private string nomeEixoGiro;
    private string botaoTiro;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        Color color = Color.white;
        switch (playerID)
        {
            case 1:
                color = Color.red;
                break;
            case 2:
                color = Color.blue;
                break;
        }
        corpo.GetComponent<MeshRenderer>().material.color = color;
    }

    void Start()
    {
        nomeEixoMovimento = "Vertical" + playerID;
        nomeEixoGiro = "Horizontal" + playerID;
        botaoTiro = "Fire" + playerID;

        rb.isKinematic = false;
        inputValorMovimento = 0f;
        inputValorGiro = 0f;
    }

    private void Update()
    {

        inputValorMovimento = Input.GetAxis(nomeEixoMovimento);
        inputValorGiro = Input.GetAxis(nomeEixoGiro);

        Move();
        Turn();

        if (Input.GetButtonDown(botaoTiro))
            Fire();

        AudioMotor();
    }

    private void Move()
    {
        Vector3 movemento = transform.forward * inputValorMovimento * velocidade * Time.deltaTime;
        rb.MovePosition(rb.position + movemento);
    }

    private void Turn()
    {
        float giro = inputValorGiro * velocidadeGiro * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, giro, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    private void Fire()
    {
        Rigidbody bombaInstance = Instantiate(bomba, mira.position, mira.rotation) as Rigidbody;
        bombaInstance.velocity = forcaTiro * mira.forward;
        audioTiro.Play();
    }

    private void AudioMotor()
    {
        if (Mathf.Abs(inputValorMovimento) < 0.1f && Mathf.Abs(inputValorGiro) < 0.1f)
        {
            if (audioMovimento.clip == veiculoAndando)
            {
                audioMovimento.clip = veiculoParado;
                audioMovimento.Play();
            }
        }
        else
        {
            if (audioMovimento.clip == veiculoParado)
            {
                audioMovimento.clip = veiculoAndando;
                audioMovimento.Play();
            }
        }
    }
}