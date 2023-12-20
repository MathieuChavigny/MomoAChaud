using System.Collections;
using UnityEngine;

public class BiomeEtatActivable : BiomeEtatBase
{
    GameObject Arbre;
    GameObject Fleur;
    GameObject Os;
    GameObject Cactus;
    int _nbOs=1;

    public override void InitEtat(BiomeEtatManager biomes)
    {

        biomes.GetComponent<Renderer>().material = biomes.biomeMaterial;

        if (biomes.aUnArbre == true)
        {

            // int chanceArbre = Random.Range(0, 5);
            GameObject arbre;
            arbre = Resources.Load<GameObject>("Item/Tree");



            Arbre = GameObject.Instantiate(arbre, biomes.transform.position + new Vector3(0, 1, 0), Quaternion.identity, biomes.ile);
            Arbre.transform.localScale = new Vector3(Random.Range(0.4f, 0.6f), Random.Range(0.65f, 0.8f), Random.Range(0.4f, 0.6f));
            Arbre.transform.position = new Vector3(Arbre.transform.position.x, Arbre.transform.position.y - 1f, Arbre.transform.position.z);

            float rotationRng = Random.Range(0f, 360f);

            Arbre.transform.localRotation = Quaternion.Euler(Arbre.transform.localRotation.x, Arbre.transform.localRotation.y + rotationRng, Arbre.transform.localRotation.z);


        }
        if (biomes.aUneFleur)
        {
            GameObject fleur;
            fleur = Resources.Load<GameObject>("Item/FleurLave");


            Fleur = GameObject.Instantiate(fleur, biomes.transform.position + Vector3.up, Quaternion.identity, biomes.ile);
            Fleur.transform.localScale = new Vector3(Random.Range(.5f, .7f), Random.Range(.5f, .7f), Random.Range(.5f, .7f));
            Fleur.transform.position = new Vector3(Fleur.transform.position.x, Fleur.transform.position.y - 0.5f, Fleur.transform.position.z);
            float rotationRng = Random.Range(0f, 360f);
            Fleur.transform.localRotation = Quaternion.Euler(Fleur.transform.localRotation.x, Fleur.transform.localRotation.y + rotationRng, Fleur.transform.localRotation.z);

        }
        if (biomes.aUnOs)
        {
             
            
            GameObject os;
            if (_nbOs == 1)
            {
                _nbOs++;
                os = Resources.Load<GameObject>("Item/Os1");
                Os = GameObject.Instantiate(os, biomes.transform.position + Vector3.up, Quaternion.identity, biomes.ile);
                Os.transform.localScale = new Vector3(Random.Range(.125f, .175f), Random.Range(.125f, .175f), Random.Range(.125f, .175f));
                Os.transform.position = new Vector3(Os.transform.position.x, Os.transform.position.y - 0.5f, Os.transform.position.z);
                float rotationRng = Random.Range(0f, 360f);
                Os.transform.localRotation = Quaternion.Euler(Os.transform.localRotation.x, Os.transform.localRotation.y + rotationRng, Os.transform.localRotation.z);
            }
        }
        if (biomes.aUnCactus)
        {

            GameObject cactus;
            cactus = Resources.Load<GameObject>("Item/cactus");
            Cactus = GameObject.Instantiate(cactus, biomes.transform.position + Vector3.up, Quaternion.identity, biomes.ile);
            Cactus.transform.localScale = new Vector3(Random.Range(.225f, .275f), Random.Range(.225f, .275f), Random.Range(.225f, .275f));
            Cactus.transform.position = new Vector3(Cactus.transform.position.x, Cactus.transform.position.y - 0.5f, Cactus.transform.position.z);
            float rotationRng = Random.Range(0f, 360f);
            Cactus.transform.localRotation = Quaternion.Euler(Cactus.transform.localRotation.x, Cactus.transform.localRotation.y + rotationRng, Cactus.transform.localRotation.z);
        }

    }

    private IEnumerator CoroutArbreMeurt(BiomeEtatManager biomes)
    {
        // Arbre.GetComponent<Transform>().localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        // yield return new WaitForSeconds(0.2f);
        // Arbre.GetComponent<Transform>().localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        // // GameObject.Destroy(Arbre);
        Vector3 startScale = Arbre.transform.localScale;
        Vector3 startPosition = Arbre.transform.position;
        Debug.Log(startScale);
        Vector3 targetScale = new Vector3(0.0f, 0.0f, 0.0f); // Shrink only on the Y-axis
        Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y - 2.0f, startPosition.z); // Shrink only on the Y-axis
        float duration = 4.0f; // Shrink for 2 seconds

        float startTime = Time.time;



        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            Arbre.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            Arbre.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null; // Wait for the next frame
        }

        // Ensure that the final scale is exactly the target scale
        Arbre.transform.localScale = targetScale;

        DestroyMoiCa(Arbre);
        yield break;
        // Destroy the GameObject after shrinking



    }
    private IEnumerator CoroutCactusMeurt(BiomeEtatManager biomes)
    {

        Vector3 startScale = Cactus.transform.localScale;
        Vector3 startPosition = Cactus.transform.position;
        Debug.Log(startScale);
        Vector3 targetScale = new Vector3(0.0f, 0.0f, 0.0f); // Shrink only on the Y-axis
        Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y - 2.0f, startPosition.z); // Shrink only on the Y-axis
        float duration = 4.0f; // Shrink for 2 seconds

        float startTime = Time.time;



        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            Cactus.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            Cactus.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null; // Wait for the next frame
        }

        // Ensure that the final scale is exactly the target scale
        Cactus.transform.localScale = targetScale;

        DestroyMoiCa(Cactus);
        yield break;
        // Destroy the GameObject after shrinking



    }
    private IEnumerator CoroutFleurMeurt(BiomeEtatManager biomes)
    {
        // Arbre.GetComponent<Transform>().localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        // yield return new WaitForSeconds(0.2f);
        // Arbre.GetComponent<Transform>().localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        // // GameObject.Destroy(Arbre);
        Vector3 startScale = Fleur.transform.localScale;
        Vector3 startPosition = Fleur.transform.position;
        Debug.Log(startScale);
        Vector3 targetScale = new Vector3(0.0f, 0.0f, 0.0f); // Shrink only on the Y-axis
        Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y - 2.0f, startPosition.z); // Shrink only on the Y-axis
        float duration = 4.0f; // Shrink for 2 seconds

        float startTime = Time.time;



        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            Fleur.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            Fleur.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null; // Wait for the next frame
        }

        // Ensure that the final scale is exactly the target scale
        Fleur.transform.localScale = targetScale;

        DestroyMoiCa(Fleur);
        yield break;
        // Destroy the GameObject after shrinking



    }


    private void DestroyMoiCa(GameObject objet)
    {
        GameObject.Destroy(objet);
    }

    public override void UpdateEtat(BiomeEtatManager biomes)
    {

    }

    public override void TriggerEnterEtat(BiomeEtatManager biomes, Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Boules"))
        {
            if (biomes.aUnArbre)
            {
                biomes.StartCoroutine(CoroutArbreMeurt(biomes));
                biomes._donneePerso.Bois++;

            }
            if (biomes.aUneFleur)
            {
                biomes.StartCoroutine(CoroutFleurMeurt(biomes));

            }
            if (biomes.aUnCactus)
            {
                biomes.StartCoroutine(CoroutCactusMeurt(biomes));
            }

            biomes._donneePerso.BouleDeNeige++;
            // GameObject.Destroy(Arbre);
            biomes.ChangerEtat(biomes.cultivable);
            biomes.tousLesCubes.Remove(biomes.gameObject);
        }


    }
}
