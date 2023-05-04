using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StileSpawn
{
    [InspectorName("All'apertura della scena (Start)")]
    Inizio,
    Ripetizione,
    //[InspectorName("Da script")]
    //Script
}


public class SpawnaOggetto : MonoBehaviour
{
    ObjectPoolingScript poolingScr;

    #region Tooltip()
    [Tooltip("All'apertura: crea l'oggetto a ripetizione; \n\nRipetizione: ne crea solo uno in tutta la scena \n\t(e lo ricrea se viene distrutto/disattivato)")]
    #endregion
    [SerializeField] StileSpawn comeSpawnare = StileSpawn.Inizio;

    [Space(10)]
    #region Tooltip()
    [Tooltip("Il tag della pool (nella scena) \nda dove prendere l'oggetto")]
    #endregion
    [SerializeField] string tagDellaPool;
    #region Tooltip()
    [Tooltip("[ ! ]  Solo se lo stile di spawn è a Ripetizione\n\t-----\nQuanto bisogna aspettare prima che \ncrei un'altra copia (in sec)")]
    #endregion
    [Min(0)]
    [SerializeField] float secDaAspettareSpawn;
    float tempoTrascorso = 0;

    [Space(10)]
    [SerializeField] bool usaQuestaRotazione = true;



    private void Start()
    {
        poolingScr = FindObjectOfType<ObjectPoolingScript>();

        //Crea l'oggetto all'inizio della scena
        //if (comeSpawnare == StileSpawn.Inizio)
            CreaOggetto();
    }

    void Update()
    {
        if (comeSpawnare == StileSpawn.Ripetizione)
        {
            if (tempoTrascorso >= secDaAspettareSpawn)
            {
                //Crea l'oggetto
                CreaOggetto();

                tempoTrascorso = 0;     //Resetta il timer
            }
            else
            {
                tempoTrascorso += Time.deltaTime;   //Aumenta il conteggio del tempo trascorso
            }
        }
    }

    public void CreaOggetto()
    {
        //Lo ruota come l'oggetto se selezionato,
        //se no, usa la rotazione standard
        Quaternion rotazDaUsare = usaQuestaRotazione ? transform.rotation : Quaternion.identity;

        //"Crea" l'oggetto (lo prende dalla pool)
        poolingScr.PrendeOggettoDallaPool(tagDellaPool, transform.position, rotazDaUsare);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black * 0.5f;
        Gizmos.DrawRay(transform.position, transform.forward * 0.5f);   
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
