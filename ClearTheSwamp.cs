using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearTheSwamp : MonoBehaviour
{
    public Text text;
    void Start()
    {
        //Put text up for player to see goal and then remove after 6 seconds.
        StartCoroutine(startingText());

        IEnumerator startingText()
        {
            text.text = ("You Must Reclaim Your Swamp From the Evil Plant Monsters");
            yield return new WaitForSeconds(6);
            text.text = ("");
        }
    }
}
