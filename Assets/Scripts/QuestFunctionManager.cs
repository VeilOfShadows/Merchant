using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFunctionManager : MonoBehaviour
{
    public static QuestFunctionManager instance;

    public GameObject oswaldObject;
    public GameObject thistlewoodRoad;
    public GameObject thistlewoodGate;
    public GameObject thistlewoodGateEast;
    public GameObject lakeviewBridgeRoad;
    public GameObject lakeviewBridgeBroken;
    public GameObject lakeviewBridgeRepaired;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Activate(string questID)
    {
        Invoke("Quest" + questID, 0f);
    }

    //Lakeview bridge Activate Oswald
    [ContextMenu("ThistlewoodGate")]
    void Quest1() { 
        oswaldObject.SetActive(true);
        thistlewoodRoad.SetActive(true);
        thistlewoodGate.transform.localEulerAngles = new Vector3(0, -90, 0);
        thistlewoodGateEast.transform.localEulerAngles = new Vector3(0, -90, 0);
    }

    //Repair Lakeview Bridge
    void Quest1a()
    {
        oswaldObject.SetActive(false);
        lakeviewBridgeBroken.SetActive(false);
        lakeviewBridgeRepaired.SetActive(true);
        lakeviewBridgeRoad.SetActive(true);
    }
}
