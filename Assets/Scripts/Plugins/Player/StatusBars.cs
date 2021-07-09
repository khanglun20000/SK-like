using UnityEngine;
using UnityEngine.UI;

public class StatusBars : MonoBehaviour
{
    public Image healthBar;
    public Image shieldBar;
    public Image manaBar;
    private PlayerBehaviour player;
    public void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }
    private void Update()
    {
        healthBar.fillAmount = player.currentHealth / player.maxHealth;
        shieldBar.fillAmount = player.currentShield/ player.maxShield;
        manaBar.fillAmount = player.currentMana / player.maxMana;
    }
}
