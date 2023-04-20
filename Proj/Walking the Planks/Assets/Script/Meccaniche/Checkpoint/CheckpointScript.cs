using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointScript : MonoBehaviour, IFeedback
{
    [SerializeField] CheckpointSO_Script checkpSO;

    [Tooltip("Il numero di questo checkpoint"), Space(10)]
    [SerializeField] int _numeroCheckpoint;

    [Space(10)]
    [SerializeField] Vector3 offset;

    [Header("—  Feedback  —")]
    [SerializeField] AudioSource raggiunto_sfx;



    private void Awake()
    {
        //Prende il giocatore e lo rimette nella posizione dell'ultimo checkpoint
        GameObject giocat = GameObject.FindGameObjectWithTag("Player");
        giocat.transform.position = checkpSO.LeggiPosizioneCheckpoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Controlla che sia il giocatore a entrare & che non l'ha gia' preso
        if (other.gameObject.CompareTag("Player")
            &&
            checkpSO.LeggiNumCheckpoint() != _numeroCheckpoint)
        {
            //Prende l'indice della scena attuale
            int indiceScena = SceneManager.GetActiveScene().buildIndex;

            //Aggiorna il conteggio e la posizione del checkpoint
            checkpSO.CambiaCheckpoint(indiceScena, _numeroCheckpoint, transform.position + offset);


            Feedback();
        }
    }

    public void Feedback()
    {
        //Riproduce il suono quando viene raggiunto un checkpoint
        raggiunto_sfx.Play();

        //TODO: feedback (particelle(?))
    }

    private void OnDrawGizmos()
    {
        #region OLD_Non usato
        //Mostra il punto con un'icona
        //Gizmos.DrawIcon(transform.position + offset, "sv_icon_dot8_pix16_gizmo", true, Color.green);
        #endregion

        //Mostra il punto con un pallino
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.position + offset, 0.05f);
    }
}
