using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Nécessaire pour travailler avec UI
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f; // Force du saut
    private bool isGrounded = true; // Vérifie si le joueur est au sol
    private Rigidbody rb; // Référence au Rigidbody

    public int score = 0; // Score initial
    public TMP_Text scoreText; // Référence au texte UI
    public TMP_Text gameOverText; // Référence au texte "Game Over"

    public AudioSource audioSource; // Référence à l'AudioSource
    public AudioClip coinSound; // Clip audio pour le son de la pièce
    public AudioClip gameOverSong; // Clip audio pour la chanson de Game Over
    public AudioClip gamePerduSong; // Clip audio pour la chanson de Game Over
    public AudioClip gameSong; // Clip audio pour la chanson de Game Over


    public float side_speed;
    public float forwardSpeed;
    public Transform centerPosition;
    public Transform leftPosition;
    public Transform rightPosition;
    private int currentPosition;
    private bool isPlay;
    private bool hasLost = false; // Indique si le joueur a perdu
    public Animator player_animator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        isPlay = false;
        currentPosition = 0;
        gameOverText.gameObject.SetActive(false); // Cache le texte "Game Over" au début
    }

    void Update()
    {
        if (hasLost) return; // Arrête les mouvements si le joueur a perdu

        if (Input.GetMouseButtonDown(0))
        {
            isPlay = true;
            player_animator.SetBool("isRun", true);
        }

        if (isPlay)
        {
            audioSource.PlayOneShot(gameSong);

            // Déplacement vers l'avant
            transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;

            // Ajustement de la position horizontale en fonction de `currentPosition`
            if (currentPosition == 0)
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    new Vector3(centerPosition.position.x, transform.position.y, transform.position.z),
                    side_speed * Time.deltaTime
                );
            }
            else if (currentPosition == 1)
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    new Vector3(leftPosition.position.x, transform.position.y, transform.position.z),
                    side_speed * Time.deltaTime
                );
            }
            else if (currentPosition == 2)
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    new Vector3(rightPosition.position.x, transform.position.y, transform.position.z),
                    side_speed * Time.deltaTime
                );
            }

            // Gestion des entrées utilisateur pour changer la position
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentPosition == 0)
                {
                    currentPosition = 1; // Aller à gauche
                }
                else if (currentPosition == 2)
                {
                    currentPosition = 0; // Retour au centre
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (currentPosition == 0)
                {
                    currentPosition = 2; // Aller à droite
                }
                else if (currentPosition == 1)
                {
                    currentPosition = 0; // Retour au centre
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                isGrounded = false; // Empêche les sauts multiples
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Applique une force vers le haut
                player_animator.SetBool("isJump", true); // Lance l'animation de saut
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Permet de sauter à nouveau
            player_animator.SetBool("isJump", false); // Arrête l'animation de saut
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            // Joue le son de la pièce
            audioSource.PlayOneShot(coinSound);

            // Incrémente le score
            score += 100;

            // Met à jour le texte du score
            scoreText.text = "Score: " + score;

            // Détruit l'objet ramassé
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("obstacle"))
        {
            // Arrête le joueur
            isPlay = false;
            hasLost = true; // Le joueur a perdu

            // Affiche le message de défaite
            ShowGameOverMessage();
        }
    }

    void ShowGameOverMessage()
    {
        gameOverText.gameObject.SetActive(true); // Affiche le message

        // Désactive les animations
        player_animator.SetBool("isRun", false);
        player_animator.SetBool("isFall", true);



        // Applique une force pour faire tomber le joueur
        rb.isKinematic = false; // Assurez-vous que le Rigidbody est affecté par la physique
        rb.velocity = Vector3.zero; // Réinitialisez la vitesse
        rb.AddForce(Vector3.down * 50f, ForceMode.Impulse); // Force vers le bas

        // Joue les sons de défaite
        audioSource.PlayOneShot(gameOverSong);
        audioSource.PlayOneShot(gamePerduSong);
    }

}
