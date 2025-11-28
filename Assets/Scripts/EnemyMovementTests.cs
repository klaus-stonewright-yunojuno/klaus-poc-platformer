using UnityEngine;

/// <summary>
/// Test script for EnemyMovement component.
/// This is a simple runtime test that validates enemy behavior in Play mode.
/// For Unity Test Framework integration, this would need to be moved to an Editor folder.
/// </summary>
public class EnemyMovementTests : MonoBehaviour
{
    [Header("Test Configuration")]
    [Tooltip("Enable to run tests on Start")]
    [SerializeField] private bool runTestsOnStart = false;

    [Tooltip("Reference to the enemy to test")]
    [SerializeField] private GameObject enemyPrefab;

    private GameObject testEnemy;
    private EnemyMovement enemyMovement;

    void Start()
    {
        if (runTestsOnStart)
        {
            RunAllTests();
        }
    }

    /// <summary>
    /// Runs all enemy movement tests
    /// </summary>
    public void RunAllTests()
    {
        Debug.Log("=== Starting Enemy Movement Tests ===");

        TestEnemyInstantiation();
        TestComponentsPresent();
        TestRigidbodyConfiguration();
        TestPatrolBehavior();

        Debug.Log("=== Enemy Movement Tests Complete ===");
    }

    /// <summary>
    /// Test 1: Enemy can be instantiated from prefab
    /// </summary>
    private void TestEnemyInstantiation()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("[TEST FAILED] Enemy prefab is not assigned!");
            return;
        }

        testEnemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);

        if (testEnemy != null)
        {
            Debug.Log("[TEST PASSED] Enemy instantiated successfully");
        }
        else
        {
            Debug.LogError("[TEST FAILED] Failed to instantiate enemy");
        }
    }

    /// <summary>
    /// Test 2: All required components are present
    /// </summary>
    private void TestComponentsPresent()
    {
        if (testEnemy == null)
        {
            Debug.LogError("[TEST SKIPPED] No test enemy available");
            return;
        }

        bool allComponentsPresent = true;

        // Check SpriteRenderer
        SpriteRenderer spriteRenderer = testEnemy.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("[TEST FAILED] SpriteRenderer component missing");
            allComponentsPresent = false;
        }

        // Check Rigidbody2D
        Rigidbody2D rb = testEnemy.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("[TEST FAILED] Rigidbody2D component missing");
            allComponentsPresent = false;
        }

        // Check Collider2D (BoxCollider2D or any Collider2D)
        Collider2D collider = testEnemy.GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogError("[TEST FAILED] Collider2D component missing");
            allComponentsPresent = false;
        }

        // Check EnemyMovement script
        enemyMovement = testEnemy.GetComponent<EnemyMovement>();
        if (enemyMovement == null)
        {
            Debug.LogError("[TEST FAILED] EnemyMovement script missing");
            allComponentsPresent = false;
        }

        if (allComponentsPresent)
        {
            Debug.Log("[TEST PASSED] All required components present");
        }
    }

    /// <summary>
    /// Test 3: Rigidbody2D is configured correctly
    /// </summary>
    private void TestRigidbodyConfiguration()
    {
        if (testEnemy == null)
        {
            Debug.LogError("[TEST SKIPPED] No test enemy available");
            return;
        }

        Rigidbody2D rb = testEnemy.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("[TEST SKIPPED] Rigidbody2D not found");
            return;
        }

        bool configurationCorrect = true;

        // Check if rotation is frozen
        if ((rb.constraints & RigidbodyConstraints2D.FreezeRotation) == 0)
        {
            Debug.LogWarning("[TEST WARNING] Rigidbody2D rotation is not frozen - enemy may rotate unexpectedly");
            configurationCorrect = false;
        }

        // Check if gravity is enabled
        if (rb.gravityScale <= 0)
        {
            Debug.LogWarning("[TEST WARNING] Rigidbody2D gravity scale is zero or negative");
        }

        if (configurationCorrect)
        {
            Debug.Log("[TEST PASSED] Rigidbody2D configured correctly");
        }
    }

    /// <summary>
    /// Test 4: Basic patrol behavior validation
    /// </summary>
    private void TestPatrolBehavior()
    {
        if (enemyMovement == null)
        {
            Debug.LogError("[TEST SKIPPED] EnemyMovement script not available");
            return;
        }

        // Test setting patrol distance
        enemyMovement.SetPatrolDistance(5f);
        Debug.Log("[TEST INFO] Set patrol distance to 5 units");

        // Test setting move speed
        enemyMovement.SetMoveSpeed(3f);
        Debug.Log("[TEST INFO] Set move speed to 3 units/second");

        Debug.Log("[TEST PASSED] Patrol behavior methods accessible");
        Debug.Log("[TEST INFO] For full movement testing, observe enemy in Play mode");
    }

    /// <summary>
    /// Clean up test objects
    /// </summary>
    void OnDestroy()
    {
        if (testEnemy != null)
        {
            Destroy(testEnemy);
        }
    }

    /// <summary>
    /// Manual test trigger for editor
    /// </summary>
    [ContextMenu("Run Tests")]
    public void RunTestsFromContextMenu()
    {
        RunAllTests();
    }
}
