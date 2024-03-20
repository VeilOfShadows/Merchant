using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCanvas : MonoBehaviour
{
    public Animation anim;

    public void PlayFadeOut() 
    {
        anim.Play("DeathFade");
    }

    public void TriggerRespawn() { 
        PlayerManager.instance.Respawn(this);
    }
    
    public void PlayFadeIn() {
        anim.Play("DeathFadeIn");
    }
}
