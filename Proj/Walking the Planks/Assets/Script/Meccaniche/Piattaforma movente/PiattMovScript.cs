using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StileMovim_EnumT
{
    [InspectorName("Ping-Pong")]
    PingPong,
    Cicla
}

public class PiattMovScript : MonoBehaviour
{
    [SerializeField] bool sonoAttivo;

    [Space(5)]
    [SerializeField] StileMovim_EnumT stileMovimento; 

    [Space(20)]
    [SerializeField] Transform[] posizioni;
    [SerializeField] float velPiatt = 1;
    int prossimaPosiz = 0;

    const float distMinima = 0.05f;

    bool reverse;

    #region Tooltip()
    [Tooltip("X = il tempo di attesa nella prima posizione (indice 0), \nY = Tempo di attesa per ogni posiz. in mezzo, \nZ = Tempo di attesa nell'ultima posiz.")]
    #endregion
    [Space(5)]
    [SerializeField] Vector3 tempoAttesa;
    float tempoTrascorso, quantoDevoAspettare;
    bool stoAspettando;

    #region Non utilizzato
    /*
    [Space(15)]
    [SerializeField] bool isEasingAttivo = false;
    [SerializeField] AnimationCurve curvaEasing = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    */
    #endregion

    [Space(20)]
    [SerializeField] LineRenderer linea;



    private void Awake()
    {
        #region Linea sul percorso

        List<Vector3> pos_temp = new List<Vector3>();

        //Prende le posizioni della piattaforma e le inserisce nella lista
        foreach (Transform t in posizioni)
        {
            pos_temp.Add(t.localPosition);
        }

        //Se si muove ciclando, aggiunge la prima posizione come ultima, creando una linea circolare
        if (stileMovimento == StileMovim_EnumT.Cicla)
            pos_temp.Add(posizioni[0].localPosition);

        //"Disegna" la linea nelle posizioni passate
        linea.positionCount = pos_temp.Count;
        linea.SetPositions(pos_temp.ToArray());

        #endregion
    }

    void Update()
    {
        float distanzaPosiz_Prima = Vector3.Distance(transform.position, posizioni[0].position);
        float distanzaPosiz_Ultima = Vector3.Distance(transform.position, posizioni[posizioni.Length-1].position);

        #region Non utilizzato
        /*float fattEasing = isEasingAttivo
                              ?
                             curvaEasing.Evaluate(distanzaTraPosiz / distMaxPrimaEDopo)
                              :
                             1;
        */
        #endregion


        #region Cambia il tempo di attesa 

        //Capisce in che punto si trova e agisce di conseguenza
        switch (stileMovimento)
        {
            case StileMovim_EnumT.PingPong:
                if (distanzaPosiz_Prima <= distMinima)
                {
                    quantoDevoAspettare = tempoAttesa.x;  //Se si trova nella prima posizione
                }
                else
                {
                    if (distanzaPosiz_Ultima <= distMinima)
                    {
                        quantoDevoAspettare = tempoAttesa.z;  //Se si trova all'ultima posizione
                    }
                    else
                    {
                        quantoDevoAspettare = tempoAttesa.y;  //Se si trova nelle posizioni tra la prima e l'ultima
                    }
                }
                break;

            case StileMovim_EnumT.Cicla:
                quantoDevoAspettare = tempoAttesa.y;  //Prende il tempo di attesa al centro
                break;
        }

        #endregion


        if (sonoAttivo)
        {
            if (stoAspettando)
            {
                //Se e' passato abbastanza tempo...
                if (tempoTrascorso >= quantoDevoAspettare)
                {
                    stoAspettando = false;  //Non aspettare piu', muoviti! (vedi else sotto)
                    tempoTrascorso = 0;     //Resetta il timer
                }
                else
                {

                    tempoTrascorso += Time.deltaTime;  //Aumenta il conteggio del tempo trascorso
                }
            }
            else
            {
                //Movimento verso la prossima posizione (solo se e' attivo)
                transform.position = Vector3.MoveTowards(transform.position,
                                                         posizioni[prossimaPosiz].position,
                                                         velPiatt * Time.deltaTime);
            }
        }


        float distanzaTraPosiz = Vector3.Distance(transform.position, posizioni[prossimaPosiz].position);

        //Controllo delle posizioni (se e' arrivato nella posizione desiderata)
        if (distanzaTraPosiz <= distMinima)
        {
        //    int old_posiz = prossimaPosiz;

            transform.position = posizioni[prossimaPosiz].position;

            switch (stileMovimento)
            {
                #region Ping-Pong
                
                case StileMovim_EnumT.PingPong:
                    if (reverse)
                    {
                        //Torna a fare il giro in avanti se e' arrivato alla fine, se no continua
                        if (prossimaPosiz <= 0)
                            reverse = false;
                        else
                            prossimaPosiz--;
                    }
                    else
                    {
                        //Torna a fare il giro in reverse se e' arrivato all'inizio, se no continua
                        if (prossimaPosiz >= posizioni.Length-1)
                            reverse = true;
                        else
                            prossimaPosiz++;
                    }
                    break;

                #endregion

                #region Cicla

                case StileMovim_EnumT.Cicla:
                        //Se e' arrivato alla fine delle posizioni, allora torna all'inizio
                        if (prossimaPosiz >= posizioni.Length-1)
                            prossimaPosiz = 0;
                        else
                            prossimaPosiz++;

                    break;

                #endregion
            }

        //    distMaxPrimaEDopo = Vector3.Distance(posizioni[old_posiz].position, posizioni[prossimaPosiz].position);

            stoAspettando = true;
        }
    }

    #region Fa entrare e uscire il giocatore o la scatola

    private void OnTriggerStay(Collider other)
    {
        //Se e' entrato il giocatore o la scatola
        if (other.gameObject.CompareTag("Player")
            ||
            other.gameObject.CompareTag("Gun-Box"))
        {
            //Diventa figlio della piattaforma
            other.gameObject.transform.SetParent(gameObject.transform);
            print("ENTRATO");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Se e' uscito il giocatore o la scatola
        if (other.gameObject.CompareTag("Player")
            ||
            other.gameObject.CompareTag("Gun-Box"))
        {
            //Lo toglie dalla piattaforma
            other.gameObject.transform.SetParent(null);
            print("uscito");
        }
    }

    #endregion

    public void AttivaPiattaforma()
    {
        sonoAttivo = true;
    }
    public void DisattivaPiattaforma()
    {
        sonoAttivo = false;
    }
    public void ResetDisattivaPiattaforma()
    {
        sonoAttivo = false;
        prossimaPosiz = 0;
        transform.position = posizioni[0].position;
    }
    public void ResetAttivaPiattaforma()
    {
        sonoAttivo = true;
        prossimaPosiz = 0;
        transform.position = posizioni[0].position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f); //Colore -> Arancione
        
        //Traccia una linea che connette tutte le
        //posizioni nell'array, dal primo all'ultimo
        for (int t = 0; t < posizioni.Length-1; t++)
            Gizmos.DrawLine(posizioni[t].position, posizioni[t+1].position);

        //Se viene scelta l'opzione Cicla, traccia
        //una linea dalla prima all'ultima posizione
        switch (stileMovimento)
        {
            case StileMovim_EnumT.Cicla:
                Gizmos.DrawLine(posizioni[0].position, posizioni[posizioni.Length-1].position);
                break;
        }
    }
}
