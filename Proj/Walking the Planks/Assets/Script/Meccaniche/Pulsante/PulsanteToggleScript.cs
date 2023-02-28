using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PulsanteToggleScript : MonoBehaviour
{
    [SerializeField] HingeJoint joint;

    #region Tooltip()
    [Tooltip("La rotazione [-45, 45] per attivare il pulsante")]
    #endregion
    [Space(10), Range(-45, 45)]
    [SerializeField] float sogliaAttivazione = 7.5f;

    [Space(15)]
    [SerializeField] UnityEvent onActivated,
                                onDeactivated;


    private void Awake()
    {
        //Prende il componente Joint se esso non e' stato assegnato
        //(da se' stesso o dai figli)
        if (joint == null)
        {
            if(GetComponent<HingeJoint>())
                joint = GetComponent<HingeJoint>();
            else
                if(GetComponentInChildren<HingeJoint>())
                    joint = GetComponentInChildren<HingeJoint>();
        }
    }

    void Update()
    {
        //Controlla se il bottone e' stato premuto abbastanza
        if (joint.angle >= sogliaAttivazione)
        {
            //Attiva ogni oggetto di conseguenza
            onActivated.Invoke();
        }
        else
        {
            //Lo disattiva se viene rilasciato
            onDeactivated.Invoke();
        }
    }
}
