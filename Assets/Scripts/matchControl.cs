using UnityEngine;
using System.Collections;

public class matchControl : MonoBehaviour {

    public int matchType ;
    public string[] athleteIDs;
    public int athleteAmount ;
    public int matchTimer;
    public int matchDuration;
    public GameObject athlete1;
    public GameObject athlete2;
    public string[] nameStarts;
    public string[] nameMiddles;
    public string[] nameEnds;
    public int king;

    GameObject centre;
    // Use this for initialization
    void Start () {
        nameStarts = new string[13];
        nameStarts[0] = "Blarg" ;
        nameStarts[1] = "Pin";
        nameStarts[2] = "Pom";
        nameStarts[3] = "Dan";
        nameStarts[4] = "Tin";
        nameStarts[5] = "Vor";
        nameStarts[6] = "Mag";
        nameStarts[7] = "Mup";
        nameStarts[8] = "Ner";
        nameStarts[9] = "Quin";
        nameStarts[10] = "Dan";
        nameStarts[11] = "Mil";
        nameStarts[12] = "Dor";
        nameMiddles = new string[6];
        nameMiddles[0] = "na";
        nameMiddles[1] = "on";
        nameMiddles[2] = "a";
        nameMiddles[3] = "o";
        nameMiddles[4] = "u";
        nameMiddles[5] = "ax";
        nameEnds = new string[13];
        nameEnds[0] = "ton";
        nameEnds[1] = "man";
        nameEnds[2] = "paz";
        nameEnds[3] = "don";
        nameEnds[4] = "er";
        nameEnds[5] = "ter";
        nameEnds[6] = "ing";
        nameEnds[7] = "ant";
        nameEnds[8] = "elor";
        nameEnds[9] = "chil";
        nameEnds[10] = "dred";
        nameEnds[11] = "nia";
        nameEnds[12] = "an";
        matchTimer = 0;
        athleteIDs = new string[athleteAmount];
        for (int i=0;i<athleteAmount;i++)
        {
            athleteIDs[i] = GenerateAthleteID();
        }
        StartMatch(matchType);

        centre = GameObject.Find("Goal");
        king = -1;
    }

    void Update ()
    {
        matchTimer++;
        if (matchTimer > matchDuration*120)
        {
            Debug.Log("Draw!");
            Destroy(athlete1);
            Destroy(athlete2);
            king = -1;
            StartMatch(0);
            matchTimer = 0;
        }
    }

    public string GenerateAthleteName()
    {
        switch ((int)Mathf.Round(Random.value*2))
        {
            case 0:
                return nameStarts[(int)Mathf.Floor(Random.value * nameStarts.Length)] + nameMiddles[(int)Mathf.Floor(Random.value * nameMiddles.Length)] + nameEnds[(int)Mathf.Floor(Random.value * nameEnds.Length)];
            case 1:
                return nameStarts[(int)Mathf.Floor(Random.value * nameStarts.Length)] + nameEnds[(int)Mathf.Floor(Random.value * nameEnds.Length)];
            case 2:
                return nameStarts[(int)Mathf.Floor(Random.value * nameStarts.Length)] + "-" + GenerateAthleteName();
            default:
                return "ERROR";
        }
    }

    public string GenerateAthleteID()
    {
        float baseSpeed = (Random.value * 10) + 5;
        int abilityType = (int)Mathf.Floor(Random.value * 5f);
        float baseMass = Random.value * 2.5f;
        float abilityPower = Random.value + 0.5f;
        string title = GenerateAthleteName();
        float abilityCooldown = (Random.value * 3) + 1;
        Color mainColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        Color frontColor = new Color((1 - mainColor.r) + ((Random.value * 0.1f) - 0.2f), (1 - mainColor.g) + ((Random.value * 0.1f) - 0.2f), (1 - mainColor.b) + ((Random.value * 0.1f) - 0.2f), 1.0f);
        string id = baseSpeed + " " + abilityType + " " + baseMass + " " + abilityPower + " " + abilityCooldown + " " + mainColor.r + " " + mainColor.g + " " + mainColor.b + " " + frontColor.r + " " + frontColor.g + " " + frontColor.b + " " + name;
        return id;
    }

    public GameObject InitialiseAthleteFromID(string id,Vector3 spawnPosition)
    {
        string[] splitID = id.Split(" "[0]);
        GameObject champ = (GameObject)Instantiate(Resources.Load("Athlete"));
        champ.transform.position = spawnPosition;
        champ.GetComponent<AthleteBrain>().baseSpeed = float.Parse(splitID[0]);
        champ.GetComponent<AthleteBrain>().abilityType = int.Parse(splitID[1]);
        champ.GetComponent<AthleteBrain>().baseMass = float.Parse(splitID[2]);
        champ.GetComponent<AthleteBrain>().abilityPower = float.Parse(splitID[3]);
        champ.GetComponent<AthleteBrain>().abilityCooldown = float.Parse(splitID[4]);
        champ.GetComponent<SpriteRenderer>().color = new Color(float.Parse(splitID[5]), float.Parse(splitID[6]), float.Parse(splitID[7]), 1.0f);
        champ.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(float.Parse(splitID[8]), float.Parse(splitID[9]), float.Parse(splitID[10]), 1.0f);
        champ.GetComponent<Rigidbody2D>().mass = champ.GetComponent<AthleteBrain>().baseMass;
        champ.GetComponent<AthleteBrain>().goal = centre;
        champ.GetComponent<AthleteBrain>().title = splitID[11].Replace("-", " ");
        champ.name = splitID[11].Replace("-"," ") + "  Ability: " + champ.GetComponent<AthleteBrain>().abilityType;
        champ.GetComponent<AthleteBrain>().id = id;
        return champ ;
    }

    public void StartMatch(int type)
    {
        int num1 = (int)Mathf.Floor(Random.value * athleteAmount);
        string athleteID1 = athleteIDs[num1];
        string athleteID2;
        int num2;
        if (king >= 0)
        {
            athleteID2 = athleteIDs[king];
            num2 = king;
        }
        else
        {
            do
            {
                num2 = (int)Mathf.Floor(Random.value * athleteAmount);
                athleteID2 = athleteIDs[num2];
            } while (athleteID2 == athleteID1);
        }
        athlete1 = InitialiseAthleteFromID(athleteID1, new Vector3(2.2f, 2.5f, 0));
        athlete2 = InitialiseAthleteFromID(athleteID2, new Vector3(0, -2.8f, 0));
        athlete1.GetComponent<AthleteBrain>().opponent = athlete2;
        athlete2.GetComponent<AthleteBrain>().opponent = athlete1;
        athlete1.GetComponent<AthleteBrain>().goal = GameObject.Find("Goal");
        athlete2.GetComponent<AthleteBrain>().goal = GameObject.Find("Goal");
        athlete1.GetComponent<AthleteBrain>().number = num1;
        athlete2.GetComponent<AthleteBrain>().number = num2;
        athlete1.GetComponent<AthleteBrain>().ai = false;
    }
}
