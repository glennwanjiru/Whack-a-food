using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button[] difficultyButtons; // Array to store difficulty buttons

    private int score;
    private float spawnRate = 1.0f;
    public bool isGameActive;

    void Start()
    {
        restartButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            if (targets.Count > 0)
            {
                int index = Random.Range(0, targets.Count);
                Instantiate(targets[index]);
                Debug.Log("Spawned: " + targets[index].name); // Debug to check if targets are spawning
            }
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score : " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
        // Show difficulty buttons
        foreach (Button button in difficultyButtons)
        {
            button.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);

        // Hide difficulty buttons when the game starts
        foreach (Button button in difficultyButtons)
        {
            button.gameObject.SetActive(false);
        }

        // Hide game over and restart UI
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        Debug.Log("Game Started");
    }
}
