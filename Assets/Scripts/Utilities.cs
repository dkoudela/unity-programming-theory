using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static bool IsBetweenRange(float thisValue, float value1, float value2)
    {
        return thisValue >= Mathf.Min(value1, value2) && thisValue <= Mathf.Max(value1, value2);
    }

    public static void ChangeText(string textName, string newText)
    {
        GameObject uiTextObject = GameObject.Find(textName);
        UnityEngine.UI.Text uiText = uiTextObject.GetComponent<UnityEngine.UI.Text>();
        uiText.text = newText;
    }

    public static Vector3 GenerateSpawnPosition(float spawnRange)
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
