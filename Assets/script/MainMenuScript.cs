using UnityEngine;
using UnityEngine.SceneManagement; // Nécessaire pour charger des scènes

public class MainMenuScript : MonoBehaviour
{
    // Fonction pour lancer le jeu
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene"); // Remplacez "GameScene" par le nom de votre scène de jeu
    }

    // Fonction pour afficher les instructions
    public void ShowInstructions()
    {
        // Affiche une fenêtre d'instructions ou charge une scène dédiée
        SceneManager.LoadScene("InstructionsScene"); // Remplacez par une scène ou créez une fenêtre plus tard
    }

    // Fonction pour quitter le jeu
    public void ExitGame()
    {
        Debug.Log("Quit Game"); // Fonctionne dans l'éditeur
        Application.Quit(); // Fonctionne pour une application construite
    }
}
