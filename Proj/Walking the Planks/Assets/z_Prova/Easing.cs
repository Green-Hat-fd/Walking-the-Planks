using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easing : MonoBehaviour
{
    [SerializeField] Vector3 posizIniziale,
                             posizFinale;
    
    [Space(7.5f), Range(0, 1)]
    [SerializeField] float dist;

    [Space(15)]
    [Min(0), SerializeField] float num = 0;
    [Min(1), SerializeField] float moltTempo = 1;
    float distWait = 0.2f / 100;
        
    public bool ricomincia;


    void Update()
    {
        transform.position = Vector3.Lerp(posizIniziale, posizFinale, EasingFunz(num));


        dist = Vector3.Distance(transform.position, posizIniziale) / Vector3.Distance(posizIniziale, posizFinale);


        //if (num >= 1 + distWait * moltTempo)
            //ricomincia = true;

        //Aumenta il numero fino a 100, poi resetta
        if (ricomincia)
        {
            num = -distWait * moltTempo;
            ricomincia = false;
        }
        else
            num += (Time.deltaTime * moltTempo) / 100;
    }

    float EasingFunz(float x)
    {
        return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
    }

private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(posizIniziale, posizFinale);
    }
}
