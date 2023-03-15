using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Checkpoint (S.O.)", fileName = "Checkpoint_SO")]
public class CheckpointSO_Script : ScriptableObject
{
    [Tooltip("Il numero della scena (espresso in indice)")]
    [SerializeField] int livello;
    
    [Space(7.5f)]
    [SerializeField] int numCheckpoint = 0;
    [SerializeField] Vector3 posizCheckpoint;



    #region Funzioni Set custom

    public void CambiaCheckpoint(int scena, int numCheck, Vector3 posCheck)
    {
        livello = scena;
        numCheckpoint = numCheck;
        posizCheckpoint = posCheck;
    }

    public void ScriviLivello(int nuovoNum)
    {
        livello = nuovoNum;
    }
    public void ScriviNumCheckpoint(int nuovoNum)
    {
        numCheckpoint = nuovoNum;
    }

    #endregion

    #region Funzioni Get custom

    public int LeggiLivello() => livello;
    public int LeggiNumCheckpoint() => numCheckpoint;
    public Vector3 LeggiPosizioneCheckpoint() => posizCheckpoint;

    #endregion
}
