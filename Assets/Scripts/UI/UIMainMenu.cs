using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;

    private void Awake()
    {
        buttonPlay.onClick.AddListener(OnButtonPlayClicked);
    }

    private void OnDestroy()
    {
        buttonPlay.onClick.RemoveAllListeners();
    }

    private void OnButtonPlayClicked()
    {
        gameObject.SetActive(false);
    }

}
