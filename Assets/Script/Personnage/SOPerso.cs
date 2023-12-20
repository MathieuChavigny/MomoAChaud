using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Perso", menuName = "Perso")]
public class SOPerso : ScriptableObject
{



    private int _bouleDeNeige;
    public int BouleDeNeige { get => _bouleDeNeige; set => _bouleDeNeige = value; }

    private int _Glace;
    public int Glace { get => _Glace; set => _Glace = value; }

    private int bois;
    public int Bois { get => bois; set => bois = value; }

    private int _seed;
    public int Seed { get => _seed; set => _seed = value; }
    private bool _peutAcheterCarotte;
    public bool PeutAcheterCarotte { get => _peutAcheterCarotte; set => _peutAcheterCarotte = value; }

    private int _carrote;
    public int Carrote { get => _carrote; set => _carrote = value; }
    private int _nbBonhomme;
    public int NbBonhomme { get => _nbBonhomme; set => _nbBonhomme = value; }
    private int _nbEnnemiTuer;
    public int NbEnnemiTuer { get => _nbEnnemiTuer; set => _nbEnnemiTuer = value; }

    [SerializeField] private int _vie = 100;
    public int vie { get => _vie; set => _vie = value; }

    [SerializeField] private GameObject _UIJoueur;
    public GameObject UIJoueur { get => _UIJoueur; set => _UIJoueur = value; }

    public ChangerScene _changerSceneSOPerso;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("bonjour"+_changerSceneSOPerso);
        
    }

    private void _updateUI()
    {

    }
}
