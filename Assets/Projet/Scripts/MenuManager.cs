using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;    // Le menu avec les boutons de mode de jeu
    public GameObject gameUIPanel;      // L'interface en jeu (Score, Temps, etc.)
    public GameObject gameOverPanel;    // Le menu de fin de partie

    private void Start()
    {
        ShowMainMenu();

        // On écoute l'événement de fin de partie du GameManager pour réafficher un menu
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameOver.AddListener(ShowGameOverMenu);
        }
    }

    public void ShowMainMenu()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (gameUIPanel != null) gameUIPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    public void StartModeDefouloir()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.currentMode = GameMode.Defouloir;
            GameManager.Instance.StartGame();
        }
        HideMenusAndShowGameUI();
    }

    public void StartModeRecette()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.currentMode = GameMode.Recette;
            GameManager.Instance.StartGame();
        }
        HideMenusAndShowGameUI();
    }

    private void HideMenusAndShowGameUI()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (gameUIPanel != null) gameUIPanel.SetActive(true);
    }

    public void ShowGameOverMenu()
    {
        if (gameUIPanel != null) gameUIPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }
}
