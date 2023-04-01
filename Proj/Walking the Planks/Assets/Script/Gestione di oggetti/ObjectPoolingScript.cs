using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool_Class
{
    public string tag;
    public GameObject obj;
    public int dimensione;
}

public class ObjectPoolingScript : MonoBehaviour
{
    Dictionary<string, Queue<GameObject>> poolDict;

    [SerializeField] List<Pool_Class> tutteLePool;


    void Awake()
    {
        //Crea un nuovo Dictionary vuoto
        poolDict = new Dictionary<string, Queue<GameObject>>();


        //Prende ogni pool nella lista
        foreach (Pool_Class p in tutteLePool)
        {
            Queue<GameObject> obj_queue = new Queue<GameObject>();  //Crea una nuova Coda vuota
            GameObject empty = new GameObject(p.tag);  //Crea un nuovo obj vuoto con il tag come nome

            empty.transform.SetParent(transform);

            //Crea tanti oggetti quanto e' la dimensione e li mette nella pool
            for (int i = 0; i < p.dimensione; i++)
            {
                GameObject pref = Instantiate(p.obj);

                pref.SetActive(false);                      //Viene disattivato
                pref.transform.SetParent(empty.transform);  //Lo mette figlio della nuova empty

                obj_queue.Enqueue(pref);
            }

            //Aggiunge nel dizionario la coda appena creata
            poolDict.Add(p.tag, obj_queue);
        }
    }


    /// <summary>
    /// Prende un qualsiasi oggetto dalla pool indicata dal <i><b>tag</b></i> data la posizione e rotazione
    /// </summary>
    /// <param name="poolTag">Nome/Tag della pool dove prendere l'oggetto</param>
    /// <returns></returns>
    public GameObject PrendeOggettoDallaPool(string poolTag, Vector3 pos, Quaternion rot)
    {
        //Controlla se c'e' l'oggetto nel dizionario
        if (poolDict.ContainsKey(poolTag))
        {
            //Prende l'oggetto riferito alla tag e lo toglie dalla coda
            GameObject obj = poolDict[poolTag].Dequeue();


            //Riposiziona, ruota, resetta il Rigidbody e attiva l'oggetto
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            ResetTuttiRigidBody(obj);
            obj.SetActive(true);


            //Lo rimette nella coda
            poolDict[poolTag].Enqueue(obj);


            return obj;
        }
        else
            return null;
    }


    /// <summary>
    /// Rimette l'oggetto <i><b>obj</b></i> nella pool indicata dal <i><b>tag</b></i>
    /// </summary>
    public void RiAggiungiOggetto(string poolTag, GameObject obj)
    {
        //Controlla se c'e' l'oggetto nel dizionario
        if (poolDict.ContainsKey(poolTag))
        {
            obj.SetActive(false);   //Lo disattiva

            poolDict[poolTag].Enqueue(obj);   //Lo rimette in coda
        }
    }

    public static void ResetTuttiRigidBody(GameObject objDaReset)
    {
        //Controlla se l'obj ha il RigidBody e resetta la sua velocita'
        Rigidbody rb_obj = objDaReset.GetComponent<Rigidbody>();

        if (rb_obj)
        {
            rb_obj.velocity = Vector3.zero;
            rb_obj.angularVelocity = Vector3.zero;
        }


        //Controlla se sono i figli ad avere il RigidBody e resetta la loro velocita'
        Rigidbody[] rb_child = objDaReset.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rb in rb_child)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
