using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour {

    Vector2 oppodirec;
    Vector2 goaldirec;
    Vector2 direc;

    Rigidbody2D rbody;
    AthleteBrain brain;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        brain = GetComponent<AthleteBrain>();

        brain.useAbility = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        oppodirec = (Vector2)(brain.opponent.transform.position - transform.position);
        goaldirec = (Vector2)(brain.goal.transform.position - transform.position);

        if (goaldirec.magnitude > ((Vector2)(brain.goal.transform.position - brain.opponent.transform.position)).magnitude)
        {
            direc = goaldirec;
        }
        else
        {
            direc = oppodirec;
        }
        direc.Normalize();
        rbody.AddForce(rbody.mass * direc * brain.currentSpeed);
    }
}
