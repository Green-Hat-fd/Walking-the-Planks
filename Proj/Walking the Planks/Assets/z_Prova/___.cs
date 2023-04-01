using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ___ : MonoBehaviour
{


    //controllo colpire è opzionale
    //colori sono entrambi opzionali (non a coppia)
    public static void DisegnaBoxCast/*/DrawBoxCast*/(Vector3 puntoOrigine, Vector3 direzione, float distanzaMax, Vector3 mezzaDimensBoxcast,
                       bool controlloColpire, RaycastHit infoColpito,
                       Color coloreRaggio, Color coloreColpito)
    {
        direzione = direzione.normalized;   //Normalizza la direzione


        Gizmos.color = coloreRaggio;//new Color(0.85f, 0.85f, 0.85f, 1);   //Sistema il colore con quello scelto

        //Disegna il cubo dove ha colpito
        //Gizmos.DrawWireCube(puntoOrigine + (direzione * distanzaMax),
        //                    mezzaDimensBoxcast * 2);
        Gizmos.DrawWireCube(puntoOrigine, puntoOrigine + (direzione * distanzaMax));


        Gizmos.color = coloreColpito;//Color.green;
        if (controlloColpire)
        {
            //Disegna una linea dal punto di inizio fin dove ha colpito
            Gizmos.DrawLine(infoColpito.point + (-direzione*infoColpito.distance), infoColpito.point);

            //Disegna un piccolo cubo dove ha colpito
            Gizmos.DrawCube(infoColpito.point, Vector3.one * 0.1f);
        }
    }


    /// <summary>
    /// Disegna il Raycast e diventa del colore scelto quando colpisce qualcosa
    /// </summary>
    /// <param name="controlloColpire"></param>
    /// <param name="infoColpito">Sarebbe il RaycastHit messo in out dal Raycast</param>
    /// <param name="coloreRaggio">Il colore del Raycast <i>(bianco di default)</i></param>
    /// <param name="coloreColpito">Il colore quando viene colpisce qualcosa</param>
    public static void DisegnaRayCast(Vector3 puntoOrigine, Vector3 direzione, float distanzaMax,
                       bool controlloColpire, RaycastHit infoColpito,
                       Color coloreRaggio, Color coloreColpito)
    {
        direzione = direzione.normalized;   //Normalizza la direzione


            Gizmos.color = coloreRaggio;//new Color(0.85f, 0.85f, 0.85f, 1);

            //Disegna il cubo dove ha colpito
            Gizmos.DrawLine(puntoOrigine, puntoOrigine + (direzione * distanzaMax));


        Gizmos.color = coloreColpito;//Color.green;
        if (controlloColpire)
        {
            //Disegna una linea dalla posizione di inizio alla fine
            //Gizmos.DrawLine(infoColpito.point, puntoOrigine + (direzione * distanzaMax));
            
            //Disegna una linea dal punto di inizio fin dove ha colpito
            Gizmos.DrawLine(puntoOrigine, infoColpito.point);

            //Disegna un piccolo cubo dove ha colpito
            Gizmos.DrawCube(infoColpito.point, Vector3.one * 0.1f);
        }
    }
}
