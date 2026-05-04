using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonSettings;
    [SerializeField] private Button buttonCredits;
    [SerializeField] private Button buttonExit;
    [SerializeField] private Button buttonSettingsBack;
    [SerializeField] private Button buttonCreditsBack;

    [SerializeField] private CanvasGroup panelMain;
    [SerializeField] private CanvasGroup panelSettings;
    [SerializeField] private CanvasGroup panelCredits;

    private void Awake()
    {
        buttonPlay.onClick.AddListener(OnButtonPlayClicked);
        buttonSettings.onClick.AddListener(OnButtonSettingsClicked);
        buttonCredits.onClick.AddListener(OnButtonCreditsClicked);
        buttonExit.onClick.AddListener(OnButtonExitClicked);
        buttonSettingsBack.onClick.AddListener(OnButtonSettingsBackClicked);
        buttonCreditsBack.onClick.AddListener(OnButtonCreditsBackClicked);
    }

    private void OnDestroy()
    {
        buttonPlay.onClick.RemoveAllListeners();
        buttonSettings.onClick.RemoveAllListeners();
        buttonCredits.onClick.RemoveAllListeners();
        buttonExit.onClick.RemoveAllListeners();
        buttonSettingsBack.onClick.RemoveAllListeners();
        buttonCreditsBack.onClick.RemoveAllListeners();

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

    private void OnButtonCreditsClicked()
    {
        SetPanel(panelMain, false);
        SetPanel(panelCredits, true);
    }

    private void OnButtonSettingsBackClicked()
    {
        SetPanel(panelMain, true);
        SetPanel(panelSettings, false);
    }

    private void OnButtonCreditsBackClicked()
    {
        SetPanel(panelMain, true);
        SetPanel(panelCredits, false);
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
