using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaScript : MonoBehaviour
{
    Animator portaAnim;


    private void Awake()
    {
        if(GetComponent<Animator>())
            portaAnim = GetComponent<Animator>();
        else
            portaAnim = GetComponentInChildren<Animator>();
    }

    public void ApriPorta()
    {
        portaAnim.SetBool("isAperta", true);
    }

    public void ChiudiPorta()
    {
        portaAnim.SetBool("isAperta", false);
    }
}
