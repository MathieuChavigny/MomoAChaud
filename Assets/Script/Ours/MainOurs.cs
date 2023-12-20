using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainOurs : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource _audio;
    [SerializeField] public AudioClip _sonDGT;
    [SerializeField] public SOPerso _perso;
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            // other.gameObject.GetComponent<Perso>()._perso.vie -= 10;
            _perso.vie -= 10;
            _audio.PlayOneShot(_sonDGT);
            // Debug.Log("vie" + other.gameObject.GetComponent<Perso>()._perso.vie);

        }

    }
}
