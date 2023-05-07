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
    Rigidbody[] rb_Ragdoll;
    CharacterJoint[] charJoint_Ragdoll;
    Collider[] collider_Ragdoll;

    //La telecamera visibile quando il giocat. muore
    [SerializeField] GameObject cameraTerzaPers;
    
    //La telecamera in prima persona (default nel gioco)
    [SerializeField] GameObject cameraGiocat;
    
    //L'empty che raggruppa la telecamera in terza pers. e il Ragdoll
    GameObject gruppoMorto;
    

    MovimGiocatRb movimGiocatScr;
    
    [SerializeField] float velCamTerzaPers = 10f;

    bool sonoMorto;

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

        rb_Ragdoll = ragdollDaSeguire.GetComponentsInChildren<Rigidbody>();
        charJoint_Ragdoll = ragdollDaSeguire.GetComponentsInChildren<CharacterJoint>();
        collider_Ragdoll = ragdollDaSeguire.GetComponentsInChildren<Collider>();
        ragdollAnim.enabled = true;

        gruppoMorto = cameraTerzaPers.transform.parent.gameObject;
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
        CambiaComponentiRagdoll(false);

        //Torna alla telecamera in prima persona
        cameraTerzaPers.SetActive(false);
    }

    void CambiaComponentiRagdoll(bool valore)
    {
        foreach (CharacterJoint joint in charJoint_Ragdoll)
        {
            joint.enableCollision = valore;
        }
        foreach (Collider col in collider_Ragdoll)
        {
            col.enabled = valore;
        }
        foreach (Rigidbody rb in rb_Ragdoll)
        {
            rb.detectCollisions = valore;
            rb.useGravity = valore;
        }

        ragdollAnim.enabled = !valore;
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
