using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f;

    // UI za bodove
    public TMP_Text scoreText;
    private int score = 0;

    // Game Over sustav
    public GameObject gameOverPanel; // Ovdje u Inspectoru odvuci Panel s gumbom
    public float fallThreshold = -6f; // Visina ispod koje se smatra da je igraè pao s platforme (Y = -4)
    private bool isDead = false;

    private void Update()
    {
        if (isDead) return;

        Jump();

        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        // Slanje vrijednosti u Animator za kretanje
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().SetFloat("MoveValue", Math.Abs(horizontal));
        }

        // Provjera je li igraè pao s platforme
        if (transform.position.y < fallThreshold)
        {
            GameOver();
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);

            if (GetComponent<Animator>() != null)
            {
                GetComponent<Animator>().SetTrigger("JumpTrigger");
            }
        }
    }

    // Metoda koju poziva novèiæ kad ga igraè pokupi
    public void AddScore()
    {
        score++;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void GameOver()
    {
        isDead = true;

        // Prikazuje Game Over panel s gumbom
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Zaustavlja vrijeme u igri
        Time.timeScale = 0f;
    }

    // Ovu metodu poveži s gumbom u Canvasu (On Click -> Player -> RestartGame)
    public void RestartGame()
    {
        Time.timeScale = 1f; // Vraæa vrijeme u normalu
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Ponovno uèitava scenu
    }
}