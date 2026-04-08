using UnityEngine;

public class CreatureMove : MonoBehaviour
{

    public Transform origin;
    public Transform target;
    public float moveSpd;
    public int moving;

    public DeathScreen deathScreen;

    private Vector3 flip;
    private Vector3 original;
    private bool hasKilled = false;

    void Start()
    {

        moving = 0;

        flip = new Vector3(-1, 1, 1);
        original = new Vector3(1, 1, 1);

    }

    private void OnMouseDown()
    {

        moving = 1;

    }

    void Update()
    {

        if (moving == 0)
        {

            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpd * Time.deltaTime);
            transform.localScale = original;

        }
        else if (moving == 1)
        {

            transform.position = Vector3.MoveTowards(transform.position, origin.position, moveSpd * Time.deltaTime);
            transform.localScale = flip;

        }

        //when the creature reaches the target it shows the death screen
        if (moving == 0 && !hasKilled && transform.position == target.position)
        {

            hasKilled = true;
            deathScreen.ShowDeathScreen();

        }

        //resets once the creature is back at origin so it can trigger again
        if (transform.position == origin.position)
        {

            hasKilled = false;
            moving = 0;

        }

    }

}

//Video referenced: https://youtu.be/_UzRw_5xqxg?si=2iAwBgJji755ftRH