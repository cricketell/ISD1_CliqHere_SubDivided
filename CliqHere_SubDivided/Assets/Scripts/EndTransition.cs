using UnityEngine;
using UnityEngine.UI;

public class EndTransition : MonoBehaviour
{

    public GameObject endComic;
    public GameObject endMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Invoke("TransitionToMenu", 3); //goes to end menu after 3 seconds

    }

    public void TransitionToMenu()
    {

        endComic.SetActive(false);
        endMenu.SetActive(true);

    }

}
