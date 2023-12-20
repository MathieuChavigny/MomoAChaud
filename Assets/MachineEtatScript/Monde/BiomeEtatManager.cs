using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BiomeEtatManager : MonoBehaviour
{
    private BiomeEtatBase etatActuel;
    public BiomeEtatActivable activable = new BiomeEtatActivable();
    public BiomeEtatCultivable cultivable = new BiomeEtatCultivable();
    public BiomeEtatSemable semable = new BiomeEtatSemable();

    public BiomeEtatFonte fonte = new BiomeEtatFonte();

    public Transform ile { get; set; }
    public Material biomeMaterial { get; set; }
    public bool aUnArbre { get; set; }
    public bool aUnStag { get; set; }
    public bool aUneFleur { get; set; }
    public bool aUnOs { get; set; }
    public bool aUnCactus { get; set; }

    public bool peutChangerEtat { get; set; }


    private int _nbCubesRestant;
    private int _nbCubesvoulu;
    public List<GameObject> tousLesCubes { get; set; }
    public SOPerso _donneePerso { get; set; }




    // Start is called before the first frame update
    void Start()
    {
        ChangerEtat(activable);

        _nbCubesRestant = tousLesCubes.Count;
        _nbCubesvoulu = tousLesCubes.Count * 70 / 100;
        // Debug.Log(_nbCubesvoulu + " cubes voulus");
        // Debug.Log(_nbCubesRestant + " cubes restants");

        // Coroutine updateScore = StartCoroutine(CoroutUpdateScore());

    }

    public void ChangerEtat(BiomeEtatBase etat)
    {
        etatActuel = etat;
        etatActuel.InitEtat(this);


    }
    // 
    // Update is called once per frame
    void Update()
    {

        // etatActuel.UpdateEtat(this);
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        etatActuel.TriggerEnterEtat(this, other);
    }
}

//     IEnumerator CoroutUpdateScore()
//     {
//         while (true)
//         {

//             yield return new WaitForSeconds(0.1f);
//             _nbCubesRestant = tousLesCubes.Count;
//             // Debug.Log(_nbCubesRestant + " cubes restants");
//             if (_nbCubesRestant <= _nbCubesvoulu)
//             {
//                 Debug.Log("Partie gagnée");
//             }
//         }
//     }
// }
