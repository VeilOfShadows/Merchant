using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using Cinemachine;
//using Unity.PlasticSCM.Editor.WebApi;

public class RoadJunction : MonoBehaviour
{
    public List<RoadController> controllers = new List<RoadController>();

    public void EnterRoad(RoadController currentController) {
        StartCoroutine(Enter(currentController));
    }

    public IEnumerator Enter(RoadController currentController) {
        for (int i = 0; i < controllers.Count; i++)
        {
            controllers[i].DeactivateTriggers();
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < controllers.Count; i++)
        {
            if (controllers[i] != currentController)
            {
                controllers[i].ActivateTriggers();
            }
        }
    }
}
