using UnityEngine;

/// <summary>
/// Crée automatiquement les matériaux pour une salle de bowling réaliste.
/// À attacher sur un GameObject vide dans la scène, puis cliquer sur le bouton
/// dans l'Inspector pour générer les matériaux (ou les appliquer via le contexte menu).
/// Les matériaux sont créés en Runtime avec des couleurs et propriétés réalistes.
/// </summary>
public class BowlingMaterials : MonoBehaviour
{
    [Header("Éléments de la salle à colorer")]
    [Tooltip("La surface de la piste (Cube allongé)")]
    public Renderer laneRenderer;

    [Tooltip("Gouttière gauche")]
    public Renderer gutterLeftRenderer;

    [Tooltip("Gouttière droite")]
    public Renderer gutterRightRenderer;

    [Tooltip("Mur gauche")]
    public Renderer wallLeftRenderer;

    [Tooltip("Mur droit")]
    public Renderer wallRightRenderer;

    [Tooltip("Mur du fond")]
    public Renderer wallBackRenderer;

    [Tooltip("Plafond")]
    public Renderer ceilingRenderer;

    [Tooltip("Sol zone joueurs")]
    public Renderer floorRenderer;

    [Tooltip("Machine retour de boule (optionnel)")]
    public Renderer ballReturnRenderer;

    [Header("Bandes néon (optionnel)")]
    [Tooltip("Objets néon à rendre émissifs")]
    public Renderer[] neonStrips;

    [Header("Éclairage")]
    [Tooltip("Lumières de la piste")]
    public Light[] laneLights;

    [Tooltip("Lumières d'ambiance néon")]
    public Light[] neonLights;

    void Start()
    {
        ApplyAllMaterials();
    }

    /// <summary>
    /// Applique tous les matériaux de la salle de bowling.
    /// </summary>
    public void ApplyAllMaterials()
    {
        // ===== PISTE DE BOWLING =====
        // Bois clair verni avec légers reflets
        ApplyMaterial(laneRenderer, new Color(0.76f, 0.60f, 0.42f), 0.3f, 0.7f);

        // ===== GOUTTIÈRES =====
        // Gris foncé mat
        Color gutterColor = new Color(0.15f, 0.15f, 0.17f);
        ApplyMaterial(gutterLeftRenderer, gutterColor, 0.1f, 0.9f);
        ApplyMaterial(gutterRightRenderer, gutterColor, 0.1f, 0.9f);

        // ===== MURS =====
        // Bleu foncé profond ambiance bowling
        Color wallColor = new Color(0.05f, 0.05f, 0.18f);
        ApplyMaterial(wallLeftRenderer, wallColor, 0.2f, 0.8f);
        ApplyMaterial(wallRightRenderer, wallColor, 0.2f, 0.8f);
        ApplyMaterial(wallBackRenderer, wallColor, 0.2f, 0.8f);

        // ===== PLAFOND =====
        // Noir mat
        ApplyMaterial(ceilingRenderer, new Color(0.02f, 0.02f, 0.02f), 0.0f, 1.0f);

        // ===== SOL ZONE JOUEURS =====
        // Moquette rouge foncé / bordeaux
        ApplyMaterial(floorRenderer, new Color(0.35f, 0.08f, 0.10f), 0.0f, 1.0f);

        // ===== MACHINE RETOUR =====
        // Gris métallique
        ApplyMaterial(ballReturnRenderer, new Color(0.4f, 0.4f, 0.45f), 0.7f, 0.3f);

        // ===== NÉONS =====
        // Alternance rose et bleu vif avec émission
        if (neonStrips != null)
        {
            for (int i = 0; i < neonStrips.Length; i++)
            {
                if (neonStrips[i] == null) continue;

                // Alterner entre rose néon et bleu néon
                Color neonColor = (i % 2 == 0)
                    ? new Color(1.0f, 0.1f, 0.6f)   // Rose vif
                    : new Color(0.1f, 0.4f, 1.0f);   // Bleu vif

                Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                mat.color = neonColor;

                // Activer l'émission pour l'effet néon
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", neonColor * 3f);

                neonStrips[i].material = mat;
            }
        }

        // ===== ÉCLAIRAGE PISTE =====
        if (laneLights != null)
        {
            foreach (var light in laneLights)
            {
                if (light == null) continue;
                light.color = new Color(1.0f, 0.95f, 0.85f); // Blanc chaud
                light.intensity = 2f;
            }
        }

        // ===== ÉCLAIRAGE NÉON =====
        if (neonLights != null)
        {
            for (int i = 0; i < neonLights.Length; i++)
            {
                if (neonLights[i] == null) continue;

                // Alterner les couleurs
                neonLights[i].color = (i % 2 == 0)
                    ? new Color(1.0f, 0.1f, 0.6f)   // Rose
                    : new Color(0.1f, 0.4f, 1.0f);   // Bleu

                neonLights[i].intensity = 1.5f;
                neonLights[i].range = 5f;
            }
        }
    }

    /// <summary>
    /// Applique un matériau URP Lit avec les propriétés spécifiées à un renderer.
    /// </summary>
    private void ApplyMaterial(Renderer renderer, Color color, float smoothness, float roughness)
    {
        if (renderer == null) return;

        Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        mat.color = color;
        mat.SetFloat("_Smoothness", smoothness);

        renderer.material = mat;
    }
}
