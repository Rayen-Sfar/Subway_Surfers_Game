using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Nécessaire pour charger et gérer les scènes

public class InstructionsScript : MonoBehaviour
{
    // Fonction appelée pour revenir au menu principal
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Charge la scène nommée "MainMenu"
    }
}
