using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitApplication : MonoBehaviour
{
    
    public void Quit()
    {

        Debug.Log("App has quit");
        Application.Quit();

    }

}
