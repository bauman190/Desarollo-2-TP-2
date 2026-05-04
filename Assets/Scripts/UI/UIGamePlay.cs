using UnityEngine;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField] private GameObject panelSettings;


    private bool isPause = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        
    }
    void TogglePause()
    {
        isPause = !isPause;
        Time.timeScale = isPause ? 0 : 1;
        panelSettings.SetActive(isPause);
        Cursor.lockState = isPause ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPause;
    }
}
