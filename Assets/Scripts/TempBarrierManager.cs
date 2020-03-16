using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBarrierManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float tiempoBarrera = 0f;
    public GameObject barrera;
    private void Start()
    {
        if(barrera)
         barrera.SetActive(true);
    }
    private void FixedUpdate()
    {
        tiempoBarrera += Time.fixedDeltaTime;
        if(tiempoBarrera>= 25)
        {
            if (barrera)
            {
                barrera.SetActive(false);
            }
             Destroy(this);
        }
    }
}
