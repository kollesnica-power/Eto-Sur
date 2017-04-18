using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] private float fullHealth = 100f;
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;

    [Space]

    [Header("Colors")]
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color zeroHealthColor = Color.red;
    [SerializeField] private PlayerMovement m_PlayerMovement;

    private float currentHealth;
    private int level = 1;
    private bool isDead;

    public bool IsDead {
        get {
            return isDead;
        }
    }

    private void OnEnable() {

        // When the player is enabled, reset the player's health and whether or not it's dead.
        if (level == 1) {
            currentHealth = fullHealth; 
        }

        isDead = false;

        // Update the health slider's value and color.
        SetHealthUI();

    }

    public void TakeDamage(float amount) {

        currentHealth -= amount;

        

        if (currentHealth <= 0f) {

            if (level == 1 && !isDead) {
                OnDeath();
            } else {
                LevelDown();
            }

        }

        SetHealthUI();

    }

    private void Heal(float amount) {

        currentHealth += amount;

        //currentHealth = Mathf.Clamp(currentHealth, 0f, fullHealth);

        if (currentHealth > fullHealth) {
            if (level < 3) {
                LevelUp();
            } else {
                currentHealth = fullHealth;
            }
        }

        SetHealthUI();



    }

    private void SetHealthUI() {

        slider.value = currentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / fullHealth);

    }



    private void LevelUp() {

        level++;

        // Increase size of the player.
        transform.localScale *= 1.25f;

        currentHealth -= fullHealth;

        m_PlayerMovement.IncreaseSpeed(0.25f);
        m_PlayerMovement.IncreaseJumpForce(0.25f);

    }

    private void LevelDown() {

        level--;

        // Decrese size of the player.
        transform.localScale /= 1.25f;

        currentHealth = fullHealth + currentHealth;
        
        m_PlayerMovement.DecreaseSpeed(0.25f);
        m_PlayerMovement.DecreaseJumpForce(0.25f);

    }




    private void OnDeath() {

        isDead = true;
        gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("HealthPlus")) {

            Destroy(other.gameObject);
            Heal(25f);

        }

    }

}
