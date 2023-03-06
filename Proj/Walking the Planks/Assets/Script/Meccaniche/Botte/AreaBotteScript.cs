using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBotteScript : MonoBehaviour
{
    [SerializeField] BotteRuzzolanteScript botteMainScr;

    float _altezIniziale;


    private void Start()
    {
        //Serve per capire quanto in alto deve stare questo oggetto (rispetto alla botte)
        _altezIniziale = Vector3.Distance(transform.position, botteMainScr.transform.position);
    }

    private void Update()
    {
        //Mantiene fermo questo oggetto nella posizione iniziale,
        //muovendolo solo nell'asse Z della botte
        transform.position = new Vector3(transform.position.x,
                                         botteMainScr.transform.position.y + _altezIniziale,
                                         botteMainScr.transform.position.z);

        transform.rotation = Quaternion.identity;
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
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject gObj = botteMainScr.LeggiGiocat_Obj();
        float mezzaAltezGiocat = gObj.GetComponent<CapsuleCollider>().height / 2;
        

        //Prende la posizione per far muovere il giocatore in un solo asse
        Vector3 pos = new Vector3(gObj.transform.position.x,
                                  transform.position.y + mezzaAltezGiocat,
                                  transform.position.z);

        //Restringe il movim. del giocatore solo nell'asse X della botte
        //(se non salta)
        if(!GameManager.inst.inputManager.Giocatore.Salto.triggered)
            gObj.transform.position = pos;
    }

    private void OnTriggerExit(Collider other)
    {
        if (botteMainScr.LeggiGiocat_Obj())
        {
            //Rimuove il giocatore dal movimento ristretto
            botteMainScr.ScriviGiocat_Obj(null);
            botteMainScr.ScriviGiocatSalito(false);

            other.transform.SetParent(null);
        }
    }
}
