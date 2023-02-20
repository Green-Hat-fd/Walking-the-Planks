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
    /* TODO:
     * Piattaforme moventi
     *   [X] Un'array di transform, dove prendono ogni e la ciclano a ping-pong (ex. 1-2-3-4-3-2-1-2-3-...)
     *   [X] Velocita'
     *   [ ] Wait time(Vector3 con X= inizio, Y= quelli a metà, Z= fine)
     *   [X] Un'opzione per l'easing (bool easingAttivo + AnimationCurve)
     */
    [SerializeField] StileMovim_EnumT stileMovimento; 

    [Space(20)]
    [SerializeField] Transform[] posizioni;
    [SerializeField] float velPiatt = 1;
    int prossimaPosiz = 0;

    #region Tooltip()
    [Tooltip("X   --> \tTempo di attesa all'inizio \nY   --> \tTempo di attesa per ogni \n\tangolo/posizione in mezzo \nZ   -> \tTempo di attesa alla fine \n")]
    #endregion
    [Space(10)]
    [SerializeField] Vector3 tempoAttesa;

    [Space(15)]
    [SerializeField] bool isEasingAttivo = false;
    [SerializeField] AnimationCurve curvaEasing = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

    bool reverse;

    void Update()
    {
        float distanzaTraPosiz = Vector3.Distance(transform.position, posizioni[prossimaPosiz].position);

        curvaEasing.keys[1].time = distanzaTraPosiz;

        /* TODO: Migliorare l'easing + capire l'interazione con la AnimationCurve
         * TODO: implementare il tempo di attesa (riga 32) 
         */
        float fattEasing = isEasingAttivo
                            ?
                           curvaEasing.Evaluate(distanzaTraPosiz) * velPiatt
                            :
                           velPiatt;

        //Movimento verso la prossima posizione
        transform.position = Vector3.MoveTowards(transform.position,
                                                 posizioni[prossimaPosiz].position,
                                                 velPiatt * fattEasing * Time.deltaTime);

        //Controllo delle posizioni
        if(transform.position == posizioni[prossimaPosiz].position)//distanzaTraPosiz <= 0.1f)
        {
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
        }
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
