using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dents : MonoBehaviour
{
    [SerializeField] private SOPerso _perso;
    private AudioSource _audio;
    [SerializeField] public AudioClip _sonDGT;
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _perso.NbEnnemiTuer = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Ennemis")
        {
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Destroy(other.gameObject);
            _perso.NbEnnemiTuer++;
            _audio.PlayOneShot(_sonDGT);
        }
    }
}
