using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public delegate void HitSoundPlay(Collision soundPlay);

    public static event HitSoundPlay OnHitSoundPlay;

    void OnCollisionEnter(Collision soundPlay)
    {
        if (soundPlay.collider.tag == "Obstacle")
        {
            if (OnHitSoundPlay != null)
            {
                OnHitSoundPlay(soundPlay);
            }
        }
    }
}
