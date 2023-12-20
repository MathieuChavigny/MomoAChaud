using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poungi : MonoBehaviour
{

    [SerializeField] Canvas _canvas;
    [SerializeField] SOPerso _perso;
    [SerializeField] AudioClip _yippee;
    [SerializeField] AudioSource _audio;

    bool _isAudioPlayed = false;
    
    // Start is called before the first frame update
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
        if (other.CompareTag("Player"))
        {
            _canvas.enabled = true;
            _perso.PeutAcheterCarotte = true;
            if (_isAudioPlayed == false)
            {
                _isAudioPlayed = true;
                _audio.PlayOneShot(_yippee);
                Invoke("resetEtat", _yippee.length);
            }
        }
    }

    private void resetEtat()
    {
        _isAudioPlayed = false;
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        _canvas.enabled = false;
        _perso.PeutAcheterCarotte = false;
    }
}
