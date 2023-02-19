using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BersaglioScript : MonoBehaviour
{
    [SerializeField] UnityEvent objDaAttivare;


    public void AttivaOggetti()
    {
        print("Bersaglio colpito");
        objDaAttivare.Invoke();
    }
}
