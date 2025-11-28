# Enemy Character Implementation

## Overview
This document describes the implementation of enemy characters with basic movement patterns for the klaus-poc-platformer Unity 5 game.

## Files Created

### 1. Assets/Prefabs/Enemy.prefab
The main enemy prefab that includes:
- **GameObject**: Named "Enemy"
- **Transform**: Standard transform component for position, rotation, and scale
- **SpriteRenderer**:
  - Uses Unity's default circle sprite (knob sprite) as a placeholder
  - Colored red (RGB: 1, 0.2, 0.2) to distinguish enemies from the player
  - Configured for 2D rendering with proper sorting
- **Rigidbody2D**:
  - Mass: 1
  - Gravity Scale: 1 (allows enemy to fall and interact with platforms)
  - Constraints: Rotation frozen (prevents unwanted spinning)
  - Collision Detection: Discrete (standard)
- **BoxCollider2D**:
  - Size: 1x1 units (matches sprite size)
  - Not a trigger (solid collision)
  - Offset: (0, 0) - centered on the sprite
- **EnemyMovement Script**: Attached with default values
  - Move Speed: 2 units/second
  - Patrol Distance: 3 units in each direction
  - Debug Gizmos: Enabled

### 2. Assets/Scripts/EnemyMovement.cs
A robust script that handles enemy patrol behavior:

#### Features:
- **Simple Patrol Pattern**: Enemies move back and forth in a straight line
- **Configurable Parameters**:
  - `moveSpeed`: How fast the enemy moves (default: 2 units/sec)
  - `patrolDistance`: How far from spawn point the enemy travels (default: 3 units)
- **Automatic Direction Changes**: Enemies reverse direction at patrol boundaries
- **Sprite Flipping**: Enemy sprite flips to face movement direction
- **Visual Debug Tools**: Gizmos show patrol boundaries in the Unity editor
- **Public Methods**:
  - `SetPatrolDistance(float)`: Change patrol range at runtime
  - `SetMoveSpeed(float)`: Change movement speed at runtime

#### How It Works:
1. On Start(), the script stores the enemy's starting position and calculates left/right boundaries
2. In FixedUpdate(), the enemy moves at constant speed in the current direction
3. When reaching a boundary, the direction reverses and the sprite flips
4. The Rigidbody2D's velocity is updated each frame for smooth physics-based movement

### 3. Assets/Scripts/EnemyMovementTests.cs
A comprehensive test script to validate enemy functionality:

#### Test Coverage:
- **Instantiation Test**: Verifies enemy can be created from prefab
- **Component Test**: Checks all required components are present
  - SpriteRenderer
  - Rigidbody2D
  - Collider2D
  - EnemyMovement script
- **Configuration Test**: Validates Rigidbody2D settings
  - Confirms rotation is frozen
  - Checks gravity scale is appropriate
- **Behavior Test**: Tests patrol behavior methods

#### Usage:
- Attach to any GameObject in the scene
- Assign the Enemy prefab to the `enemyPrefab` field
- Enable `runTestsOnStart` to auto-run tests in Play mode
- Or right-click the component and select "Run Tests" from the context menu

## Unity Editor Setup Instructions

### To Use the Enemy Prefab:
1. Open the Unity project
2. Navigate to `Assets/Prefabs/` in the Project window
3. Drag the `Enemy` prefab into your scene
4. Position it on a platform
5. Enter Play mode to see the enemy patrol

### To Customize Enemy Behavior:
1. Select an enemy instance in the Hierarchy
2. In the Inspector, find the "Enemy Movement" component
3. Adjust parameters:
   - **Move Speed**: Higher = faster movement
   - **Patrol Distance**: Higher = longer patrol range
   - **Show Debug Gizmos**: Toggle to show/hide patrol boundaries

### To Test Enemy Functionality:
1. Create an empty GameObject in your scene
2. Add the `EnemyMovementTests` component
3. Assign the Enemy prefab to the `enemyPrefab` field
4. Right-click the component and select "Run Tests"
5. Check the Console for test results

## Technical Notes

### Physics Configuration
- The Rigidbody2D uses dynamic body type for realistic physics
- Rotation is frozen on the Z-axis to prevent spinning
- Gravity is enabled so enemies fall onto platforms
- Linear drag is 0 for smooth movement

### Collision Detection
- BoxCollider2D is used for simple rectangular collision bounds
- Size can be adjusted to match different sprite sizes
- Not configured as a trigger - enemies have solid collisions

### Movement Implementation
- Uses velocity-based movement via Rigidbody2D
- Preserves Y-axis velocity (doesn't interfere with jumping/falling)
- Only modifies X-axis velocity for horizontal patrol
- Boundary detection uses world space positions

### Future Enhancements
This implementation provides a foundation for:
- Health system integration (enemy can take damage)
- Attack behavior (enemy can damage player on collision)
- Different enemy types (varying speeds, patrol patterns)
- AI improvements (platform edge detection, player pursuit)
- Animation integration (walk, turn, attack animations)

## Acceptance Criteria Fulfillment

✅ **A new prefab named "Enemy" is created** - Located at `Assets/Prefabs/Enemy.prefab`

✅ **SpriteRenderer with placeholder sprite** - Uses Unity's default circle sprite with red color

✅ **Rigidbody2D component attached** - Configured with gravity and frozen rotation

✅ **Collider2D component** - BoxCollider2D sized to match the sprite

**Bonus:** Movement script included to provide immediate patrol functionality
