using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signpost : MonoBehaviour
{
    public GameObject canvas;
    public List<SignpostText> textFields;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(true);
            for (int i = 0; i < textFields.Count; i++)
            {
                textFields[i].ScaleUp();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(false);
            //for (int i = 0; i < textFields.Count; i++)
            //{
            //    textFields[i].ScaleUp();
            //}
        }
    }
}
