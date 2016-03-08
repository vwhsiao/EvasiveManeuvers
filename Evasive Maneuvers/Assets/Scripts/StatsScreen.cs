using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StatsScreen : MonoBehaviour
{
    public GameObject canvas;

	// Use this for initialization
	void Start()
    {
        GameManager.instance.StatsUI.transform.SetParent(canvas.transform);
        Transform statsUI = canvas.transform.FindChild("StatsUI");
        statsUI.SetSiblingIndex(0);
	}

    public void ToMainMenu()
    {
        DeleteGameManager();
        SceneManager.LoadScene("main_menu");
    }

    public void PlayAgain()
    {
        DeleteGameManager();
        SceneManager.LoadScene("main");
    }

    private void DeleteGameManager()
    {
        Destroy(GameManager.instance.gameObject);
        GameManager.instance = null;
    }
}
