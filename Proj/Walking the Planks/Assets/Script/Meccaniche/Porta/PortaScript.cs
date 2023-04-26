using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaScript : MonoBehaviour
{
    Animator portaAnim;

    AudioSource porta_sfxSource;
    [Space(10)]
    [SerializeField] AudioClip portaApre_sfx;
    [SerializeField] AudioClip portaChiude_sfx;
    int doOnce_sfx = 0;



    private void Awake()
    {
        if(GetComponent<Animator>())
            portaAnim = GetComponent<Animator>();
        else
            portaAnim = GetComponentInChildren<Animator>();

        porta_sfxSource = GetComponent<AudioSource>();
    }

    public void ApriPorta()
    {
        portaAnim.SetBool("isAperta", true);
        

        //Controlla se DoOnce è a 0
        if (doOnce_sfx <= 0)
        {
            //Riproduci il rumore dell'apertura
            porta_sfxSource.PlayOneShot(portaApre_sfx);

            doOnce_sfx = 1;       //Porta il DoOnce a 1 -> per il suono quando Chiude
        }
    }

    public void ChiudiPorta()
    {
        portaAnim.SetBool("isAperta", false);


        //Controlla se DoOnce è a 0
        if (doOnce_sfx >= 1)
        {
            //Riproduci il rumore della chiusura
            porta_sfxSource.PlayOneShot(portaChiude_sfx);

            doOnce_sfx = 0;       //Porta il DoOnce a 0 -> per il suono quando Apre
        }
    }
}
