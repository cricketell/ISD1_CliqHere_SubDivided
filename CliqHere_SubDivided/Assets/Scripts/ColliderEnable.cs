using UnityEngine;

public class ColliderEnable : MonoBehaviour
{
    public GameObject collisionObj;
    public GameObject objToEnable; //Object that will be enabled or disabled
    public GameObject objClicked; //Object that was clicked

    public void OnMouseDown()
    {

        collisionObj.SetActive(true);
        objToEnable.SetActive(false);
        objClicked.SetActive(false);

    }

}
