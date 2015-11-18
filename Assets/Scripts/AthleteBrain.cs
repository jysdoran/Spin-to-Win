using UnityEngine;
using System.Collections;

public class AthleteBrain: MonoBehaviour
{
    public float baseSpeed;
    public float currentSpeed;
    public GameObject goal;
    public float abilityCooldown;
    public float abilityChance;
    public int abilityType;
    public float size;
    public GameObject opponent;
    public float baseHealth;
    public float abilityPower;
    public string title;
    public bool anchored;
    public float currentHealth;
    public float baseMass;
    public bool useAbility;
    public string id;
    public int number;
    public bool ai;
    float abilityTimer;
    float usedTimer;
    Vector2 direc;
    Rigidbody2D rbody;


    void Start()
    {
        usedTimer = 0;
        rbody = GetComponent<Rigidbody2D>();
        rbody.mass = baseMass;
        currentSpeed = baseSpeed;
        currentHealth = baseHealth;
        abilityTimer = abilityCooldown;
        if (ai)
        {
            gameObject.AddComponent<AIMovement>();
        } else
        {
            gameObject.AddComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        size = ((rbody.mass) / (rbody.mass + 0.5f)) + 0.4f;
        transform.localScale = new Vector3(size*1.5f,size*1.5f,0f);
        transform.GetChild(0).localScale = (new Vector3(1f, 1f,0f)) * (abilityTimer / abilityCooldown);

        if (abilityTimer <= 0 && useAbility)
        {
            UseAbility();
            abilityTimer = abilityCooldown;
        
        } else if (abilityTimer > 0)
        {
            abilityTimer -= Time.deltaTime;
            
        }
    }

    void FixedUpdate()
    {
        if (rbody.mass > baseMass)
        {
            rbody.mass = baseMass + ((rbody.mass - baseMass) * (abilityTimer / abilityCooldown));
        } else if (currentSpeed > baseSpeed)
        {
            currentSpeed = baseSpeed + ((currentSpeed-baseSpeed) * (abilityTimer / abilityCooldown));
        } else if (anchored)
        {
            usedTimer++;
            rbody.velocity = Vector3.zero;
            if (usedTimer > abilityPower * 20)
            {
                usedTimer = 0;
                anchored = false;
            }
        }

    }

    public void UseAbility()
    {
        switch (abilityType)
        {
            case 0:
                rbody.mass *= (abilityPower*10) + 0.5f;
                break;
            case 1:
                currentSpeed *= (abilityPower*4) + 0.5f;
                break;
            case 2:
                rbody.AddForce(rbody.mass * direc * currentSpeed * abilityPower * 50);
                break;
            case 3:
                anchored = true;
                break;
            case 4:
                rbody.mass *= (abilityPower * 10) + 0.5f;
                break;
            default:
                break;
        }
    }
}