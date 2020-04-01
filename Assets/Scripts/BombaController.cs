using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaController : MonoBehaviour
{
    public LayerMask playerLayer;

    public ParticleSystem particulaExplosao;

    public AudioSource audioExplosao;

    public float forcaExplosao = 500f;

    public float areaExplosao = 5f;

    public float tempoDeVida = 2f;



    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, tempoDeVida);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, areaExplosao, playerLayer);

        foreach (Collider c in colliders)
        {
            Rigidbody targetRigidbody = c.GetComponent<Rigidbody>();

            if (!targetRigidbody)
                continue;
            targetRigidbody.AddExplosionForce(forcaExplosao, transform.position, areaExplosao);
        }

        particulaExplosao.transform.parent = null;

        particulaExplosao.Play();

        audioExplosao.Play();

        ParticleSystem.MainModule mainModule = particulaExplosao.main;
        Destroy(particulaExplosao.gameObject, mainModule.duration);

        Destroy(gameObject);


    }

}

