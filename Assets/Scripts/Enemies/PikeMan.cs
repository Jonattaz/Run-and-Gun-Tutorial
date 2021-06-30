using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikeMan : Enemy
{ 
    // Variável que representa a distância que deve existir entre o jogador e este objeto, para que ele possa se movimentar
    public float walkDistance;

    //Controla o estado da movimentação/ true = está andando. false = não está
    private bool walk;

    // Controla o estado do ataque/ true = está atacando. false = não está
    private bool attack = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        anim.SetBool("Walk", walk);
        anim.SetBool("Attack", attack);

        if (Mathf.Abs(targetDistance) < walkDistance)
        {
            walk = true;
        }

        if (Mathf.Abs(targetDistance) < attackDistance)
        {
            attack = true;
            walk = false;
        }

    }

    private void FixedUpdate()
    {
        if (walk && !attack)
        {
            if (targetDistance  < 0)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                if (!facingRight)
                {
                    Flip();
                }
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                if (facingRight)
                {
                    Flip(); 
                }
            }
        }
    }

    // Método que controla o estado do ataque, para que não fique atacando direto
    public void ResetAttack()
    {
        attack = false;


    }



}
