using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _CubeRequis;
    [SerializeField] int _nbCubeRequis;
    [SerializeField] TextMeshProUGUI _CubeRestant;
    [SerializeField] TextMeshProUGUI _vie;
    [SerializeField] TextMeshProUGUI _neige;
    [SerializeField] TextMeshProUGUI _bois;
    [SerializeField] TextMeshProUGUI _carotte;

    private List<GameObject> _TousLesCubes;
    public List<GameObject> tousLesCubes { get; set; }

    private GameObject _Perso;
    public GameObject Perso { get => _Perso; set => _Perso = value; }



    // Start is called before the first frame update
    void Start()
    {
        _nbCubeRequis = tousLesCubes.Count * 70 / 100;
        _CubeRequis.text = "Cube requis : " + _nbCubeRequis;
        // _vie.text = "Vie : " + Perso.GetComponent<Perso>().donneePerso.vie;
        // _neige.text ="Boule de Neige: " + Perso.GetComponent<Perso>().donneePerso.BouleDeNeige;


        // Debug.Log(Perso.GetComponent<Perso>().donneePerso.vie + "vie");
    }


    // Update is called once per frame
    void Update()
    {
        _CubeRestant.text = "Cube restant : " + tousLesCubes.Count;
        _vie.text = "Vie : " + Perso.GetComponent<Perso>().donneePerso.vie;
        _neige.text ="Boule de Neige: " + Perso.GetComponent<Perso>().donneePerso.BouleDeNeige;
        _bois.text ="Bois: " + Perso.GetComponent<Perso>().donneePerso.Bois;
        _carotte.text ="Carotte(s): " + Perso.GetComponent<Perso>().donneePerso.Carrote;
    }
}
