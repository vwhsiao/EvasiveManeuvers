using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StatsScreen : MonoBehaviour
{

	// Use this for initialization
	void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
	
	}

    public void ToMainMenu()
    {
        SceneManager.LoadScene("main_menu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("main");
    }
}
