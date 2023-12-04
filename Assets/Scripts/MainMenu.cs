using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public UnityEvent OnStartGame;

    public void OnStartButtonClicked() {
        //SceneManager.LoadScene(1);
        OnStartGame?.Invoke();
    }

    public void OnExitButtonClicked() {
        Debug.Log("Sair!");
        Application.Quit();
    }
}