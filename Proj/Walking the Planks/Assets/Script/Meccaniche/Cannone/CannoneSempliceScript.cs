using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannoneSempliceScript : MonoBehaviour
{
    ObjectPoolingScript poolingScr;

    [SerializeField] bool sonoAttivo;

    [Space(10)]
    [SerializeField] float potenzaImpulso = 10;
    [SerializeField] float secDaAspettare = 10;
    float tempoTrascorso;

    [Space(15)]
    [SerializeField] Transform puntoOrigineProiet;
    #region Tooltip()
    [Tooltip("La tag del proiettile da prendere nella pool")] 
    #endregion
    [SerializeField] string proiettile_tag;

    [Space(10)]
    [SerializeField] ParticleSystem sparkle_part;
    #region Tooltip()
    [Tooltip("La quantità delle particelle sparticles rispetto al timer")]
    #endregion
    [SerializeField] AnimationCurve misuraSparkle;
    float frequenzaIniziale_Sparkle;
    [SerializeField] ParticleSystem fumo_part;


    private void Awake()
    {
        poolingScr = FindObjectOfType<ObjectPoolingScript>();
        frequenzaIniziale_Sparkle = sparkle_part.emission.rateOverTime.constant;  //Prende il rateOverTime iniziale delle partic. sparkles
    }

    void Update()
    {
        //Se e' passato abbastanza tempo...
        if (tempoTrascorso >= secDaAspettare)
        {
            SparaPallaDiCannone();  //Spara la palla di cannone
            tempoTrascorso = 0;     //Resetta il timer
        }
        else
        {
            if(sonoAttivo)
                tempoTrascorso += Time.deltaTime;  //Aumenta il conteggio del tempo trascorso

            //Cambia le sparkles del cannone rispetto al timer
            float rapportoQuantita = misuraSparkle.Evaluate(tempoTrascorso / secDaAspettare);
            var em = sparkle_part.emission;
            em.rateOverTime = rapportoQuantita * frequenzaIniziale_Sparkle;
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
    }

    public void AttivaCannoneSemplice()
    {
        sonoAttivo = true;
    }
    public void DisattivaCannoneSemplice()
    {
        sonoAttivo = false;
    }
}
