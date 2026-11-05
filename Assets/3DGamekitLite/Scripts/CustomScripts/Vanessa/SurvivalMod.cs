using Gamekit3D;
using UnityEngine;

public class SurvivalMod : MonoBehaviour
{
    [SerializeField] private float thirstLevel = 100f;
    [SerializeField] private float thirstSpeed;
    [SerializeField] private float timer = 10f;

    [SerializeField] private bool thirsty;
    [SerializeField] private bool parched;

    [SerializeField] private GameObject playerObject; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }
    
    public void RestoreThirst()
    {
        thirstLevel = 100f;
        thirsty = false;
        parched = false;
        Debug.Log("Thirst Restored!");
    }
    
    public void SmashBox()
    {
        if (Random.Range(1, 3) == 1)
        {
            RestoreThirst();
            Debug.Log("thirst Restored!");
        }
        else 
        {
            Debug.Log("No Water Found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        
        if (thirstLevel <= 0 && thirsty)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
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
                        timer = 10f;
            }
            
        }
    }
}
