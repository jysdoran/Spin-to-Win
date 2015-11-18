using UnityEngine;
using System.Collections;

public class RingCheck : MonoBehaviour {

    public float ringDamage;
    matchControl mc;

    void Start()
    {
        mc = gameObject.GetComponent<matchControl>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //other.gameObject.GetComponent<AthleteMovement>().currentHealth -= ringDamage;
        Debug.Log("KO!: "+other.gameObject.GetComponent<AthleteBrain>().opponent.GetComponent<AthleteBrain>().name+" Wins!");

        mc.king = other.gameObject.GetComponent<AthleteBrain>().opponent.GetComponent<AthleteBrain>().number;

        Destroy(other.gameObject.GetComponent<AthleteBrain>().opponent);
        Destroy(other.gameObject);

        mc.StartMatch(0);
    }
}
