using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject credits;
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
        StartCoroutine(ImageCoroutine(tutorial, 0.05f, 1, true));
    }

    public void HideTutorial()
    {
        StartCoroutine(ImageCoroutine(tutorial, -0.05f, 0, false));
    }

    public void ShowCredits()
    {
        StartCoroutine(ImageCoroutine(credits, 0.05f, 1, true));
    }
    
    public void HideCredits()
    {
        StartCoroutine(ImageCoroutine(credits, -0.05f, 0, false));
    }

    IEnumerator ImageCoroutine(GameObject image, float delta, int destination, bool active)
    {
        if (actionLocked) { yield break; }
        else { actionLocked = true; }

        image.SetActive(true);
        Color color = image.GetComponent<Image>().color;
        while (true)
        {
            if ((delta < 0 && image.GetComponent<Image>().color.a <= destination) ||
                (delta > 0 && image.GetComponent<Image>().color.a >= destination))
                break;

            color.a += delta;
            image.GetComponent<Image>().color = color;

            color = image.GetComponent<Image>().color;
            yield return null;
        }
        color.a = destination;
        image.GetComponent<Image>().color = color;
        image.SetActive(active);

        actionLocked = false;
        yield break;
    }
}
