using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NO_LevelManager : MonoBehaviour
{
    #region Tooltip()
    [Tooltip("Ogni oggetto la quale posizione viene portata nel suo punto di spawn ad ogni reset")]
    #endregion
    [SerializeField] List<SpawnaOggetto> objDaResettare;   //Tiene conto di ogni oggetto e della sua posizione iniziale

    [Space(15)]
    [SerializeField] UnityEvent altroDaResettare;


    public void ResetCompleto()
    {
        foreach (SpawnaOggetto spawn in objDaResettare)
        {
            spawn.CreaOggetto();    //Resetta ogni oggetto nella sua posiz. iniziale
        }

        altroDaResettare.Invoke();  //Resetta qualsiasi altra cosa da resettare
    }
}
