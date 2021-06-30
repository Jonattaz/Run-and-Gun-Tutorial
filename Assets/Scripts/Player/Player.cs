using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variável que controla a velocidade do player
    public float speed = 5f;

    // Variável que controla a força do pulo
    public float jumpForce = 600;

    // Objeto que serve de referência para instanciar os tiros
    public Transform shotSpawner;

    // Variável que armazena o prefab do tiro
    public GameObject bulletPrefab;

    // Variável que representa o rigidBody do jogador
    private Rigidbody2D rb;

    // Verifica para qual lado o jogador está virado
    private bool facinRight = true;

    // Variável para checar o pulo do jogador
    private bool jump;

    // Variável que checa se o jogador está no chão
    private bool onGround = false;

    // Variável que representa o transform do objeto groundCheck
    private Transform groundCheck;

    // Variável que faz a movimentação do jogador;
    private float hForce = 0;

    // Variável que checa se o jogador está morto
    private bool isDead = false;

    // Variável que representa o animator
    private Animator anim;

    // Variável que verifica se o jogador está agachado
    private bool crouched;

    // Variável que verifica se o jogador está olhando para cima
    private bool lookingUp;

    // Variável que representa o fire rate do player
    private float fireRate = 0.5f;

    // Variável que controla quando o jogador poderá atirar novamente
    private float nextFire;

    // Variável que verifica se o jogador está carrengando a munição da arma
    private bool reloading;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            onGround = Physics2D.Linecast(transform.position, groundCheck.position, 
                1 << LayerMask.NameToLayer("Ground"));

            if (onGround)
            {
                anim.SetBool("Jump", false);
            }


            if (Input.GetButtonDown("Jump") && onGround && !reloading)
            {
                jump = true;
            }
            else if(Input.GetButtonUp("Jump"))
            {
                if (rb.velocity.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                }
            }


            if (Input.GetKeyDown(KeyCode.F) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                anim.SetTrigger("Shoot");
                GameObject tempBullet = Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);
                if (!facinRight && !lookingUp)
                {
                    tempBullet.transform.eulerAngles = new Vector3(0,0,180);
                }else if (!facinRight && lookingUp)
                {
                    tempBullet.transform.eulerAngles = new Vector3(0,0,90);
                }
                
                if (crouched && !onGround)
                {
                    tempBullet.transform.eulerAngles = new Vector3(0, 0, -90);
                }
            }

            lookingUp = Input.GetButton("Up");
            crouched = Input.GetButton("Down");

            anim.SetBool("LookingUp", lookingUp);
            anim.SetBool("Crouched", crouched);

            if (Input.GetButtonDown("Reload"))
            {
                reloading = true;
                anim.SetBool("Reloading", true);
            }
            else if (Input.GetButtonUp("Reload"))
            {
                reloading = false;
                anim.SetBool("Reloading", false);
            }

            if ((crouched || lookingUp || reloading) && onGround)
            {
                hForce = 0;
            }

        }
    }


    private void FixedUpdate()
    {
        if (!isDead)
        {
            if(!crouched && !lookingUp && !reloading)
            {
                hForce = Input.GetAxisRaw("Horizontal");
            }
            
            anim.SetFloat("Speed", Mathf.Abs(hForce));

            rb.velocity = new Vector2(hForce * speed, rb.velocity.y);
            if (hForce > 0 && !facinRight)
            {
                Flip();

            }else if (hForce < 0 && facinRight )
            {
                Flip();
            }

            if (jump)
            {
                anim.SetBool("Jump", true);
                jump = false;
                rb.AddForce(Vector2.up * jumpForce);
            }


        }

    }


    // Método responsável pela ação de virar o personagem de acordo com o lado que ele for
    void Flip()
    {
        facinRight = !facinRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }



}

















