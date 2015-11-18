using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    AthleteBrain brain;
    Vector2 direc;
    Rigidbody2D rbody;


    void Start()
    {
        brain = GetComponent<AthleteBrain>();
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        brain.useAbility = Input.GetButton("Jump");
    }

    void FixedUpdate()
    {

        direc = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        direc.Normalize();

        rbody.AddForce(rbody.mass * direc * brain.currentSpeed);
    }
}
