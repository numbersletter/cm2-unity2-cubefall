using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
    * GameControl class is attached to GameHandler object
    * and manages key global variables in the game, as well as
    * the general state of the game.  

*/
public class GameControl : MonoBehaviour
{
   
    public bool gameOver = false;
  
    private float score = 0f;

    public GameObject GameOngoingParent;

    public GameObject GameOverOverlayParent;

    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI gameOverScoreText;

    private void Start()
    {
        GameOngoingParent.SetActive(true);
        GameOverOverlayParent.SetActive(false);

        // obtain the text fields we will be changing 
        scoreText = GameOngoingParent.GetComponentInChildren<TextMeshProUGUI>(true);
        gameOverScoreText = GameOverOverlayParent.transform.Find("GameOverScoreText").GetComponent<TextMeshProUGUI>();

        if (scoreText == null)
        {
            Debug.LogError("GameOngoingParent must contain a TextMeshProUGUI ScoreText child.", this);
        }

        if (gameOverScoreText == null)
        {
            Debug.LogError("GameOverOverlayParent must contain a TextMeshProUGUI score child.", this);
        }
    }

    // subscribe to the EnemyPassedKillZone event when the GameControl object is enabled, and unsubscribe when it is disabled
    private void OnEnable()
    {
        EnemyScript.EnemyPassedKillZone += AddEnemyDodgeBonus;
    }
    private void OnDisable()
    {
        EnemyScript.EnemyPassedKillZone -= AddEnemyDodgeBonus;
    }

    // Update is called once per frame
    void Update()
    {
        // if game is still running
        if(!gameOver)
        {
            score += Time.deltaTime;

            if (scoreText != null)
            {
                scoreText.text = "Score: " + score.ToString("#.00");
            }
        }
    }

    public void AddEnemyDodgeBonus()
    {
        if (!gameOver)
        {
            score += 10f;
        }
    }

    public void endGame()
    {

        // prevent endGame from running multiple times
        if (gameOver)
        {
            return;
        }
        
        // now change the state of the game
        gameOver = true;
        
        // disable the current in game score text 
        GameOngoingParent.SetActive(false);
        
        // set the score in the gameover scren 
        if (gameOverScoreText != null)
        {
            gameOverScoreText.text = "Score: " + score.ToString("#.00");
        }

        // enable the GameOver (this is overlaying the current screen)
        GameOverOverlayParent.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}
