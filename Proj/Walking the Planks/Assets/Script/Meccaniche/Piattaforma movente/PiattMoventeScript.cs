using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiattMoventeScript : MonoBehaviour
{
    Vector3 pIniz;
    

    private void Start()
    {
        pIniz = transform.position;
    }

    void Update()
    {
        transform.localPosition = pIniz + Mathf.Sin(Time.time * .75f) * Vector3.right * 3;
    }

    private void OnTriggerStay(Collider other)
    {
        bool nonSonoIo = !other.gameObject.CompareTag("piatt-mov");

        if (nonSonoIo && other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(gameObject.transform);
            print("ENTRATO");
        }


        //if (!other.gameObject.CompareTag("Finish") && other.gameObject.CompareTag("Player"))
        //    other.transform.SetParent(gameObject.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        bool nonSonoIo = !other.gameObject.CompareTag("piatt-mov");

        if (nonSonoIo && other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);
            print("uscito");
        }


        //if (!other.gameObject.CompareTag("Finish") && other.gameObject.CompareTag("Player"))
        //    other.transform.SetParent(null);
    }
}
