using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField] private TowerBehavior tower;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text streakText;
    [SerializeField] private TMP_Text maxScoreText;
    [SerializeField] private TMP_Text hightText;

    [SerializeField] private CanvasGroup panelGameOver;


    private void Awake()
    {
        TowerBehavior.GameOver += GameOverSceen;
        tower.UpdateScore += UpdateUI;
    }


    private void UpdateUI(float score, float maxScore, int streak, int hight)
    {
        scoreText.text = score.ToString();
        streakText.text = streak.ToString();
        maxScoreText.text = maxScore.ToString();
        hightText.text = hight.ToString();
    }

    private void GameOverSceen()
    {
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
