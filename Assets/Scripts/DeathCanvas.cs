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

    public void PlayFadeIn() {
        PlayerManager.instance.Respawn();
        anim.Play("DeathFadeIn");
    }
}
