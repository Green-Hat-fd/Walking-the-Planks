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
        [Tooltip("L'oggetto la cui posizione sarà resettata")]
        #endregion
        [SerializeField] GameObject obj;
        #region Tooltip()
        [Tooltip("Se attivo, utilizzerà la posizione e rotazione attuale \ndell'oggetto come posizione da usare nel reset")]
        #endregion
        [SerializeField] bool usaQuestoTransform = true;
        #region Tooltip()
        [Tooltip("Inserire la posizione e rotazione scelta \ndove l'oggetto verrà portato nel reset \nsolo se il bool è disattivo")]
        #endregion
        [SerializeField] Vector3 posizIniziale,
                                 rotazIniziale;
        Transform transfIniziale;


        public void ResetTransformObj()
        { 
            obj.transform.position = posizIniziale;
            obj.transform.eulerAngles = rotazIniziale;
        }

        public void ControllaPosizOriginale()
        {
            //Usa la posizione originale dell'obj come posiz. nel reset
            if (usaQuestoTransform)
                transfIniziale = obj.transform;

            posizIniziale = transfIniziale.position;
            rotazIniziale = transfIniziale.eulerAngles;
        }

        public GameObject LeggiObj() => obj;
    }


    #region Tooltip()
    [Tooltip("Ogni oggetto la quale posizione viene portata nella posizIniziale ad ogni reset")]
    #endregion
    [SerializeField] List<ObjReset_Class> objDaResettare;   //Tiene conto di ogni oggetto e della sua posizione iniziale

    //[Space(15)]
    [SerializeField] UnityEvent altroDaResettare;

    [Space(15)]
    [Range(-1000, 0)]
    [SerializeField] float xNegativoReset = -700;


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
            cl.ResetTransformObj();  //Resetta ogni oggetto nella sua posiz. iniziale

            ObjectPoolingScript.ResetTuttiRigidBody(cl.LeggiObj());
        }

        altroDaResettare.Invoke();  //Resetta qualsiasi altra cosa da resettare
    }

    /// <param name="obj_reset">L'oggetto da resettare</param>
    public void ResetSingoloOggetto(GameObject obj_reset)
    {
        foreach (ObjReset_Class cl in objDaResettare)
        {
            //Cerca l'oggetto nella classe
            if (cl.LeggiObj() == obj_reset)
            {
                //Se c'e', fa un reset della posizione, rotazione e del Rigidbody
                cl.ResetTransformObj();
                ObjectPoolingScript.ResetTuttiRigidBody(cl.LeggiObj());
                break;
            }
        }
    }

    private void Update()
    {
        //Controlla ogni oggetto...
        foreach (ObjReset_Class cl in objDaResettare)
        {
            //Se un oggetto si trova in uno spazio negativo fuori dalla mappa,
            //lo ripota al suo posto
            if (cl.LeggiObj().transform.position.y <= xNegativoReset)
            {
                cl.ResetTransformObj();
                ObjectPoolingScript.ResetTuttiRigidBody(cl.LeggiObj());
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.6f, 0.01f, 0.1f);

        Gizmos.DrawWireCube(new Vector3(transform.position.x,
                                        xNegativoReset,
                                        transform.position.z),
                            new Vector3(150f, 0.1f, 150f));
    }
}
