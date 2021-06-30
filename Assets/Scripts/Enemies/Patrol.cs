using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Enemy
{
    // Game object que representa o tiro do Patrol
    public GameObject bulletPrefab;

    // Variável que representa a taxa de tiro do Patrol
    public float fireRate;

    // Tranform que representa onde o tiro será spawnado
    public Transform shotSpawner;

    // Variável que controla quando o Patrol irá atirar novamente
    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (targetDistance < 0)
        {
            if (!facingRight)
            {
                Flip();
            }
        }
        else
        {
            if (facingRight)
            {
                Flip();
            }
        }



        if (Mathf.Abs(targetDistance) < attackDistance && Time.time > nextFire)
        {
            anim.SetTrigger("Shooting");
            nextFire = Time.time + fireRate;
            
        }

    }

    // Método que controla o tiro
    public void Shooting()
    {
        GameObject tempBullet = Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
        if (!facingRight)
        {
            tempBullet.transform.eulerAngles = new Vector3(0,0,180);

        }
    }



}












