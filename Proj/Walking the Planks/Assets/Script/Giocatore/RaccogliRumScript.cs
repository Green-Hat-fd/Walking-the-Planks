using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RaccogliRumScript : MonoBehaviour
{
    [SerializeField] RumSO_Script rumSO;

    [Header("-- Animazione --")]
    [SerializeField] float altezzaOndulamento = 0.35f;
    [SerializeField] float velOndulamento = 1.5f;
    [SerializeField] float velRotazione = 15f;
    [Space(5)]
    [SerializeField] AudioSource raccolto_sfx;

    Transform objDaMuovere;
    Vector3 posizIniziale;
    Collider coll;



    private void Awake()
    {
        posizIniziale = transform.position;
        objDaMuovere = transform.GetChild(0);
        coll = GetComponent<Collider>();

        rumSO.CambiaRumRaccolto(!gameObject.activeInHierarchy);
    }

    void Update()
    {
        objDaMuovere.position = posizIniziale + transform.up 
                                                * Mathf.Sin(Time.time * velOndulamento)
                                                * altezzaOndulamento;

        objDaMuovere.localEulerAngles += transform.up
                                         * Time.deltaTime
                                         * velRotazione;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Attiva il Rum nel giocatore
            //rumSO.CambiaRumRaccolto(true);
            other.GetComponent<RumScript>().enabled = true;

            //Feedback
            raccolto_sfx.Play();

            //Si auto-disattiva
            objDaMuovere.gameObject.SetActive(false);
            coll.enabled = false;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Vector3 sopra = transform.position + transform.up * altezzaOndulamento,
                sotto = transform.position - transform.up * altezzaOndulamento;
        float raggio = 0.05f;


        Gizmos.color = Color.white;
        Gizmos.DrawLine(sotto, sopra);
        Gizmos.DrawSphere(sotto, raggio);
        Gizmos.DrawSphere(sopra, raggio);
    }
}
