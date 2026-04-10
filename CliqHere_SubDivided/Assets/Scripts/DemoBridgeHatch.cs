using UnityEngine;

public class DemoBridgeHatch : MonoBehaviour
{

    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        animator = GetComponent<Animator>();

    }

    
    void OnMouseDown()
    {

        animator.SetBool("IsOpen", true);

    }

}
