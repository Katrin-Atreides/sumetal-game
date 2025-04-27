using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSFX : MonoBehaviour
{
    AudioSource source;
    float origPitch;
    public AudioClip collisionSound, bumpingSound;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        origPitch = source.pitch;
    }

    private void OnCollisionEnter(Collision collision)
    {
        source.pitch = Random.Range(origPitch - 0.1f, origPitch + 0.1f);

        if (collision.gameObject.tag == "enemy") source.PlayOneShot(collisionSound);
        else source.PlayOneShot(bumpingSound);
    }
}
