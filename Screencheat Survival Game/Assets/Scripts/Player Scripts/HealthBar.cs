using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider sliderHealth;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth()
    {
        //finds the player and gets the health component
        GameObject Avatar = GameObject.Find("Hunter");
        HunterMovement playerScript = Avatar.GetComponent<HunterMovement>();

        //sets the health bar value to the player's max health
        sliderHealth.maxValue = playerScript.health;
        sliderHealth.value = playerScript.health;

        //fills the colour in
        fill.color = gradient.Evaluate(1F);
    }


    public void SetHealth()
    {
        //does the same as before
        GameObject Avatar = GameObject.Find("Avatar");
        HunterMovement playerScript = Avatar.GetComponent<HunterMovement>();

        //sets the player's current health
        sliderHealth.value = playerScript.health;
        fill.color = gradient.Evaluate(sliderHealth.normalizedValue);
    }

    public void Damage()
    {
        //same as before
        GameObject Avatar = GameObject.Find("Avatar");
        HunterMovement playerScript = Avatar.GetComponent<HunterMovement>();

        //when damaged, remove five health
        playerScript.health -= 5;

        //sets the value for the bar
        sliderHealth.value = playerScript.health;

        //if the player is dead, print this
        if (playerScript.health <= 0)
        {
            print("you are dead");
        }
    }
}
