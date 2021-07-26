using UnityEngine;

/// <summary>
/// <c>ControlledGameObjectByStrategy</c> is implemented by the strategies to controll the related game object
/// </summary>
/// <remarks>
/// The passed game object is then used by the strategy for accessing its properties, components, etc. 
/// to be able changing the game object's behavior or anything else.
/// </remarks>
// ABSTRACTION
public interface ControlledGameObjectByStrategy
{
    void Register(GameObject gameObject);
}

/// <summary>
/// <c>PlayerControllerStrategy</c> controlls the player game object
/// </summary>
/// <remarks>
/// GoF Strategy for easily switch of different player behaviour
/// Handles movement of the player and reaction on collision with enemy
/// </remarks>
// ABSTRACTION
public interface PlayerControllerStrategy : ControlledGameObjectByStrategy
{
    void HandleControlls();
    void HandleEnemyCollision(Collision collision);
}

/// <summary>
/// <c>EnemySpawnStrategy</c> ensures the right enemy spawn strategy for the game difficulty
/// </summary>
/// <remarks>
/// GoF Strategy for easily switch of different enemy spawn strategy for the game difficulty
/// It spawns the enemies and triggers enemy to attack the player
/// </remarks>
// ABSTRACTION
public interface EnemySpawnStrategy : ControlledGameObjectByStrategy
{
    void SpawnEnemies(int gameLevel);
    void Attack();
}

/// <summary>
/// <c>PowerupSpawnStrategy</c> ensures the right powerup spawn strategy for the game difficulty
/// </summary>
/// <remarks>
/// GoF Strategy for easily switch of different powerup spawn strategy for the game difficulty
/// It spawns the powerups
/// </remarks>
// ABSTRACTION
public interface PowerupSpawnStrategy : ControlledGameObjectByStrategy
{
    void SpawnPowerups(int gameLevel);
}

/// <summary>
/// <c>SpawnObserver</c> is beeing notified when the <c>GameManager</c> changes the game
/// </summary>
/// <remarks>
/// GoF Observer pattern for breaking the circular dependency amount the GameManager and GameObjects
/// GameManager notifies the SpawnObservers when strategies have been switched, new game has been started or new level is set
/// </remarks>
// ABSTRACTION
public interface SpawnObserver
{
    void Register();
    void Spawn();
    void Game();
}

