using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class changeTransmogifier : MonoBehaviour
{
    public TextMeshPro text;

    TransmogifierScript tScript;
    public bool increase = true;

    void Start()
    {
        tScript = transform.parent.GetComponent<TransmogifierScript>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(increase)
            {
                tScript.Increase();
            }
            else
            {
                tScript.Decrease();
            }

            text.text = tScript.multAm.ToString();
        }
    }
}
