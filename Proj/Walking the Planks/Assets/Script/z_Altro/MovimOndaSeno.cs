using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovimOndaSeno : MonoBehaviour
{
    [SerializeField] bool attivo = true;

    [Space(10)]
    [SerializeField] Vector2 limiti;
    [Min(0)]
    [SerializeField] float velocita;

    enum AsseMovim_Enum
    {
        asseX,
        asseY,
        asseZ,
    }
    [Space(10)]
    [SerializeField] AsseMovim_Enum asseMovimento;

    Vector3 posizRef;
    float tempoPassato_ondaSeno = 0;



    private void Awake()
    {
        posizRef = transform.position;

    }

    void Update()
    {
        if (attivo)
        {
            switch (asseMovimento)
            {
                case AsseMovim_Enum.asseX:
                    transform.position = posizRef + transform.right * MovimentoTramiteOndaSeno();
                    break;
                    
                case AsseMovim_Enum.asseY:
                    transform.position = posizRef + transform.up * MovimentoTramiteOndaSeno();
                    break;
                    
                case AsseMovim_Enum.asseZ:
                    transform.position = posizRef
                                         + transform.forward * MovimentoTramiteOndaSeno();
                    break;
            }

            tempoPassato_ondaSeno += Time.deltaTime;
        }
    }

    float MovimentoTramiteOndaSeno()
    {
        float m = 1,  //la mezza ampiezza dell'onda
              q = 0,  //spostamento del centro delll'onda sull'asse Y
              dist, maggiore;

        //Calcola la M
        dist = Mathf.Abs(limiti.y - limiti.x); //Calcola l'ampiezza dell'onda
        m = dist / 2.0f;

        //Calcola la Q
        maggiore = Mathf.Abs(limiti.y) > Mathf.Abs(limiti.x) ? limiti.y : limiti.x; //Prende il maggiore tra |min| e |max|
        q = maggiore > 0 ? maggiore - m : maggiore + m; //Calcola la distanza dall'origine (0; 0)


        return (m * Mathf.Sin(tempoPassato_ondaSeno * velocita)) + q;  //Ritorna la nuova onda seno
    }

    private void OnDrawGizmos()
    {
        limiti.x = Mathf.Clamp(limiti.x, limiti.x, 0);
        limiti.y = Mathf.Clamp(limiti.y, 0, limiti.y);


        Vector3 punto = Application.isPlaying ? posizRef : transform.position;

        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(posizRef, 0.1f);

        switch (asseMovimento)
        {
            case AsseMovim_Enum.asseX:
                Gizmos.color = Color.red;
                Gizmos.DrawLine(punto + transform.right * limiti.x,
                                punto + transform.right * limiti.y);
                break;
                    
            case AsseMovim_Enum.asseY:
                Gizmos.color = Color.green;
                Gizmos.DrawLine(punto + transform.up * limiti.x,
                                punto + transform.up * limiti.y);
                break;
                    
            case AsseMovim_Enum.asseZ:
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(punto + transform.forward * limiti.x,
                                punto + transform.forward * limiti.y);
                break;
        }
    }

    public void AccendiMovimOnda()
    {
        attivo = true;
    }
    public void SpegniMovimOnda()
    {
        attivo = false;
    }
}
