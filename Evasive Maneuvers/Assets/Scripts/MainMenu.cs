using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorial;
    private bool actionLocked = false;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
        //Debug.Log (tutorial.GetComponent<Image>().color.a);
	}

	public void StartGameButton()
	{
		SceneManager.LoadScene("main");
	}

    public void ShowTutorial()
    {
        StartCoroutine(TutorialCoroutine(0.05f, 1, true));
    }

    public void HideTutorial()
    {
        StartCoroutine(TutorialCoroutine(-0.05f, 0, false));
    }

    IEnumerator TutorialCoroutine(float delta, int destination, bool active)
    {
        if (actionLocked) { yield break; }
        else { actionLocked = true; }

        tutorial.SetActive(true);
        Color color = tutorial.GetComponent<Image>().color;
        while (true)
        {
            if ((delta < 0 && tutorial.GetComponent<Image>().color.a <= destination) ||
                (delta > 0 && tutorial.GetComponent<Image>().color.a >= destination))
                break;

            color.a += delta;
            tutorial.GetComponent<Image>().color = color;

            color = tutorial.GetComponent<Image>().color;
            yield return null;
        }
        color.a = destination;
        tutorial.GetComponent<Image>().color = color;
        tutorial.SetActive(active);

        actionLocked = false;
        yield break;
    }
}
