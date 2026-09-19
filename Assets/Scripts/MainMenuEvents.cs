using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _mainMenuDoc;

    private Button _playButton;
    private Button _highScoresButton;
 
    private void Awake()
    {
        _mainMenuDoc = GetComponent<UIDocument>();
        _playButton = _mainMenuDoc.rootVisualElement.Q<Button>("PlayButton") as Button;
        _highScoresButton = _mainMenuDoc.rootVisualElement.Q<Button>("HighScoresButton") as Button;

        _playButton.RegisterCallback<ClickEvent>(OnPlayButtonClick);
        _highScoresButton.RegisterCallback<ClickEvent>(OnHighScoresButtonClick);
    }
 
    private void OnPlayButtonClick(ClickEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CubeGame");
    }

    private void OnHighScoresButtonClick(ClickEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("HighScoresScene");
    }

    private void OnDisable()
    {
        _playButton.UnregisterCallback<ClickEvent>(OnPlayButtonClick);
        _highScoresButton.UnregisterCallback<ClickEvent>(OnHighScoresButtonClick);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
