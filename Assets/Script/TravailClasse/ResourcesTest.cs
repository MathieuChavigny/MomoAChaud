using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesTest : MonoBehaviour
{

    private List<List<Material>> biomesMats = new List<List<Material>>(); //initialiser la liste des biomes
    private Object mats;




    // Start is called before the first frame update
    void Start()
    {
        LoadResource();
    }


    private void LoadResource()
    {
        int nbBiomes = 1; //initialiser le nombre de biomes
        int nbVariant = 1; //initialiser le nombre de variantes
        bool resteDesMats = true;   //initialiser le booléen pour savoir s'il reste des matériaux à charger
        List<Material> tpBiome = new List<Material>(); //initialiser la liste des matériaux d'un biome (temporaire)
        // charger les matériaux
        do // fait ça tant qu'il reste des matériaux à charger
        {
            mats = Resources.Load("Biomes/b" + nbBiomes + "_v" + nbVariant); // va checher "l'URL" du matériaux
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
                    biomesMats.Add(tpBiome);  // ajouter la liste des matériaux du biome à la liste des biomes
                    tpBiome = new List<Material>(); // réinitialiser la liste des matériaux du biome
                    nbBiomes++; // check si il y a un autre biome
                    nbVariant = 1; // réinitialiser le nombre de variantes
                }
            }

        } while (resteDesMats); //tant qu'il reste des matériaux à charger
    }
}
