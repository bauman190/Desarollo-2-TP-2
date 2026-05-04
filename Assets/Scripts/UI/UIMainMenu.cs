using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonSettings;
    [SerializeField] private Button buttonExit;
    [SerializeField] private Button buttonBack;

    [SerializeField] private CanvasGroup panelMain;
    [SerializeField] private CanvasGroup panelSettings;

    private void Awake()
    {
        buttonPlay.onClick.AddListener(OnButtonPlayClicked);
        buttonSettings.onClick.AddListener(OnButtonSettingsClicked);
        buttonExit.onClick.AddListener(OnButtonExitClicked);
        buttonBack.onClick.AddListener(OnButtonBackClicked);
    }

    private void OnDestroy()
    {
        buttonPlay.onClick.RemoveAllListeners();
        buttonSettings.onClick.RemoveAllListeners();
        buttonExit.onClick.RemoveAllListeners();
        buttonBack.onClick.RemoveAllListeners();
    }
    private void OnButtonPlayClicked()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("GamePlay");
    }
    private void OnButtonSettingsClicked()
    {
        SetPanel(panelMain, false);
        SetPanel(panelSettings, true);
    }
   
    private void OnButtonBackClicked()
    {
        SetPanel(panelMain, true);
        SetPanel(panelSettings, false);
    }
    private void SetPanel(CanvasGroup panel, bool state)
    {
        panel.alpha = state ? 1:0;
        panel.interactable = state;
        panel.blocksRaycasts = state;
    }
    private void OnButtonExitClicked()
    {
#if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit(0);
#endif
    }
}
