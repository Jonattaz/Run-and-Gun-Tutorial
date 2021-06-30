using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeShop : MonoBehaviour
{
    // Referência todos os textos do painel de upgrade
    public Text healthText,damageText, fireRateText, bulletsText, reloadTimeText, upgradeCostText;

    // Referência ao GameManager
    GameManager gameManager;

    // Referência ao Player
    Player player;

    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameManager.gameManager;
        player = FindObjectOfType<Player>();
        UpdateUI();

    }

    // Método que atualiza os valores na tela de upgrade
    private void UpdateUI()
    {
        healthText.text = "Health: " + gameManager.health;
        damageText.text = "Damage: " + gameManager.damage;
        fireRateText.text = "Fire Rate: " + gameManager.fireRate;
        bulletsText.text = "Bullets: " + gameManager.bullets;
        reloadTimeText.text = "Reload Time: " + gameManager.reloadTime;
        upgradeCostText.text = "Upgrade Cost: " + gameManager.upgradeCost;
    }

    // Método para o botão de upgrade da vida(health)
    public void SetHealth()
    {
        if (gameManager.coins >=  gameManager.upgradeCost)
        {
            gameManager.health++;
            FindObjectOfType<UIManager>().UpdateHealthBar();
            player.SetPlayerStatus();
            SetCoins(gameManager.upgradeCost);
            gameManager.upgradeCost += (gameManager.upgradeCost / 5);
            UpdateUI();

        }
    }
    // Método para o botão de upgrade do dano
    public void SetDamage()
    {
        if (gameManager.coins >= gameManager.upgradeCost)
        {
            gameManager.damage++;
            player.SetPlayerStatus();
            SetCoins(gameManager.upgradeCost);
            gameManager.upgradeCost += (gameManager.upgradeCost / 5);
            UpdateUI();
        }
    }

    // Método para o botão de upgrade do fire rate
    public void SetFireRate()
    {
        if (gameManager.coins >= gameManager.upgradeCost)
        {
            gameManager.fireRate -= 0.1f;
            if (gameManager.fireRate <= 0)
            {
                gameManager.fireRate = 0;
            }

            player.SetPlayerStatus();
            SetCoins(gameManager.upgradeCost);
            gameManager.upgradeCost += (gameManager.upgradeCost / 5);
            UpdateUI();

        }
    }

    // Método para o botão de upgrade do número de munição
    public void SetBullets()
    {
        if ( gameManager.coins  >=  gameManager.upgradeCost)
        {
            gameManager.bullets++;

            player.SetPlayerStatus();
            SetCoins(gameManager.upgradeCost);
            gameManager.upgradeCost += (gameManager.upgradeCost / 5);
            UpdateUI();
        }
    }
    // Método para o botão de upgrade do reload time
    public void SetReloadTime()
    {
        if (gameManager.coins >= gameManager.upgradeCost)
        {
            gameManager.reloadTime -= 0.1f;

            if (gameManager.reloadTime <= 0)
            {
                gameManager.reloadTime = 0;
            }

            player.SetPlayerStatus();
            SetCoins(gameManager.upgradeCost);
            gameManager.upgradeCost += (gameManager.upgradeCost / 5);
            UpdateUI();
        }
    }


    // Método que atualiza o número de moedas que o player possui após realizar o upgrade
    private void SetCoins(int coin)
    {
        gameManager.coins -= coin;
        FindObjectOfType<UIManager>().UpdateCoinsUI();


    }

}























