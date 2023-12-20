using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorielUI : MonoBehaviour
{

    [SerializeField] private GameObject _tutorielUI;

    [SerializeField] float _temps = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("RetirerTutoriel", _temps);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RetirerTutoriel()
    {
        Destroy(_tutorielUI);
    }
}
