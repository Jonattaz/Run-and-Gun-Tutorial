using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    // Variável que representa o tempo que será levado para destruir o objeto
    public float destroyTime;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
