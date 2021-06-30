using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Variável que controla a velocidade do player
    public float speed = 5f;

    // Variável que controla a força do pulo
    public float jumpForce = 600;

    // Tempo para recarregar a munição
    public float reloadTime;

    // Variável que controla se o jogador pode atirar
    public bool canFire = true;

    // Variável que representa o tempo do qual o personagem fica invulneravel quando leva dano
    public float damageTime = 1f;

    // Objeto que serve de referência para instanciar os tiros
    public Transform shotSpawner;

    // Variável que armazena o prefab do tiro
    public GameObject bulletPrefab;

    // Váriavel que armazena o rigidbody da bomba
    public Rigidbody2D bombRb;

    // Variável para saber se levou dano
    private bool tookDamage = false;

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

    // Variável que representa as balas do jogador
    private int bullets;

    // Variável que representa a vida atual do player
    private int health;

    // Variável que representa a vida total do player
    private int maxHealth;

    // Variável que representa o número do bombas
    private int bombs;

    // Representa o gameManager
    GameManager gameManager;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
        gameManager = GameManager.gameManager;

        SetPlayerStatus();
        bombs = gameManager.bombs;
        health = maxHealth;

        UpdateBulletsUI();
        UpdateBombsUI();
        UpdateHealthUI();
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


            if (Input.GetKeyDown(KeyCode.F) && Time.time > nextFire && bullets > 0 && !reloading && canFire)
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

                bullets--;
                UpdateBulletsUI();

            }
            else if(Input.GetKeyDown(KeyCode.F) && bullets <= 0 && onGround)
            {
                StartCoroutine(Reloading());
            }

            lookingUp = Input.GetButton("Up");
            crouched = Input.GetButton("Down");

            anim.SetBool("LookingUp", lookingUp);
            anim.SetBool("Crouched", crouched);

            if (Input.GetButtonDown("Reload"))
            {
                StartCoroutine(Reloading());
            }

            if (Input.GetKeyDown(KeyCode.G) && bombs > 0)
            {
                Rigidbody2D tempBomb = Instantiate(bombRb, transform.position, transform.rotation);
                if(facinRight)
                {
                    tempBomb.AddForce(new Vector2(8,10), ForceMode2D.Impulse);
                }else if (!facinRight)
                {
                    tempBomb.AddForce(new Vector2(-8, 10), ForceMode2D.Impulse);
                }

                bombs--;
                UpdateBombsUI();
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


    // Método atribui os status do player usando os valores do GameManager
    public void SetPlayerStatus()
    {
        fireRate = gameManager.fireRate;
        bullets = gameManager.bullets;
        reloadTime = gameManager.reloadTime;
        maxHealth = gameManager.health;

    }


    // Método que atualiza o número de balas de acordo com o que o personagem tem
    private void UpdateBulletsUI()
    {
        FindObjectOfType<UIManager>().UpdateBulletUI(bullets);
    }

    // Corrotina que recarrega a munição
   IEnumerator Reloading()
    {
        reloading = true;
        anim.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime);
        bullets = gameManager.bullets;
        reloading = false;
        anim.SetBool("Reloading", false);
        UpdateBulletsUI();
    }


    // Método que atualiza o número de bombas de acordo com o que o personagem tem  
    private void UpdateBombsUI()
    {
        FindObjectOfType<UIManager>().UpdateBombsUI(bombs);
        gameManager.bombs = bombs;
    }


    // Método que atualiza a vida do player
    private void UpdateHealthUI()
    {
        FindObjectOfType<UIManager>().UpdateHealthUI(health);
    }
    
    // Método que atualiza o número de modedas do player
    private void UpdateCoinsUI()
    {
        FindObjectOfType<UIManager>().UpdateCoinsUI();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !tookDamage)
        {
            StartCoroutine(TookDamage());
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !tookDamage)
        {
            StartCoroutine(TookDamage());
        }
        else if(collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            gameManager.coins += 1;
            UpdateCoinsUI();
        }
    }

    // Corrotina responsável pelo dano que o personagem leva
    IEnumerator TookDamage()
    {
        tookDamage = true;
        health--;
        UpdateHealthUI();
        if (health <= 0)
        {
            isDead = true;
            anim.SetTrigger("Death");
            Invoke("ReloadScene", 2f);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(9,10);
            for (float i = 0; i < damageTime; i+= 0.2f)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                yield return new WaitForSeconds(0.1f);
                GetComponent<SpriteRenderer>().enabled = true;
                yield return new WaitForSeconds(0.1f);
            }
            Physics2D.IgnoreLayerCollision(9,10,false);
            tookDamage = false;
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    // Método que controla o aumento de bombas e vidas ao pegar uma caixa
    public void SetHealthAndBombs(int life, int bomb)
    {
        health += life;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }

        bombs += bomb;
        UpdateBombsUI();
        UpdateHealthUI();

    }



}

















