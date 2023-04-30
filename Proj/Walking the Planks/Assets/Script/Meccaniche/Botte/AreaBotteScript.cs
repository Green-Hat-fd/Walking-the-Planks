using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBotteScript : MonoBehaviour
{
    [SerializeField] BotteRuzzolanteScript botteMainScr;

    [SerializeField] float attritoSopraBotte = 10;
    [SerializeField] float attritoNormale = 0;
    float _altezIniziale;

    bool giocatInPosizione;



    private void Start()
    {
        //Serve per capire quanto in alto deve stare questo oggetto (rispetto alla botte)
        _altezIniziale = Vector3.Distance(transform.position, botteMainScr.transform.position);
    }

    private void Update()
    {
        //Mantiene fermo questo oggetto nella posizione iniziale,
        //muovendolo solo nell'asse Z della botte
        transform.localPosition = new Vector3(transform.localPosition.x,
                                              botteMainScr.transform.localPosition.y + _altezIniziale,
                                              botteMainScr.transform.localPosition.z);

        //Mantiene l'area sempre orientata nel verso del genitore,
        //che sara' anche quello della botte
        transform.localRotation = Quaternion.Euler(Vector3.up * transform.parent.rotation.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Se e' entrato il giocatore...
        if (other.gameObject.CompareTag("Player"))
        {
            //Fa entrare il giocatore sulla botte
            botteMainScr.ScriviGiocat_Obj(other.gameObject);
            botteMainScr.ScriviGiocatSalito(true);

            other.transform.SetParent(transform);

            //Aumenta l'attrito del giocatore
            other.GetComponent<MovimGiocatRb>().CambiaAttritoRb(attritoSopraBotte);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject gObj = botteMainScr.LeggiGiocat_Obj();
        float mezzaAltezGiocat = gObj.GetComponent<CapsuleCollider>().height / 2;


        #region Calcolo della posizione del giocatore

        #region OLD_Non usato
        //---Funziona solo quando il genitore ha rotazione (0, 0, 0)---//
        //Vector3 pos = new Vector3(gObj.transform.position.x,
        //                          transform.position.y + mezzaAltezGiocat,
        //                          transform.position.z); 
        #endregion

        //Prende la rotazione sull'asse Y del genitore e la rende tra 0 e 1
        float rotazGenitoreClamp = Mathf.Abs(transform.parent.localEulerAngles.y) / 90;

        //Capisce qual e' l'asse "destra"/"right" del genitore per far muovere il giocatore solo in quell'asse
        // (Se ruotato di 0° ==========> lo blocca sull'asse X)
        // (Se ruotato di 90° o -90° ==> lo blocca sull'asse Z)
        float bloccoAsseX = Mathf.Lerp(gObj.transform.position.x, transform.position.x, rotazGenitoreClamp);
        float bloccoAsseZ = Mathf.Lerp(transform.position.z, gObj.transform.position.z, rotazGenitoreClamp);

        //Prende la posizione per far muovere il giocatore in un solo asse
        Vector3 pos = new Vector3(bloccoAsseX, transform.position.y + mezzaAltezGiocat, bloccoAsseZ);

        #endregion


        //Restringe il movim. del giocatore solo nell'asse X della botte
        //(se non salta)
        //if(!GameManager.inst.inputManager.Giocatore.Salto.triggered)
        if(GameManager.inst.inputManager.Giocatore.Salto.ReadValue<float>() <= 0)
        {
            if (giocatInPosizione)
            {
                gObj.transform.position = pos;
            }
            else
            {
                //Controlla se deve continuare a trascinare o no il giocatore verso "pos"
                giocatInPosizione = gObj.transform.position == pos;

                //Trascina il giocatore verso "pos"
                gObj.transform.position = Vector3.MoveTowards(gObj.transform.position, pos, Time.deltaTime * 2.5f);
            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && botteMainScr.LeggiGiocat_Obj())
        {
            //Rimuove il giocatore dal movimento limitato
            botteMainScr.ScriviGiocat_Obj(null);
            botteMainScr.ScriviGiocatSalito(false);

            other.transform.SetParent(null);

            //Diminuisce l'attrito del giocatore
            other.GetComponent<MovimGiocatRb>().CambiaAttritoRb(attritoNormale);

            giocatInPosizione = false;
        }
    }
}
