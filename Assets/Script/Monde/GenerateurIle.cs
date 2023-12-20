using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.SceneManagement;

public class GenerateurIle : MonoBehaviour
{

    [SerializeField] private GameObject cubeBase; // variable pour le gameobject du cube
    [SerializeField] private int largeurIle = 10; // variable pour la largeur de l'ile
    [SerializeField] private int profondeurIle = 10; // variable pour la profondeur de l'ile
    [SerializeField] private Transform Ile; // variable pour le transform de l'ile
    [SerializeField] public GameObject _Perso;

    [SerializeField, Range(2f, 50f)] private float hauteurTerrain = 5f; // variable pour la hauteur du terrain
    public Renderer textureRenderer; // variable pour le textureRenderer du plane pour la ""minimap"" de l'ile

    [SerializeField, Range(2f, 50f)] private float detailterrain = 5f; // variable pour le detail du terrain

    [SerializeField, Range(2, 50)] float erosionIle = 10f; // variable pour l'erosion des côté de l'ile
    [SerializeField, Range(0, 1)] float centre = 0.5f; // variable pour le centre de la hauteur de l'eau

    private List<List<Material>> BiomesMats = new List<List<Material>>(); //initialiser la liste des biomes

    private List<GameObject> _TousLesCubes = new List<GameObject>(); //initialiser la liste des biomes
    public List<GameObject> tousLesCubes { get; set; }

    [SerializeField] private int _nbEnnemy = 50;
    [SerializeField] private int _nbPoungi = 3;


    [SerializeField] private Transform _UI;
    [SerializeField] private GameObject _UIJoueur;

    private int _nbCubesRestant;
    private int _nbCubesvoulu;

    [SerializeField] public ChangerScene _changerScene;

    private string _nomScene;
    private SOPerso _donneesPerso;

    // Start is called before the first frame update
    void Awake()
    {
        
        LoadResources(); // fonction pour remplir la liste de matériaux avec des liste de biomes variants
        float[,] unTerrain = TerraformerIle(largeurIle, profondeurIle);  // matrice pour le terrain
        float[,] unTerrainBiomes = TerraformerIle(largeurIle, profondeurIle);  // matrice pour le terrain
        unTerrain = AquaformerCerclulaire(unTerrain); // ajoute l'eau autour de l'ile (en cercle)
        AfficherIle(unTerrain, unTerrainBiomes); // fait apparaitre l'ile avec les cubes
        GameObject UIJoueur = Instantiate(_UIJoueur, new Vector3(0, 0, 0), Quaternion.identity, _UI); // placement de l'UI du joueur
        UIJoueur.GetComponent<UIManager>().tousLesCubes = _TousLesCubes;
        // Debug.Log(_Perso + "perso");
        UIJoueur.GetComponent<UIManager>().Perso = _Perso;
        _nbCubesRestant = _TousLesCubes.Count;
        // Debug.Log(_nbCubesRestant + " cubes restants");
        _nbCubesvoulu = _TousLesCubes.Count * 70 / 100;
        // Debug.Log(_nbCubesvoulu + " cubes voulus");
        Coroutine updateScore = StartCoroutine(CoroutUpdateScore());
        Coroutine vagueEnnemis = StartCoroutine(CoroutVagueEnnemie());
        _donneesPerso = _Perso.GetComponent<Perso>().donneePerso;

        _nomScene = _changerScene.DonnerNomScene();

        if (_nomScene == "niveau1" || _nomScene == "niveau2" || _nomScene == "niveau3" || _nomScene == "niveau4")
        {
            StopCoroutine(vagueEnnemis);
        }
        // Debug.Log(_nomScene);




    }

    private void LoadResources()
    {
        int nbBiomes = 1; //initialiser le nombre de biomes
        int nbVariant = 1; //initialiser le nombre de variantes
        bool resteDesMats = true;   //initialiser le booléen pour savoir s'il reste des matériaux à charger
        List<Material> tpBiome = new List<Material>(); //initialiser la liste des matériaux d'un biome (temporaire)
        // charger les matériaux
        do // fait ça tant qu'il reste des matériaux à charger
        {
            Object mats = Resources.Load("Biomes/b" + nbBiomes + "_v" + nbVariant); // va checher "l'URL" du matériaux
            if (mats)   // si le matériaux existe
            {
                tpBiome.Add((Material)mats);    //cast en matériel l'objet dans le tableau
                // Debug.Log(mats);
                nbVariant++; // check si il y a une autre variante
            }
            else // si le matériaux n'existe pas
            {
                if (nbVariant == 1) // si il n'y a pas d'autre variante
                {
                    resteDesMats = false; // il n'y a plus de matériaux à charger
                }
                else // si il y a d'autre variante
                {
                    BiomesMats.Add(tpBiome);  // ajouter la liste des matériaux du biome à la liste des biomes
                    tpBiome = new List<Material>(); // réinitialiser la liste des matériaux du biome
                    nbBiomes++; // check si il y a un autre biome
                    nbVariant = 1; // réinitialiser le nombre de variantes
                }
            }

        } while (resteDesMats); //tant qu'il reste des matériaux à charger
    }

    // fonction pour la création dew l'ile
    private float[,] TerraformerIle(int largeurIle, int profondeurIle)
    {
        float[,] terrain = new float[largeurIle, profondeurIle];    // création matrice pour le terrain
        float attenuation = detailterrain; // variable la transformation de detailterrain en atténuation
        int bruit = Random.Range(0, 10000);// variable pour le bruit

        for (int colonne = 0; colonne < largeurIle; colonne++)
        {
            //x                                         // double boucle pour les coordonees en x et z 
            for (int ligne = 0; ligne < profondeurIle; ligne++)
            {
                //z
                // terrain[x,z] = Random.Range(0f,1f);
                float val = Mathf.PerlinNoise((colonne / attenuation) + bruit, (ligne / attenuation) + bruit); // création du terrain avec le bruit et le perlin noise
                terrain[colonne, ligne] = Mathf.Clamp01(val); // variable pour la hauteur du terrain
            }
        }
        return terrain; // retourne le terrain
    }


    // fonction pour placer les cubes et les couleurs des biomes
    private void AfficherIle(float[,] terrain, float[,] unTerrainBiomes)
    {
        int largeur = terrain.GetLength(0); // variable pour la largeur de l'ile
        int profondeur = terrain.GetLength(1);  // variable pour la profondeur de l'ile
        Texture2D texture = new Texture2D(largeur, profondeur); // variable pour la texture de des cubes
        for (int colonne = 0; colonne < largeur; colonne++)
        {
            //x                                         // double boucle pour le placement des cubes x et z 
            for (int ligne = 0; ligne < profondeur; ligne++)
            {
                //z
                Color couleur = new Color(1, 1, 1) * terrain[colonne, ligne]; // couleur des cubes
                texture.SetPixel(colonne, ligne, couleur);    // placement des pixel pour la couleur des cubes
                InstancierBiome(colonne, terrain[colonne, ligne], ligne, unTerrainBiomes[colonne, ligne]); // placement des cubes selon les biomes(hauteur)
            }
        }
        _TousLesCubes = Shuffle(_TousLesCubes); // mélange les cubes
        for (int i = 0; i < _nbEnnemy; i++)
        {
            // Debug.Log(_nbEnnemy);
            // Debug.Log(Resources.Load("Ennemi/Winnie"));
            // Debug.Log(TousLesCubes[i]);

            GameObject Ennemi = Instantiate((GameObject)Resources.Load("Ennemi/Winnie"),
            new Vector3(_TousLesCubes[i].transform.position.x, _TousLesCubes[i].transform.position.y + 1, _TousLesCubes[i].transform.position.z), Quaternion.identity /*, Ile*/); // placement des cubes
            Ennemi.GetComponent<EnnemiEtatManager>().cible = _Perso;
            Ennemi.GetComponent<EnnemiEtatManager>().home = _TousLesCubes[i];
            Ennemi.GetComponent<EnnemiEtatManager>().TousLesCubes = _TousLesCubes;
        }
        for (int i = 0; i < _nbPoungi; i++)
        {
            // Debug.Log(_nbEnnemy);
            // Debug.Log(Resources.Load("Ennemi/Winnie"));
            // Debug.Log(TousLesCubes[i]);

            GameObject Poungi = Instantiate((GameObject)Resources.Load("Ami/Poungi"),
            new Vector3(_TousLesCubes[i].transform.position.x, _TousLesCubes[i].transform.position.y + 1, _TousLesCubes[i].transform.position.z), Quaternion.identity /*, Ile*/); // placement des cubes
            Poungi.GetComponent<PoungiEtatManager>().cible = _Perso;
            Poungi.GetComponent<PoungiEtatManager>().home = _TousLesCubes[i];
            Poungi.GetComponent<PoungiEtatManager>().TousLesCubes = _TousLesCubes;
        }
        texture.Apply(); //applique la couleur(pixel de couleur) à la texture
        textureRenderer.sharedMaterial.mainTexture = texture; // change la couleur des cubes selon la texture 
    }
    // fonction pour placer les cubes selon les biomes(hauteur)
    private void InstancierBiome(int x, float y, int z, float yVariants)
    {
        _nomScene = _changerScene.DonnerNomScene();
        float[] tpAngles = { 0, 90, 180, 270 }; // variable pour les angles de rotation des cubes
        if (y > 0)
        {
            GameObject unBiome = Instantiate(cubeBase, new Vector3(x, y * hauteurTerrain, z), Quaternion.identity, Ile);
            unBiome.GetComponent<BiomeEtatManager>().ile = Ile; // placement des cubes
            unBiome.transform.eulerAngles = new Vector3(0, tpAngles[Random.Range(0, tpAngles.Length)], 0); // rotation des cubes
            unBiome.GetComponent<BiomeEtatManager>()._donneePerso = _Perso.GetComponent<Perso>().donneePerso;

            // placement des cubes
            _TousLesCubes.Add(unBiome); // ajoute les cubes à la liste des cubes
            int quelBiome = Mathf.RoundToInt(y * (BiomesMats.Count - 1)); //variable qui détermine la couleur des cubes selon la hauteur
            int quelVariant = Mathf.RoundToInt(Mathf.Clamp01(yVariants * Random.Range(0, BiomesMats[quelBiome].Count))); //variable qui détermine la couleur des cubes selon la hauteur


            unBiome.GetComponent<BiomeEtatManager>().biomeMaterial = BiomesMats[quelBiome][quelVariant]; // change la couleur des cubes selon la hauteur
            if (unBiome.GetComponent<BiomeEtatManager>().biomeMaterial.name == "b2_v1")
            {
                int chanceArbre = Random.Range(0, 5);
                if (chanceArbre > 3)
                {

                    if (_nomScene == "niveau1")
                    {

                        unBiome.GetComponent<BiomeEtatManager>().aUnArbre = false;


                    }
                    else
                    {
                        unBiome.GetComponent<BiomeEtatManager>().aUnArbre = true;
                    }
                }

            }
            if (unBiome.GetComponent<BiomeEtatManager>().biomeMaterial.name == "b3_v1")
            {
                int chanceStag = Random.Range(0, 5);
                if (chanceStag > 2)
                {

                    unBiome.GetComponent<BiomeEtatManager>().aUnStag = true;
                }
            }
            if (unBiome.GetComponent<BiomeEtatManager>().biomeMaterial.name == "b4_v2")
            {
                int chanceFleur = Random.Range(0, 5);
                if (chanceFleur > 2)
                {

                    unBiome.GetComponent<BiomeEtatManager>().aUneFleur = true;
                }
            }
            if (unBiome.GetComponent<BiomeEtatManager>().biomeMaterial.name == "b4_v1" || unBiome.GetComponent<BiomeEtatManager>().biomeMaterial.name == "b5_v1")
            {
                int chanceOs = Random.Range(0, 5);
                if (chanceOs > 3)
                {
                    if (_nomScene == "niveau1" || _nomScene == "niveau2")
                    {
                        unBiome.GetComponent<BiomeEtatManager>().aUnOs = false;

                    }
                    else
                    {

                        unBiome.GetComponent<BiomeEtatManager>().aUnOs = true;
                    }
                }
            }
            if (unBiome.GetComponent<BiomeEtatManager>().biomeMaterial.name == "b4_v1")
            {
                int chanceCactus = Random.Range(0, 5);
                if (chanceCactus > 3)
                {

                    unBiome.GetComponent<BiomeEtatManager>().aUnCactus = true;
                }
            }


            // Debug.Log(unBiome.GetComponent<BiomeEtatManager>().biomeMaterial.name);

            unBiome.gameObject.AddComponent<BoxCollider>(); // ajoute un box collider au cube
            unBiome.GetComponent<BiomeEtatManager>().tousLesCubes = _TousLesCubes;
        }
    }

    //fonction sigmoid
    private float Sigmoid(float valeur)
    {
        return 1f / (1f + Mathf.Exp(-erosionIle * (valeur - centre))); // calcul pour le sigmoid
    }

    // fonction pour le placement de l'eau autour de l'ile(carre)
    private float[,] AquaformerIle(float[,] terrain)
    {
        int largeur = terrain.GetLength(0); // variable pour la largeur de l'ile
        int profondeur = terrain.GetLength(1); // variable pour la profondeur de l'ile
        for (int x = 0; x < largeur; x++)
        {
            //x                                     // double boucle pour le placement de l'eau x et z
            for (int z = 0; z < profondeur; z++)
            {
                //z
                float vx = Mathf.Abs(x / (float)largeur * 2 - 1); // variable pour le centre de l'ile selon la largeur
                float vz = Mathf.Abs(z / (float)profondeur * 2 - 1); // variable pour le centre de l'ile selon la profondeur
                float val = Mathf.Max(vx, vz); // variable pour le centre de l'ile selon la largeur et la profondeur
                val = Sigmoid(val); // variable pour la valeur qui a été calculé selon la fonction sigmoid (besoin de précision car pas sûr)
                terrain[x, z] = Mathf.Clamp01(terrain[x, z] - val); // nouvelle variable qui retire la valeur calculé par sigmoid de la matrice du terrain (besoin de précision car pas sûr)
            }
        }
        return terrain; // retourne le terrain
    }
    // fonction pour le placement de l'eau autour de l'ile(cercle)
    private float[,] AquaformerCerclulaire(float[,] terrain)
    {
        int largeur = terrain.GetLength(0); // variable pour la largeur de l'ile
        int profondeur = terrain.GetLength(1); // variable pour la profondeur de l'ile

        float centreLargeur = largeur / 2f; // variable pour le centre de l'ile selon la largeur 
        float centreProfondeur = profondeur / 2f; // variable pour le centre de l'ile selon la profondeur
        float rayon = centreLargeur; // variable pour le rayon de l'ile

        for (int x = 0; x < largeur; x++)
        {                                       // double boucle qui regarder toute les coordonnées x et z de l'ile
            for (int z = 0; z < profondeur; z++)
            {
                float a = Mathf.Abs(x - centreLargeur); // variable pour déterminer la distance entre le centre de l'ile et la coordonnée x
                float b = Mathf.Abs(z - centreProfondeur); // variable pour déterminer la distance entre le centre de l'ile et  la coordonnée z
                float c = (a * a) + (b * b); // pythagore
                float distanceAuCentre = Mathf.Sqrt(c); //racine carré de la valeur c (suite de pythagore)

                float val = Sigmoid(distanceAuCentre / rayon); // variable pour la valeur qui a été calculé selon la fonction sigmoid (besoin de précision car pas sûr)
                terrain[x, z] = Mathf.Clamp01(terrain[x, z] - val); // nouvelle variable qui retire la valeur calculé par sigmoid de la matrice du terrain (besoin de précision car pas sûr)
            }
        }
        return terrain; //// retourne le terrain
    }



    // Update is called once per frame (inutile mais garde au cas où)
    void Update()
    {

    }


    private List<T> Shuffle<T>(List<T> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            T temp = _list[i];
            int randomIndex = Random.Range(i, _list.Count);
            _list[i] = _list[randomIndex];
            _list[randomIndex] = temp;
        }



        return _list;
    }

    IEnumerator CoroutUpdateScore()
    {
        while (true)
        {

            yield return new WaitForSeconds(0.1f);
            _nbCubesRestant = _TousLesCubes.Count;
            Debug.Log(_donneesPerso.NbEnnemiTuer);
            // Debug.Log(_nbCubesRestant + " cubes restants");
            if (_nbCubesRestant <= _nbCubesvoulu)
            {
                if (_nomScene == "niveau1")
                {
                    _Perso.GetComponent<Perso>().changerScene.DeuxiemeNiveau();
                    yield break;
                }
                else if(_nomScene == "niveau2" && _donneesPerso.Bois >= 25){
                    _Perso.GetComponent<Perso>().changerScene.TroisiemeNiveau();
                    yield break;
                }
                else if(_nomScene == "niveau3" && _donneesPerso.NbBonhomme >=1)
                {
                    _Perso.GetComponent<Perso>().changerScene.QuatriemeNiveau();
                }
                else if(_nomScene == "niveau4" && _donneesPerso.NbEnnemiTuer >=10)
                {
                    _Perso.GetComponent<Perso>().changerScene.CinquiemeNiveau();
                }
                else if(_nomScene == "niveau5")
                {

                    Debug.Log("Partie gagnée");

                    _Perso.GetComponent<Perso>().changerScene.Fin();
                    StopCoroutine(CoroutVagueEnnemie());
                    yield break;
                }
            }
        }
    }

    IEnumerator CoroutVagueEnnemie()
    {

        while (true)
        {
            // Debug.Log("bonjourVague");
            yield return new WaitForSeconds(120f);
            for (int i = 0; i < 5; i++)
            {
                // Debug.Log("vagueEnnemis spawn");
                // Debug.Log(_nbEnnemy);
                // Debug.Log(Resources.Load("Ennemi/Winnie"));
                // Debug.Log(TousLesCubes[i]);

                GameObject Ennemi = Instantiate((GameObject)Resources.Load("Ennemi/Winnie"),
                new Vector3(_TousLesCubes[i].transform.position.x, _TousLesCubes[i].transform.position.y + 1, _TousLesCubes[i].transform.position.z), Quaternion.identity /*, Ile*/); // placement des cubes
                Ennemi.GetComponent<EnnemiEtatManager>().cible = _Perso;
                Ennemi.GetComponent<EnnemiEtatManager>().home = _TousLesCubes[i];
                Ennemi.GetComponent<EnnemiEtatManager>().TousLesCubes = _TousLesCubes;
            }
        }
    }

}

