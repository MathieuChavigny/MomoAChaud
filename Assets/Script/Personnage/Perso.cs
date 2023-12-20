using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Perso : MonoBehaviour
{
    [Header("Paramètres")]
    [SerializeField] private float _vitesseMouvement = 20.0f;
    [SerializeField] private float _vitesseRotation = 3.0f;
    [SerializeField] private float tempsSmoothRotation = 0.1f;
    [SerializeField] private Transform _champDeForce;
    [SerializeField] private float _vitesse = 0.0f;
    private float _vitesseSaut;
    private Vector3 directionMouvement = Vector3.zero;
    float smoothRotationVelocity;
    private bool _enSaut = false;
    [SerializeField] private Transform _camera;

    [Header("Paramètres Saut")]
    [SerializeField] private float _impulseSaut = 30.0f;
    [SerializeField] private float _gravite = 0.2f;

    [Header("Composants")]
    private Animator _anim;
    private CharacterController _controller;

    [Header("Objets")]
    [SerializeField] private GameObject BonhommesNeige;
    private int _coutBonhommeNeige = 100;
    private int _coutCarotte = 25;
    [SerializeField] private Collider _dentCollider;


    [Header("donnéesPerso")]
    [SerializeField] public SOPerso _perso;
    public SOPerso donneePerso { get => _perso; set => _perso = value; }
    [SerializeField] public ChangerScene _changerScene;
    public ChangerScene changerScene { get => _changerScene; set => _changerScene = value; }

    [Header("Sons")]
    private AudioSource _audio;
    [SerializeField] public AudioClip _sonSaut;
    [SerializeField] public AudioClip _sonAttaque;
    [SerializeField] public AudioClip _acheterCarotte;
    [SerializeField] public AudioClip _pasAssezDeBois;


    void Start()
    {
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _controller = GetComponent<CharacterController>();
        donneePerso._changerSceneSOPerso = changerScene;
        donneePerso.vie = 100;
        donneePerso.Bois = 0;
        donneePerso.BouleDeNeige = 0;
        donneePerso.Carrote = 0;
        donneePerso.NbBonhomme = 0;
        donneePerso.NbEnnemiTuer = 0;
        Debug.Log(donneePerso.NbEnnemiTuer);

    }

    // Update is called once per frame
    void Update()
    {
        // permet de définir la vitesse du perso à l'aide de la vitesse et de la détection des touche W et S en les multipliant entre eux et donne la valeur à vitesse
        float vitesseX = Input.GetAxis("Horizontal") * _vitesseMouvement;
        float vitesseY = Input.GetAxis("Vertical") * _vitesseMouvement;
        // permet de "set" le paramètre à true à l'aide du paramètre enCourse et vitesse en vérifiant si la vitesse du personnage est supérieur à 0 et définie si oui ou non, si il est en mouvement
        _anim.SetBool("enCourse", Mathf.Abs(vitesseY) > 0 || Mathf.Abs(vitesseX) > 0);
        // permet de définir la valeur de directionMouvement à l'aide de la variable vitesse en les attribuant à un vector3
        directionMouvement = new Vector3(vitesseX, 0, vitesseY);
        // permet de faire déplacer le personnage à l'aide de directionMouvement en les distribuant à la fonction TransformDirection qui j'estime est équivalent à un transform.Translate mais pour le characterController


        if (directionMouvement.magnitude >= 0.1f)
        {
            //=================== système de rotation ===================
            float targetAngle = Mathf.Atan2(directionMouvement.x, directionMouvement.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            // détermine l'angle qui est supposé être
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothRotationVelocity, tempsSmoothRotation);
            // fait la rotation
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //=========== système de d'assignation de direction à la vitesse ==========

            directionMouvement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
        {
            directionMouvement = Vector3.zero;
        }

        // directionMouvement = transform.TransformDirection(directionMouvement);
        // Debug.Log(directionMouvement);
        // permet de définir la vitesse du saut à la variable impulsion le personnage à l'aide de la détection de la touche espace et de la variable du characterController : isGrounded en vérifiant si le personnage est au sol et si la touche espace est pressée
        if (Input.GetButtonDown("Jump") && _controller.isGrounded && _enSaut == false)
        {
            _enSaut = true;
            _anim.SetBool("enSaut", true);
            Invoke("Sauter", 0.25f);
        }
        // permet de "set" le paramètre à false à l'aide du paramètre isGrounded, vitesseSaut et impulseSaut en vérifiant si le personnage touche à  terre et si la vitesse du saut est inférieur à la valeur de l'impulsion du saut
        // _anim.SetBool("enSaut", !_controller.isGrounded && _vitesseSaut > -_impulseSaut);
        // permet de définir la valeur de directionMouvement à l'aide de la variable vitesse en les attribuant au vector.y du characterController
        directionMouvement.y += _vitesseSaut;
        // permet de faire un système de gravité à l'aide de la variable gravité et vitesseSaut en s'assurant que tant et aussi longtemps que le personnage n'est pas au sol, la gravité ne s'applique pas
        if (!_controller.isGrounded)
        {
            _vitesseSaut -= _gravite;
            // _anim.SetBool("enSaut", true);
            // if (true)
            // {
            // _anim.SetBool("enSaut", false);
            // }

        }
        // permet de faire déplacer le personnage sans problème lié à la physique à l'aide de la variable time.deltaTime et directionMouvement en les attribuant à la fonction Move du characterController
        // **   bonus 2    **
        // permet d'augmenter la grosseur du champ de force à l'aide de la vitesse du personnage en l'attribuant au localScale du champ de force
        _controller.Move(directionMouvement * _vitesse * Time.deltaTime);
        float vectorVitesse = Mathf.Sqrt((vitesseX * vitesseX) + (vitesseY * vitesseY));
        // Debug.Log(vectorVitesse);
        _champDeForce.localScale = Vector3.one * (vectorVitesse / 1.5f);
        // Debug.Log(_champDeForce.localScale);

        // if (Input.GetButtonDown("mouse 0"))
        // {
        //     _anim.SetBool("enAttaque", true);
        // }


        if (donneePerso.vie <= 0)
        {
            changerScene.Mort();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_perso.BouleDeNeige >= _coutBonhommeNeige && _perso.Carrote >= 1)
            {
                _perso.BouleDeNeige -= _coutBonhommeNeige;
                _perso.Carrote--;
                _perso.NbBonhomme++;
                Instantiate(BonhommesNeige, transform.position + new Vector3(0, 1, 0) + transform.forward * -1, Quaternion.identity);
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_perso.PeutAcheterCarotte && _perso.Bois >= _coutCarotte)
            {
                _perso.Carrote++;
                _perso.Bois -= _coutCarotte;
                _audio.PlayOneShot(_acheterCarotte);
            }
            else if (_perso.PeutAcheterCarotte)
            {
                _audio.PlayOneShot(_pasAssezDeBois);
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (_anim.GetBool("enAttaque") == false)
            {

                _anim.SetBool("enAttaque", true);
                _dentCollider.enabled = true;
                Invoke("Attaque", 0.8f);
            }
        }

    }

    private void Attaque()
    {
        _anim.SetBool("enAttaque", false);
        _audio.PlayOneShot(_sonAttaque);
        _dentCollider.enabled = false;
    }
    private void Sauter()
    {
        _vitesseSaut = _impulseSaut;
        _enSaut = false;
        _anim.SetBool("enSaut", false);
        _audio.PlayOneShot(_sonSaut);

    }



    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
    }


}