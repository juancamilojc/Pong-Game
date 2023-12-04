using UnityEngine;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour {
    public UnityEvent OnStartGame;

    public void OnStartButtonClicked() {
        OnStartGame?.Invoke();
    }

    public void OnExitButtonClicked() {
        //Debug.Log("Sair!");
        Application.Quit();
    }
}