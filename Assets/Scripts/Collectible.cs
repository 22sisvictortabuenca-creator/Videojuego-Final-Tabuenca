using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int scoreValue = 1;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.name == "Player")
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(scoreValue);
            }
            
            Destroy(this.gameObject);
        }
    }
}
