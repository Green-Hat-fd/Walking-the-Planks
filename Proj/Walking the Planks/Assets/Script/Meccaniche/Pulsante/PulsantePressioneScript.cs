using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PulsantePressioneScript : MonoBehaviour
{
    Vector3 posizIniziale;
    [SerializeField] ConfigurableJoint joint;
    [SerializeField] Transform bottoneAttivatore;

    #region Tooltip()
    [Tooltip("La percentuale [0, 1] per attivare il pulsante \n\n(0 = sempre attivo, sconsigliato)\n(1 = attivo solo se tocca il fondo)")]
    #endregion
    [Space(10), Range(0, 1)]
    [SerializeField] float sogliaAttivazione = 0.65f;

    [Space(15)]
    [SerializeField] UnityEvent onPressed,
                                onReleased;


    private void Awake()
    {
        //Prende il componente Joint se esso non e' stato assegnato
        //(da se' stesso o dai figli)
        if (joint == null)
        {
            if (GetComponent<ConfigurableJoint>())
                joint = GetComponent<ConfigurableJoint>();
            else
                if (GetComponentInChildren<ConfigurableJoint>())
                joint = GetComponentInChildren<ConfigurableJoint>();
        }

        //Memorizza il riferimento al bottone in se' come l'oggetto del joint
        if (bottoneAttivatore == null)
            bottoneAttivatore = joint.transform;

        //Memorizza la posizione iniziale
        posizIniziale = bottoneAttivatore.position;
    }

    void Update()
    {
        //Calcolo di quanto il bottone e' stato premuto/calato
        float percBottone = Vector3.Distance(posizIniziale, bottoneAttivatore.position);
        percBottone /= joint.linearLimit.limit;


        //Controlla se il bottone e' stato premuto abbastanza in fondo
        if (percBottone >= sogliaAttivazione)
        {
            //Attiva ogni oggetto di conseguenza
            onPressed.Invoke();
        }
        else
        {
            //Lo disattiva se viene rilasciato
            onReleased.Invoke();
        }
    }
}
