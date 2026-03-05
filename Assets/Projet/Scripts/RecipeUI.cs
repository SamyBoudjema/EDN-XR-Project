using UnityEngine;
using TMPro;

public class RecipeUI : MonoBehaviour
{
    public TextMeshProUGUI recipeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged.AddListener(UpdateScore);
            GameManager.Instance.OnTimeChanged.AddListener(UpdateTimer);
        }
        UpdateRecipeText("Salade d'Acides : 3 Citrons, 2 Pommes"); // Placeholder
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }

    public void UpdateTimer(float time)
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void UpdateRecipeText(string newRecipe)
    {
        if (recipeText != null)
            recipeText.text = "Commande:\n" + newRecipe;
    }
}
