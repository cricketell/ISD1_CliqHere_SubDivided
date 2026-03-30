using UnityEngine;

public class CreatureMove : MonoBehaviour
{

    public Transform origin; //Creature starting point
    public Transform target; //Creature target
    public float moveSpd;
    public int moving; //forward = 0, back = 1

    private Vector3 flip; //flipped local scale X axis
    private Vector3 original; //original local scale X axis

    //Animator animator;

    void Start()
    {

        moving = 0; //starts with the Creature moving towards the target

        flip = new Vector3(-1, 1, 1); //setting Vector3 to control the Creature flipping to suit the direction
        original = new Vector3(1, 1, 1); //original scale

    }

    private void OnMouseDown()
    {

        moving = 1; //sends Creature back to origin

    }

    void Update()
    {

        if (moving == 0)
        {

            //Moves the Creature towards the target point at the set movement speed
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpd * Time.deltaTime);
            //Sets the local scale of the Creature to the original as it's moving forward 
            transform.localScale = original;

        }
        else if (moving == 1)
        {

            //Moves the Creature towards the origin point at the set movement speed
            transform.position = Vector3.MoveTowards(transform.position, origin.position, moveSpd * Time.deltaTime);
            //Sets the local scale of the Creature to the be flipped on the X axis as it's moving backwards 
            transform.localScale = flip;

        }

        if (transform.position == origin.position)
        {

            //Makes the Creature start moving towards the target once it's reached the origin point
            moving = 0;

        }
    }

}

//Video referenced: https://youtu.be/_UzRw_5xqxg?si=2iAwBgJji755ftRH

