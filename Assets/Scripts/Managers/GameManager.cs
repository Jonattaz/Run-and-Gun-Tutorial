using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variável que representa a vida do player
    public int health;

    // Variável que representa o dano que o player pode causar
    public int damage;

    // Variável que representa o tempo que o player irá demorar para atirar novamente
    public float fireRate;

    // Variável que reprenta o tempo que o jogador leva para recarregar a arma
    public float reloadTime;

    // Variável que representa o número de balas que o jogador tem até precisar recarregar novamente
    public int bullets;

    // Variável que representa as moedas do jogador
    public int coins;

    // Variável que representa o número de bombas que o jogador possui
    public int bombs;

    // Variável que representa o custo para os upgrades do player
    public int upgradeCost;

    // Instância do game manager
    public static GameManager gameManager;


    // Start is called before the first frame update
    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}




















