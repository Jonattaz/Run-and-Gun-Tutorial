using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Variável que controla a velocidade da bala
    public float speed = 10f;
    // Variável que controla o dano dado pela bala
    public int damage = 1;
    // Variável que representa o tempo levado para a destruição da bala ao ser instânciada
    public float destroyTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime) ;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy otherEnemy = collision.GetComponent<Enemy>();
        if (otherEnemy != null)
        {
            otherEnemy.TookDamage(damage);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }

}





























