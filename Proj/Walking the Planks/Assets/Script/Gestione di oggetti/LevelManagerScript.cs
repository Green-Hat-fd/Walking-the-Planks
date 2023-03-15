using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManagerScript : MonoBehaviour
{
    [System.Serializable]
    class ObjReset_Class
    {
        #region Tooltip()
        [Tooltip("Se attivo, utilizzerà la posizione attuale dell'oggetto \ncome posizione da usare nel reset")]
        #endregion
        [SerializeField] bool usaQuestaPosiz = true;
        #region Tooltip()
        [Tooltip("L'oggetto la cui posizione sarà resettata")]
        #endregion
        [SerializeField] GameObject obj;
        #region Tooltip()
        [Tooltip("Inserire la posizione scelta dove \nl'oggetto verrà portato nel reset \nsolo se il bool è disattivo")]
        #endregion
        [SerializeField] Vector3 posizIniziale;


        public void ResetPosizioneObj() { obj.transform.position = posizIniziale; }

        public void ControllaPosizOriginale()
        {
            //Usa la posizione originale dell'obj come posiz. nel reset
            if (usaQuestaPosiz)
                posizIniziale = obj.transform.position;
        }

        public GameObject LeggiObj() => obj;
    }


    #region Tooltip()
    [Tooltip("Ogni oggetto la quale posizione viene portata nella posizIniziale ad ogni reset")]
    #endregion
    [SerializeField] List<ObjReset_Class> objDaResettare;   //Tiene conto di ogni oggetto e della sua posizione iniziale

    [Space(15)]
    [SerializeField] UnityEvent altroDaResettare;


    void Awake()
    {
        //Controlla se devo utilizzare la posiz. attuale
        //dell'oggetto come posizione iniziale
        foreach (ObjReset_Class cl in objDaResettare)
            cl.ControllaPosizOriginale();
    }

    public void ResetCompleto()
    {
        foreach (ObjReset_Class cl in objDaResettare)
        {
            cl.ResetPosizioneObj();  //Resetta ogni oggetto nella sua posiz. iniziale

            ObjectPoolingScript.ResetTuttiRigidBody(cl.LeggiObj());
        }

        altroDaResettare.Invoke();  //Resetta qualsiasi altra cosa da resettare
    }
}
