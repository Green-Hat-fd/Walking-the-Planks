using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio3D : MonoBehaviour
{
    [Min(0)]
    [SerializeField] float velRotaz = 2;

    [Space(10)]
    [SerializeField] bool continuoCrescere;



    void Update()
    {
        transform.Rotate(0, velRotaz * Time.deltaTime, 0);

        if(continuoCrescere)
           velRotaz += Time.deltaTime * 2;
    }
}
