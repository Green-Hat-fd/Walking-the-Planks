using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsGiocatore : MonoBehaviour
{
    [SerializeField] float secDiAttesa = 1.5f;
    float tempoTrascorso;

    [Space(15)]
    [SerializeField] GameObject ragdoll;
    [SerializeField] Transform ragdollDaSeguire;
    Animator ragdollAnim;
    Rigidbody[] ragdoll_gruppoRb;
    CharacterJoint[] ragdoll_gruppoCharJoint;
    Collider[] ragdoll_gruppoCollider;

    //La telecamera visibile quando il giocat. muore
    [SerializeField] GameObject cameraTerzaPers;
    
    //La telecamera in prima persona (default nel gioco)
    [SerializeField] GameObject cameraGiocat;
    

    MovimGiocatRb movimGiocatScr;
    
    [SerializeField] float velCamTerzaPers = 10f;

    bool sonoMorto;

    [Space(15)]
    [SerializeField] Animator pistolaAnim;
    [SerializeField] Animator rumAnim;

    [Space(15), Range(-1000, 0)]
    [SerializeField] float xNegativoReset = -700;

    [Space(15)]
    [SerializeField] CheckpointSO_Script checkpoint;
    LevelManagerScript levelManagerScr;




    private void Start()
    {
        movimGiocatScr = GetComponent<MovimGiocatRb>();
        levelManagerScr = FindObjectOfType<LevelManagerScript>();

        ragdollAnim = ragdoll.GetComponent<Animator>();

        ragdoll_gruppoRb = ragdollDaSeguire.GetComponentsInChildren<Rigidbody>();
        ragdoll_gruppoCharJoint = ragdollDaSeguire.GetComponentsInChildren<CharacterJoint>();
        ragdoll_gruppoCollider = ragdollDaSeguire.GetComponentsInChildren<Collider>();
        
        //Resetta il ragdoll
        ResetPosizioneRotazioneOssaRagdoll();
        ragdollAnim.enabled = true;
    }

    void Update()
    {
        if (sonoMorto)
        {
            GetComponent<Rigidbody>().drag = 0;

            cameraGiocat.SetActive(false);
            movimGiocatScr.enabled = false;
            
            EntraModalitaRagdoll();

            //Ruota la camera in terza persona attorno al giocatore
            cameraTerzaPers.transform.Rotate(0, velCamTerzaPers * Time.deltaTime, 0);


            if (tempoTrascorso >= secDiAttesa)
            {
                //Ritorna al checkpoint
                transform.position = checkpoint.LeggiPosizioneCheckpoint();

                //Resetta il RigidBody del giocatore
                ObjectPoolingScript.ResetTuttiRigidBody(gameObject);

                //Resetta tutto il livello
                levelManagerScr.ResetCompleto();

                //Resetta le animazioni per i modelli della pistola e del Rum
                pistolaAnim.SetTrigger("Reset Animazione");
                rumAnim.SetTrigger("Reset Animazione");


                sonoMorto = false;
                tempoTrascorso = 0;
            }
            else
            {
                tempoTrascorso += Time.deltaTime;
            }
        }
        else
        {
            cameraGiocat.SetActive(true);
            movimGiocatScr.enabled = true;

            EsciModalitaRagdoll();

            if(tempoTrascorso != 0)
                tempoTrascorso = 0;   //Reset -- misura di sicurezza
        }

        cameraTerzaPers.transform.position = ragdollDaSeguire.position;

        //Controlla se il giocatore si trova in uno spazio
        //negativo fuori dalla mappa, e lo fa morire
        if (transform.position.y <= xNegativoReset)
            sonoMorto = true;
    }

    #region Controllo Ragdoll

    void EntraModalitaRagdoll()
    {
        //Cambia la telecamera
        cameraTerzaPers.SetActive(true);

        //Attiva il ragdoll, attivando e tutte le sue componenti
        ragdoll.SetActive(true);
        CambiaComponentiRagdoll(true);
    }
    void EsciModalitaRagdoll()
    {
        //Disttiva il ragdoll, disattivando e tutte le sue componenti
        ResetPosizioneRotazioneOssaRagdoll();
        CambiaComponentiRagdoll(false);

        //Torna alla telecamera in prima persona
        cameraTerzaPers.SetActive(false);
    }

    void CambiaComponentiRagdoll(bool valore)
    {
        foreach (CharacterJoint joint in ragdoll_gruppoCharJoint)
        {
            joint.enableCollision = valore;
        }
        foreach (Collider col in ragdoll_gruppoCollider)
        {
            col.enabled = valore;
        }
        foreach (Rigidbody rb in ragdoll_gruppoRb)
        {
            rb.detectCollisions = valore;
            rb.useGravity = valore;
        }

        #region --Non usato--
        //GetComponent<Rigidbody>().detectCollisions = !valore;
        //GetComponent<Rigidbody>().useGravity = !valore; 
        #endregion

        ragdollAnim.enabled = !valore;
    }

    void ResetPosizioneRotazioneOssaRagdoll()
    {
        ragdollDaSeguire.localPosition = Vector3.zero;
        ragdollDaSeguire.localRotation = Quaternion.identity;
    }

    #endregion


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        
        Gizmos.DrawWireCube(new Vector3(transform.position.x,
                                    xNegativoReset,
                                    transform.position.z),
                        new Vector3(100f, 0.1f, 100f));
    }

    #region Funzioni Get custom

    public bool LeggiSonoMorto() => sonoMorto;

    //void Funzione() { }

    #endregion

    #region Funzioni Set custom

    public void ScriviSonoMorto(bool nuovoValore)
    {
        sonoMorto = nuovoValore;
    }

    #endregion
}
