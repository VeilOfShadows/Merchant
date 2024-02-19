using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player SO Connection", menuName = "Create/Misc/NPlayer SO Connection")]
public class PlayerSOConnections : ScriptableObject
{
    public string methodName;

    //public void Invoke() 
    //{
    //    OpenShop();
    //}

    public void OpenShop() {
        //methodName = "OpenShop;
        DialogueFunctionManager.instance.OpenShop();
    }

    public void AcceptQuest() {
        DialogueFunctionManager.instance.AcceptQuest();
    }
}
