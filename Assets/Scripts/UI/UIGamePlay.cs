using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField] private TowerBehavior tower;
    [SerializeField] private CanvasGroup panelGameOver;

    private void Awake()
    {
        tower.GameOver += GameOverSceen;
    }


    private void GameOverSceen()
    {
        Time.timeScale = 0;
        SetPanel(panelGameOver, true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void SetPanel(CanvasGroup panel, bool state)
    {
        panel.alpha = state ? 1 : 0;
        panel.interactable = state;
        panel.blocksRaycasts = state;
    }
}
