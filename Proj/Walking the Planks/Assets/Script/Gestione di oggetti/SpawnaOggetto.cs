using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnaOggetto : MonoBehaviour
{
    ObjectPoolingScript poolingScr;

    #region Tooltip()
    [Tooltip("Il tag della pool (nella scena) \nda dove prendere l'oggetto")]
    #endregion
    [SerializeField] string tagDellaPool;

    [Space(10)]
    #region Tooltip()
    [Tooltip("Se vero, crea l'oggetto a ripetizione; \nSe falso, ne crea solo uno in tutta la scena \n(e lo ricrea se viene distrutto/disattivato)")]
    #endregion
    [SerializeField] bool ripetizione;

    #region Tooltip()
    [Tooltip("[ ! ]  Solo se la booleana sopra è attiva\n\t-----\nQuanto bisogna aspettare prima che \ncrei un'altra copia (in sec)")]
    #endregion
    [Min(0)]
    [SerializeField] float secDaAspettareSpawn;
    float tempoTrascorso = 0;



    private void Start()
    {
        poolingScr = FindObjectOfType<ObjectPoolingScript>();

        if (!ripetizione)
            CreaOggetto();
    }

    void Update()
    {
        if (ripetizione)
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
