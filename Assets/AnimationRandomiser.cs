using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRandomiser : MonoBehaviour
{
    public Animator anim;
    public int anims;
    int lastRoll;

    public void PickRandom() {
        int newRoll = Random.Range(1, anims + 1);
        while (newRoll == lastRoll)
        {
            newRoll = Random.Range(1, anims + 1);
        }
        lastRoll = newRoll;
        anim.SetTrigger(newRoll.ToString());
    }
}
