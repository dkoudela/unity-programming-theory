using UnityEngine;

public class Health : MonoBehaviour
{
    private int health;
    private string healthTextPrefix = "Health: ";

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            health = 10;
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            health = 2;
            if (gameObject.name.Contains("Boss Enemy"))
            {
                health = 5;
            }
        }

        if (gameObject.CompareTag("Player"))
        {
            Utilities.ChangeText("Health", healthTextPrefix + health);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (0 >= health)
        {
            Destroy(gameObject);
        }
    }

    public void DecreaseHealth()
    {
        health--;
        UpdatePlayerHealthText();
    }

    public void IncreaseHealth(int newHealth)
    {
        health += newHealth;
        UpdatePlayerHealthText();
    }

    private void UpdatePlayerHealthText()
    {
        if (gameObject.CompareTag("Player"))
        {
            Utilities.ChangeText("Health", healthTextPrefix + health);
        }
    }
}
