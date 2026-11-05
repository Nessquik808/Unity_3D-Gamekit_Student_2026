using Gamekit3D;
using UnityEngine;

public class SurvivalMod : MonoBehaviour
{
    [SerializeField] private float thirstLevel = 100f;
    [SerializeField] private float thirstSpeed;
    [SerializeField] private float timer = 10f;

    [SerializeField] private bool thirsty;
    [SerializeField] private bool parched;
    
    public int waterBottles = 0;

    [SerializeField] private GameObject playerObject; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        DrinkWater();
        
        /*Subtracting the current value of thirstLevel with thirstSpeed.
         thirstSpeed is normalizes by using Time.deltaTIme, so that it's frame rate independent. 
         Math.Clamp resrticts the thirstLevel between its current may value and 0. 
        */
        thirstLevel -= Mathf.Clamp(thirstSpeed*Time.deltaTime,0, thirstLevel);
        
        /* if staement chacks if thirstLevel is equal to, or lower that 50 
         */

        if (thirstLevel <= 50f && !thirsty)
        {
            Debug.Log("I'm getting thirsty...");  
            thirsty = true;
        }
        
        if (thirstLevel <= 0f && !parched)
        {
            Debug.Log("I'm parched!");  
            parched = true;
        }
        if (thirstLevel <= 0 && thirsty)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ThirstDamage();
                timer = 10f;
            }
            
        }
    }
    public void RestoreThirst()
    {
        thirstLevel = 100f;
        thirsty = false;
        parched = false;
        Debug.Log("Thirst Restored!");
    }
    private void ThirstDamage()
    {
        Damageable.DamageMessage msg = new Damageable.DamageMessage()
        {
            amount = 1,
            damager = this,
            damageSource = new Vector3(0, 0, 0),
            direction = new Vector3(0, 0, 0),
            throwing = false,
            stopCamera = false,
        };
        playerObject.GetComponent<Damageable>().ApplyDamage(msg);
        Debug.Log("Damage by Thirst!");
    }
    public void AddWater()
    {
        if (Random.Range(1, 4) == 1)
        {
            waterBottles++;
            Debug.Log("Water Bottles: " + waterBottles);
        }
        else 
        {
            Debug.Log("No Water Found!");
        }
    }
    private void DrinkWater()
    {
        if (Input .GetKeyDown(KeyCode.Q)) 
        {
            if (waterBottles > 0)
            {
                waterBottles--;
                Debug.Log("Drank Water! Water Bottles: " + waterBottles);
                RestoreThirst();
            }
            else
            {
                Debug.Log("No Water Bottles Left!");
            }
        }
    }
}
