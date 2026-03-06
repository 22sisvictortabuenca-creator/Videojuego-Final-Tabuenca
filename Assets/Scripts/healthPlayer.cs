using UnityEngine;
using System.Collections;

public class HealthPlayer : MonoBehaviour
{
    public int lives = 3;
    public GameManager gManager; 
    
    [Header("Invencibilidad")]
    public float invincibilityTime = 1.5f;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Actualizar la interfaz al empezar
        if (GameManager.instance != null) {
            GameManager.instance.UpdateLivesUI(lives);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckDamage(other.gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckDamage(collision.gameObject);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        CheckDamage(other.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckDamage(collision.gameObject);
    }

    private void CheckDamage(GameObject obj)
    {
        if (!isInvincible && (obj.CompareTag("enemy") || obj.CompareTag("Lava")))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (isInvincible) return;

        lives--;
        Debug.Log("Vidas restantes: " + lives);
        
        if (GameManager.instance != null) {
            GameManager.instance.UpdateLivesUI(lives);
        }

        if (lives <= 0)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.ShowGameOver();
            }
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PlayerController>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
        else
        {
            StartCoroutine(InvincibilityRoutine());
        }
    }

    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        
        if (spriteRenderer != null)
        {
            for (float i = 0; i < invincibilityTime; i += 0.2f)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return new WaitForSeconds(0.2f);
            }
            spriteRenderer.enabled = true;
        }
        else
        {
            yield return new WaitForSeconds(invincibilityTime);
        }
        
        isInvincible = false;
    }
}