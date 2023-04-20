using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannoneSpecialeScript : MonoBehaviour
{
    ObjectPoolingScript poolingScr;

    [SerializeField] bool sonoAttivo;

    [Space(10)]
    [SerializeField] float potenzaImpulso = 10;
    [SerializeField] Vector2 rangeSecDaAspettare = new Vector2(3, 5);
    float secDaAspettare,
          tempoTrascorso;

    [Space(15)]
    [SerializeField] Transform puntoOrigineProiet;
    #region Tooltip()
    [Tooltip("La tag del proiettile da prendere nella pool")]
    #endregion
    [SerializeField] string proiettile_tag;

    [Header("—  Feedback  —")]
    #region Particelle
    [SerializeField] ParticleSystem sparkle_part;
    #region Tooltip()
    [Tooltip("La quantità delle particelle sparticles rispetto al timer")]
    #endregion
    [SerializeField] AnimationCurve misuraSparkle;
    float frequenzaIniziale_Sparkle;
    [SerializeField] ParticleSystem fumo_part;
    #endregion

    #region Audio
    [Space(5)]
    [SerializeField] AudioSource miccia_sfx;
    [SerializeField] AudioSource spara_sfx;
    #endregion

    #region Animazioni
    //[Space(5)]
    Animator sparaAnim;
    #endregion



    void Awake()
    {
        poolingScr = FindObjectOfType<ObjectPoolingScript>();
        sparaAnim = transform.GetChild(0).GetComponent<Animator>();
        frequenzaIniziale_Sparkle = sparkle_part.emission.rateOverTime.constant;  //Prende il rateOverTime iniziale delle partic. sparkles


        Vector2 rangeTmp;

        //Rende il minimo nella X e il massimo nella Y del Vector2
        rangeTmp.x = Mathf.Min(rangeSecDaAspettare.x, rangeSecDaAspettare.y);
        rangeTmp.y = Mathf.Max(rangeSecDaAspettare.x, rangeSecDaAspettare.y);

        rangeSecDaAspettare = rangeTmp;

        //...e genera un nuovo tempo casuale
        GeneraNuovoTempoRandom();
    }

    void Update()
    {
        //Se e' passato abbastanza tempo...
        if (tempoTrascorso >= secDaAspettare)
        {
            SparaPallaDiCannone();     //Spara la palla di cannone
            tempoTrascorso = 0;        //Resetta il timer
            GeneraNuovoTempoRandom();  //Genera un nuovo tempo da aspettare
        }
        else
        {
            if(sonoAttivo)
                tempoTrascorso += Time.deltaTime;  //Aumenta il conteggio del tempo trascorso


            float rapportoQuantita = misuraSparkle.Evaluate(tempoTrascorso / secDaAspettare);
            
            //Cambia le sparkles del cannone rispetto al timer (solo se e' attivo)
            var em = sparkle_part.emission;
            em.rateOverTime = sonoAttivo ? rapportoQuantita * frequenzaIniziale_Sparkle : 0;

            //Cambia il volume della miccia rispetto al timer (solo se e' attivo)
            miccia_sfx.volume = sonoAttivo ? rapportoQuantita : 0;
        }
    }

    void SparaPallaDiCannone()
    {
        //Crea e salva la palla di cannone
        GameObject proiet = poolingScr.PrendeOggettoDallaPool(proiettile_tag, puntoOrigineProiet.position, Quaternion.identity);

        //Lancia la palla di cannone
        proiet.GetComponent<Rigidbody>().AddForce(puntoOrigineProiet.up * potenzaImpulso, ForceMode.Impulse);


        //Fa vedere la particella del fumo
        fumo_part.Play();

        //Riproduce il suono dell'esplosione per lo sparo
        spara_sfx.PlayOneShot(spara_sfx.clip);

        //Riproduce l'animazione del cannone
        sparaAnim.SetTrigger("Spara");
    }

    void GeneraNuovoTempoRandom()
    {
        //Prende un nuovo tempo a caso tra il range scelto nella variabile
        secDaAspettare = Random.Range(rangeSecDaAspettare.x, rangeSecDaAspettare.y);
    }

    public void AttivaCannoneSpeciale()
    {
        sonoAttivo = true;
    }
    public void DisattivaCannoneSpeciale()
    {
        sonoAttivo = false;
    }
}
