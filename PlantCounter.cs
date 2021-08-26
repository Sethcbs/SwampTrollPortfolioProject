using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantCounter : MonoBehaviour
{
    public Text text;
    public Text congratulations;

    void Update()
    {
        //number of game objects with the tag "Enemy".
        float numberOfPlants = GameObject.FindGameObjectsWithTag("Enemy").Length;

        //set text to the number of remaining game object with the tag "Enemy".
        text.text = ("Plants Remaining:" + numberOfPlants);

        //if the number of game object with the tag "Enemy" is zero then set text to tell the player they won.
        if (numberOfPlants == 0)
        {
            congratulations.text = ("Congratulations, your swamp is yours!");
        }
    }

}
