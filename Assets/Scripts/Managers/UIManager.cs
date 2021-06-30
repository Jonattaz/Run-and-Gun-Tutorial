using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Variável que serve de referência ao texto das munições
    public Text bulletText;

    // Variável que serve de referência ao texto da vida
    public Text healthText;

    // Variável que serve de referência a barra de vida
    public Slider healthBar;

    // Variável que referência o texto das moedas
    public Text coinsText;

    // Variável que representa o texto das bombas
    public Text bombsText;


    // Start is called before the first frame update
    void Awake()
    {
        UpdateHealthBar();
        UpdateCoinsUI();

    }

    // Método que atualiza o UI das munições
    public void UpdateBulletUI(int bullets)
    {
        bulletText.text = bullets.ToString();
    }

    // Método que atualiza o UI da vida
    public void UpdateHealthUI(int health)
    {
        healthText.text = health.ToString();
        healthBar.value = health;
    }


    // Método que atualiza o UI das moedas
    public void UpdateCoinsUI()
    {
        coinsText.text = GameManager.gameManager.coins.ToString();
    }

    // Método que atualiza o UI das bombas
    public void UpdateBombsUI(int bombs)
    {
        bombsText.text = bombs.ToString();
    }

    // Método que atualiza a barra de vida
    public void UpdateHealthBar()
    {
        healthBar.maxValue = GameManager.gameManager.health;
    }






}


















