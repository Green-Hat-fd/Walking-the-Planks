using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiaScenaScript : MonoBehaviour
{
    [SerializeField] OpzioniSO_Script opzSO;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            opzSO.ScenaSuccessiva();
        }
    }
}
