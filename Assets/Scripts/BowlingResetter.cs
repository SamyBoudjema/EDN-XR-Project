using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Permet de réinitialiser la scène de bowling via un bouton du contrôleur XR.
/// À attacher sur l'objet BowlingAlley.
/// </summary>
public class BowlingResetter : MonoBehaviour
{
    [Tooltip("Action d'entrée XR pour réinitialiser la scène (ex: bouton du contrôleur)")]
    public InputActionReference resetAction;

    void Update()
    {
        // Vérifier si le bouton de reset est pressé
        if (resetAction != null && resetAction.action.triggered)
        {
            // Recharger la scène active
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
