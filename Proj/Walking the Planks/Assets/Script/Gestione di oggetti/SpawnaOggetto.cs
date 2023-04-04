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

    [Space(10)]
    #region Tooltip()
    [Tooltip("[ ! ]  Solo se lo stile di spawn è a Ripetizione\n\t-----\nQuanto bisogna aspettare prima che \ncrei un'altra copia (in sec)")]
    #endregion
    [Min(0)]
    [SerializeField] float secDaAspettareSpawn;
    float tempoTrascorso = 0;



    private void Start()
    {
        poolingScr = FindObjectOfType<ObjectPoolingScript>();

        if (comeSpawnare == StileSpawn.Inizio)
            CreaOggetto();
    }

    void Update()
    {
        if (comeSpawnare == StileSpawn.Ripetizione)
        {
            if (tempoTrascorso >= secDaAspettareSpawn)
            {
                //Prende l'oggetto dalla pool
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
        poolingScr.PrendeOggettoDallaPool(tagDellaPool, transform.position, Quaternion.identity);
    }
}
