using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouleNeige : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _temps = 5;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(Random.Range(-45, 50), Random.Range(0, 361), Random.Range(-45, 50));
        _rb.AddForce(transform.forward * Random.Range(100, 500));
    }

    // Update is called once per frame
    void Update()
    {
        _temps -= Time.deltaTime;
        if(_temps <= 0){
            Destroy(gameObject);
        }
    }


    private /// <summary>
            /// OnTriggerEnter is called when the Collider other enters the trigger.
            /// </summary>
            /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
    

}
