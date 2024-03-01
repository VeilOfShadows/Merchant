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
    bool coroutineRunning = false;
    public GameObject playerPathTrigger;
    public void EnterRoad(RoadController currentController) {
        StartCoroutine(Enter(currentController));
    }

    public IEnumerator Enter(RoadController currentController) {
        if (coroutineRunning)
        {
            yield break;
        }
        coroutineRunning = true;
        //currentController.DeactivateTriggers();
        //for (int i = 0; i < controllers.Count; i++)
        //{
        //    controllers[i].DeactivateTriggers();
        //}

        //yield return new WaitForSeconds(.3f);

        for (int i = 0; i < controllers.Count; i++)
        {
            if (controllers[i] != currentController)
            {
                controllers[i].ActivateTriggers();
            }
            else
            {
                controllers[i].DeactivateTriggers();
            }
        }
        playerPathTrigger.SetActive(false);
        yield return new WaitForSeconds(.5f);
        playerPathTrigger.SetActive(true);

        //for (int i = 0; i < controllers.Count; i++)
        //{
        //    controllers[i].DeactivateTriggers();
        //}

        //playerPathTrigger.SetActive(false);
        //yield return new WaitForSeconds(.3f);

        //for (int i = 0; i < controllers.Count; i++)
        //{
        //    if (controllers[i] != currentController)
        //    {
        //        controllers[i].ActivateTriggers();
        //    }
        //}
        //yield return new WaitForSeconds(.2f);
        //playerPathTrigger.SetActive(true); 
        coroutineRunning = false;
    }
}
