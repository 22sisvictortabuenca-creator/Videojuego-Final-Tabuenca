using UnityEngine;

public class EnemyHorizontal : MonoBehaviour
{
    public float speed = 2f;
    public float walkDistance = 3f;
    
    public bool isMovingRight = true;
    private float startX; 
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startX = transform.position.x;
    }

    void Update()
    {
        float horizontalVelocity = isMovingRight ? speed : -speed;
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);

        if (isMovingRight && transform.position.x > startX + walkDistance)
        {
            FlipEnemy();
        }
        else if (!isMovingRight && transform.position.x < startX - walkDistance)
        {
            FlipEnemy();
        }
    }


    void FlipEnemy()
    {
        isMovingRight = !isMovingRight;
        
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}