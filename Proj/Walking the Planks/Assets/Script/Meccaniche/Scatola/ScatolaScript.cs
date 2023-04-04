using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatolaScript : MonoBehaviour
{
    [Header("—  Feedback  —")]
    [SerializeField] AudioSource scatola_sfxSource;
    [SerializeField] AudioClip[] scatolaHit_sfx;
    [SerializeField] AudioClip scatolaDistr_sfx;
    [SerializeField] Vector2 rangePitch = new Vector2(0.85f, 1.5f);



    private void Awake()
    {
        scatola_sfxSource = GetComponent<AudioSource>();

        //Scabia le variabili del range del pitch se sono scritte: [magg, min]
        if(rangePitch.x > rangePitch.y)
        {
            float temp = rangePitch.y;
            rangePitch.y = rangePitch.x;
            rangePitch.x = temp;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Spike":
                scatola_sfxSource.pitch = Random.Range(rangePitch.x, rangePitch.y);
                scatola_sfxSource.PlayOneShot(scatolaDistr_sfx);
                break;

            default:
                int i = Random.Range(0, scatolaHit_sfx.Length);
                AudioClip clip = scatolaHit_sfx[i];

                //Prende una clip a caso e la riproduce
                scatola_sfxSource.pitch = 1;
                scatola_sfxSource.PlayOneShot(clip);
                break;
        }
    }
}
