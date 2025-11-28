using UnityEngine;

/// <summary>
/// Controls basic enemy movement with a simple patrol pattern.
/// The enemy moves back and forth in a straight line on a platform.
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Speed at which the enemy moves")]
    [SerializeField] private float moveSpeed = 2f;

    [Tooltip("Distance the enemy patrols in each direction")]
    [SerializeField] private float patrolDistance = 3f;

    [Header("Debug")]
    [Tooltip("Show patrol boundaries in the editor")]
    [SerializeField] private bool showDebugGizmos = true;

    private Vector2 startPosition;
    private float leftBoundary;
    private float rightBoundary;
    private int direction = 1; // 1 for right, -1 for left
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get required components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Store starting position and calculate patrol boundaries
        startPosition = transform.position;
        leftBoundary = startPosition.x - patrolDistance;
        rightBoundary = startPosition.x + patrolDistance;

        // Configure Rigidbody2D if not already set in the inspector
        if (rb != null)
        {
            rb.freezeRotation = true;
            rb.gravityScale = 1f;
        }
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        // Move the enemy
        Vector2 velocity = rb.velocity;
        velocity.x = direction * moveSpeed;
        rb.velocity = velocity;

        // Check if we've reached a boundary
        if (direction > 0 && transform.position.x >= rightBoundary)
        {
            // Hit right boundary, turn left
            direction = -1;
            FlipSprite();
        }
        else if (direction < 0 && transform.position.x <= leftBoundary)
        {
            // Hit left boundary, turn right
            direction = 1;
            FlipSprite();
        }
    }

    /// <summary>
    /// Flips the sprite to face the direction of movement
    /// </summary>
    private void FlipSprite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = direction < 0;
        }
    }

    /// <summary>
    /// Draw patrol boundaries in the editor for debugging
    /// </summary>
    void OnDrawGizmos()
    {
        if (!showDebugGizmos) return;

        Vector2 center = Application.isPlaying ? startPosition : (Vector2)transform.position;
        float left = center.x - patrolDistance;
        float right = center.x + patrolDistance;
        float y = center.y;

        // Draw patrol line
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(left, y, 0), new Vector3(right, y, 0));

        // Draw boundary markers
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(left, y, 0), 0.2f);
        Gizmos.DrawWireSphere(new Vector3(right, y, 0), 0.2f);
    }

    /// <summary>
    /// Public method to change patrol distance at runtime if needed
    /// </summary>
    public void SetPatrolDistance(float distance)
    {
        patrolDistance = Mathf.Max(0.5f, distance);
        leftBoundary = startPosition.x - patrolDistance;
        rightBoundary = startPosition.x + patrolDistance;
    }

    /// <summary>
    /// Public method to change movement speed at runtime if needed
    /// </summary>
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = Mathf.Max(0.1f, speed);
    }
}
