using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsGiocatore : MonoBehaviour
{
    //
    [SerializeField] float secDiAttesa = 1.5f;
    float tempoTrascorso;

    [Space(15)]
    [SerializeField]
    GameObject cameraTerzaPers,
               cameraGiocat;  //La telecamera in prima persona (default nel gioco)

    bool sonoMorto;


    void Update()
    {
        {
            if (sonoMorto)
            {
                //cameraGiocat.SetActive(false);
                //cameraTerzaPers.SetActive(true);

                if (tempoTrascorso >= secDiAttesa)
                {
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
                //Ritorna al checkpoint

                //cameraGiocat.SetActive(true);
                //cameraTerzaPers.SetActive(false);
            }
        }
    }

    #region Funzioni Get custom

    bool LeggiSonoMorto()
    {
        return sonoMorto;
    }

    //void Funzione() { }

    #endregion

    #region Funzioni Set custom

    void ScriviSonoMorto(bool nuovoValore)
    {
        sonoMorto = nuovoValore;
    }

    #endregion
}
