using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneLoader : MonoBehaviour
{

    public string endScene = "End";

    void OnMouseDown()
    {

        SceneManager.LoadScene(endScene);

    }

}
