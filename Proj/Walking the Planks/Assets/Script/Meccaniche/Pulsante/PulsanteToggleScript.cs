using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PulsanteToggleScript : MonoBehaviour
{
    [SerializeField] HingeJoint joint;
    JointSpring _jointSpring;

    #region Tooltip()
    [Tooltip("La rotazione [-45, 45] per attivare il pulsante")]
    #endregion
    [Space(10), Range(-45, 45)]
    [SerializeField] float sogliaAttivazione = 7.5f;

    [Space(15)]
    [SerializeField] UnityEvent onActivated,
                                onDeactivated;

    [Header("—  Feedback  —")]
    [SerializeField] AudioSource puls_sfxSource;
    [SerializeField] AudioClip pulsPremuto_sfx,
                               pulsRilasciato_sfx;
    float doOnce_sfx = 0;

    bool attivato = false;


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

        _jointSpring = joint.spring;
    }

    void Update()
    {
        //Controlla se il bottone e' stato premuto
        attivato = joint.angle <= sogliaAttivazione;


        if (attivato)
        {
            //Lo manda verso il limite massimo
            _jointSpring.targetPosition = joint.limits.min;
            
            //Attiva ogni oggetto di conseguenza
            onActivated.Invoke();

            SFX_Premi();        //Feedback sonoro
        }
        else
        {
            //Lo manda verso il limite minimo
            _jointSpring.targetPosition = joint.limits.max;
            
            //Lo disattiva se viene rilasciato
            onDeactivated.Invoke();

            SFX_Rilascia();     //Feedback sonoro
        }

        joint.spring = _jointSpring;
    }


    void SFX_Premi()
    {
        //Controlla se DoOnce è a 0
        if (doOnce_sfx <= 0)
        {
            //Riproduce il suono del pulsante attivato
            puls_sfxSource.PlayOneShot(pulsPremuto_sfx);

            doOnce_sfx = 1;       //Porta il DoOnce a 1 -> per il suono quando Rilascia
        }
    }
    void SFX_Rilascia()
    {
        //Controlla se DoOnce è a 0
        if (doOnce_sfx >= 1)
        {
            //Riproduce il suono del pulsante disattivato
            puls_sfxSource.PlayOneShot(pulsRilasciato_sfx);

            doOnce_sfx = 0;       //Porta il DoOnce a 0 -> per il suono quando Preme
        }
    }
}
