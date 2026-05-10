using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonMainMenu;

    private void Awake()
    {
        buttonRestart.onClick.AddListener(OnButtonRestartClicked);
        buttonMainMenu.onClick.AddListener(OnButtonMainMenuClicked);
    }

    private void OnDestroy()
    {
        buttonRestart.onClick.RemoveAllListeners();
        buttonMainMenu.onClick.RemoveAllListeners();

    }
    private void OnButtonRestartClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnButtonMainMenuClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
