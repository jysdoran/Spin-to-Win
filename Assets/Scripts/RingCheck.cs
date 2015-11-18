using UnityEngine;
using System.Collections;

public class RingCheck : MonoBehaviour {

    public float ringDamage;

    void OnTriggerExit2D(Collider2D other)
    {
        //other.gameObject.GetComponent<AthleteMovement>().currentHealth -= ringDamage;
        Debug.Log("KO!: "+other.gameObject.GetComponent<AthleteBrain>().opponent.GetComponent<AthleteBrain>().name+" Wins!");
        Destroy(other.gameObject.GetComponent<AthleteBrain>().opponent);
        Destroy(other.gameObject);

        matchControl mc = gameObject.GetComponent<matchControl>();
        mc.StartMatch(0);
    }
}
