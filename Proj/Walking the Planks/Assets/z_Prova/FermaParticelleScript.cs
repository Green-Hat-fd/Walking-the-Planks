using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermaParticelleScript : MonoBehaviour
{
    ParticleSystem part;
    [SerializeField] float tempoDiArrivo = 0.16f;


    private void Awake()
    {
        part = GetComponent<ParticleSystem>();

        //Mette la particella in un determinato tempo e la blocca
        part.Simulate(tempoDiArrivo);
    }
}
