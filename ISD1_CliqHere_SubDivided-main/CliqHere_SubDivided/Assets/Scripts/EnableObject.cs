using UnityEngine;

public class EnableObject : MonoBehaviour
{

    public GameObject objToEnable; //Object that will be enabled or disabled
    public GameObject objClicked; //Object that was clicked
    
    private void OnMouseDown()
    {

        objToEnable.SetActive(false);
        objClicked.SetActive(false);

    }

}
