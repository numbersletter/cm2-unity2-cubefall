using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

/*
    * GameControl class is attached to GameHandler object
    * and manages key global variables in the game, as well as
    * the general state of the game.  

*/
public class GameControl : MonoBehaviour
{
   
    public bool gameOver = false;
  
    public float timer = 0f;

    public GameObject winText;

    // Update is called once per frame
    void Update()
    {
        // if game is still running
        if(!gameOver)
        {
            timer += Time.deltaTime;
        }
        // if game is over
        else
        {
            winText.GetComponent<TextMeshProUGUI>().text = "You survived: " + timer.ToString("#.00") + " seconds";
        }
    }

    public void endGame(){
        gameOver = true;
        StartCoroutine(restart());
    }

    IEnumerator restart(){
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
