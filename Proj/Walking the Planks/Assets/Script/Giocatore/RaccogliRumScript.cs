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

    Transform modelloDaMuovere;
    Vector3 posizIniziale;
    Collider coll;



    private void Awake()
    {
        posizIniziale = transform.position;
        modelloDaMuovere = transform.GetChild(0);
        coll = GetComponent<Collider>();

        //Si auto-disattiva se lo si ha raccolto
        //o si auto-attiva se NON lo si ha ancora raccolto
        modelloDaMuovere.gameObject.SetActive(!rumSO.LeggiRaccolto());
        coll.enabled = !rumSO.LeggiRaccolto();
    }

    void Update()
    {
        modelloDaMuovere.position = posizIniziale + transform.up 
                                                * Mathf.Sin(Time.time * velOndulamento)
                                                * altezzaOndulamento;

        modelloDaMuovere.localEulerAngles += transform.up
                                         * Time.deltaTime
                                         * velRotazione;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Attiva il Rum nel giocatore
            rumSO.CambiaRumRaccolto(true);

            //Feedback
            raccolto_sfx.Play();
            other.GetComponent<RumScript>().AnimazioneRaccolto();

            //Si auto-disattiva
            modelloDaMuovere.gameObject.SetActive(false);
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
