using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsanteToggleScript : MonoBehaviour
{
    [SerializeField] Attivazione_Class[] objDaAttivare;

    bool pulsanteToggleAttivo;
    Vector3 posizIniziale;
    ConfigurableJoint joint;


    private void Awake()
    {
        posizIniziale = transform.position;
        joint = GetComponent<ConfigurableJoint>();
    }

    void Update()
    {
        //TODO: Funzioni che cambiano la variabile (questo è il toggle)
        
        //TODO: PS usa ConfigurableJoint.linearLimit.limit per a pressione
        //      e HingeJoint.angle per il toggle (es. angle<10 attivo angle>=10 disattivo) 
    }

    void InvertiIsAttivoPerOgniOggetto()
    {
        foreach (Attivazione_Class act in objDaAttivare)
                act.InvertiAttivo();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(posizIniziale, .01f);
    }
}
