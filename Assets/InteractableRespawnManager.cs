using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRespawnManager : MonoBehaviour
{
    public static InteractableRespawnManager instance;
    public List<Interactable> onCooldown = new List<Interactable>();
    List<Interactable> toRemove = new List<Interactable>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ReduceCooldowns() 
    {
        for (int i = 0; i < onCooldown.Count; i++)
        {
            onCooldown[i].currentCooldown -= 1;
            if (onCooldown[i].currentCooldown <= 0)
            {
                onCooldown[i].Respawn();
                toRemove.Add(onCooldown[i]);
            }
        }

        CleanList();
    }

    void CleanList() 
    {
        if (toRemove.Count > 0)
        {
            for (int i = 0; i < toRemove.Count; i++)
            {
                for (int j = 0; j < onCooldown.Count; j++)
                {
                    if (onCooldown[i] == toRemove[i])
                    {
                        onCooldown.RemoveAt(j);
                    }
                }
            }
        }
    }
}
