using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CambiaMusica : MonoBehaviour
{
    AudioSource musicaSource;
    
    [SerializeField] List<AudioClip> listaMusiche;

    Queue<AudioClip> playlistRandom = new Queue<AudioClip>();



    private void Start()
    {
        musicaSource = GetComponent<AudioSource>();

        //Crea una "playlist" casuale della musica e inizia a farla sentire
        RimescolaMusica();
        StartCoroutine(CambiaMusicaSfondo());
    }

    IEnumerator CambiaMusicaSfondo()
    {
        while (true)
        {
            //Riproduce la musica in "playlist", togliendola dalla coda
            AudioClip clipOra = playlistRandom.Dequeue();
            musicaSource.PlayOneShot(clipOra);


            yield return new WaitForSeconds(clipOra.length);

            //Se la "playlist" e' finita, ne genera una nuova casuale
            if (playlistRandom.Count <= 0)
                RimescolaMusica();
        }
    }
    

    /// <summary>
    /// Prende tutti gli AudioClip e crea una nuova "playlist" casuale
    /// </summary>
    void RimescolaMusica()
    {
        playlistRandom = MescolaCoda(listaMusiche);
    }

    #region Funz. per mescolare/randomizzare la coda

    Queue<T> MescolaCoda<T>(List<T> lista)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            //Prende l'audio [i] e lo mette da parte,
            //poi prende un indice casuale
            T temp = lista[i];
            int indiceRandom = Random.Range(0, lista.Count);

            //Scambia l'audio [i] e quello dell'indice casuale
            lista[i] = lista[indiceRandom];
            lista[indiceRandom] = temp;
        }


        //Crea una nuova coda e sposta gli elementi della lista mescolata nella coda
        Queue<T> coda = new Queue<T>();

        foreach (T elem in lista)
        {
            coda.Enqueue(elem);
        }


        return coda;
    }

    #endregion
}
