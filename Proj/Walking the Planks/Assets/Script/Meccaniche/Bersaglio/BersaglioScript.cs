using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BersaglioScript : MonoBehaviour, IFeedback
{
    ObjectPoolingScript poolingScr;
    GameObject mesh;
    Collider collComp;

    [SerializeField] UnityEvent objDaAttivare;

    [SerializeField] bool distruggereDopoAttivo = true;

    [Header("—  Feedback  —")]
    #region Audio
    [SerializeField] AudioSource bers_sfx;
    [SerializeField] Vector2 rangePitch = new Vector2(0.85f, 1.5f);
    #endregion

    #region Particelle
    [SerializeField] string bersaglioRotto_part_tag;
    #endregion



    private void Awake()
    {
        poolingScr = FindObjectOfType<ObjectPoolingScript>();
        
        mesh = transform.GetChild(0).gameObject;
        collComp = GetComponent<Collider>();
    }

    public void AttivaOggetti()
    {
        print("Bersaglio colpito");  //DEBUG
        
        //Avvia l'evento
        objDaAttivare.Invoke();


        Feedback();


        //Viene tolto solo se la variabile e' vera
        if (distruggereDopoAttivo)
        {
            NascondiBersaglio();
        }
    }

    public void NascondiBersaglio()
    {
        //Nasconde e disattiva/Mostra e attiva il bersagio
        mesh.SetActive(false);
        collComp.enabled = false;
    }
    public void MostraBersaglio()
    {
        //Nasconde e disattiva/Mostra e attiva il bersagio
        mesh.SetActive(true);
        collComp.enabled = true;
    }

    public void Feedback()
    {
        //Riproduce il suono di quando viene colpito (con un pitch casuale)
        bers_sfx.pitch = Random.Range(rangePitch.x, rangePitch.y);
        bers_sfx.Play();

        //Fa vedere le particelle della palla che si rompe
        poolingScr.PrendeOggettoDallaPool(bersaglioRotto_part_tag, transform.position, Quaternion.identity);
    }
}
