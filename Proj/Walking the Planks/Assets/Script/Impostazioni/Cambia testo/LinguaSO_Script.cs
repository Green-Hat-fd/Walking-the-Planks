using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestoConTag_Class
{
    public string tag;
    [TextArea(2, 7)]
    public string testo;
}

[CreateAssetMenu(menuName = "Scriptable Objects/Lingua (S.O.)", fileName = "NuovaLingua (Lingua)")]
public class LinguaSO_Script : ScriptableObject
{
    Dictionary<string, string> testiDict = new Dictionary<string, string>();

    #region Tooltip()
    [Tooltip("Scrivere il testo nella lingua specificata dal nome dello Scriptable Obj. \n\n/!\\   Associare ad ogni testo un proprio tag")]
    #endregion
    [SerializeField] List<TestoConTag_Class> testiDaModificare;

   

    public void SpostaTestiNelDictionary()
    {
        //Cancella tutto il dictionary
        testiDict.Clear();

        //Trasferisce tutte le informazioni dalla Lista al Dictionary
        foreach (TestoConTag_Class t in testiDaModificare)
        {
            testiDict.Add(t.tag, t.testo);
        }
    }

    /// <summary>
    /// Ritorna il testo rispetto al <b><i>tag</i></b> passato (se esiste)
    /// </summary>
    public string LeggiTesti(string tagDaControllare)
    {
        //Se c'e' nel Dictionary il tag, allora restituisce il testo nella lingua
        return testiDict.ContainsKey(tagDaControllare)
                ?
                testiDict[tagDaControllare]
                :
                null;
    }        
}
