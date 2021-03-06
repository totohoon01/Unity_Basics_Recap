using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerrelCtrl : MonoBehaviour
{
    //Audio Event
    private new AudioSource audio;
    public AudioClip audioSource;
    //Renderer Event
    private new MeshRenderer renderer;
    public Texture[] textures;
    //Exploding Event
    private int hitCount = 0;
    public GameObject expEffect;

    void Start()
    {
        audio = GetComponent<AudioSource>();

        renderer = GetComponentInChildren<MeshRenderer>();
        int idx = Random.Range(0, textures.Length);
        renderer.material.mainTexture = textures[idx];
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            hitCount++;
            if (hitCount >= 3)
            {
                ExplodeBarrel();
            }
        }
    }

    void ExplodeBarrel()
    {
        //Exp 
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 1000.0f);
        Destroy(this.gameObject, 2.0f);
        //Exp Sound
        audio.PlayOneShot(audioSource, 1.0f);
        //Exp Effect
        GameObject obj = Instantiate(expEffect, this.transform.position, Quaternion.identity);
        Destroy(obj, 4.0f);
    }
}
