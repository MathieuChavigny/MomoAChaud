using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BiomeEtatCultivable : BiomeEtatBase
{

    GameObject Arbre;
    GameObject Fleur;
    GameObject Cactus;
    private Material _Cultiver;
    private Material _transform;

    private BiomeEtatManager _biomes;
    public override void InitEtat(BiomeEtatManager biomes)
    {
        _biomes = biomes;
        _Cultiver = Resources.Load<Material>("Biomes/Cultive");
        _transform = Resources.Load<Material>("Biomes/Transform");


        Coroutine coroutineChangerEtat = biomes.StartCoroutine(CoroutChangerEtat(biomes));


        // Debug.Log("hello je suis cultivable");
    }





    private IEnumerator CoroutChangerEtat(BiomeEtatManager biomes)
    {

        // initialisation de la variable de temps
        float timer = 0;
        //attend 2 secondes
        yield return new WaitForSeconds(0.5f);

        // tant que le temps est inférieur à 2 secondes
        while (timer < 2)
        {
            // augmenter le temps selon le temps passé
            timer += Time.deltaTime;
            // faire tourner le cube sur lui même en diagonale
            biomes.transform.Rotate(Vector3.one, 8);
            yield return null;
        }
        biomes.transform.rotation = Quaternion.identity;
        biomes.GetComponent<Renderer>().material = _transform;
        yield return new WaitForSeconds(1f);
        biomes.GetComponent<Renderer>().material = _Cultiver;
        yield return new WaitForSeconds(1f);

        if (biomes.aUnArbre == true)
        {
            float startTime = Time.time;
            Vector3 targetScale = new Vector3(Random.Range(0.4f, 0.6f), Random.Range(0.8f, 1f), Random.Range(0.4f, 0.6f)); // Shrink only on the Y-axis
            float duration = 4.0f; // Shrink for 2 seconds
            Vector3 startScale = new Vector3(0f, 0f, 0f);
            // Debug.Log("test arbre");
            GameObject arbre;
            arbre = Resources.Load<GameObject>("Item/TreeBlanc");
            Arbre = GameObject.Instantiate(arbre, biomes.transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
            float rotationRng = Random.Range(0f, 360f);
            
            Arbre.transform.localRotation = Quaternion.Euler(Arbre.transform.localRotation.x, Arbre.transform.localRotation.y + rotationRng, Arbre.transform.localRotation.z);

            Arbre.transform.localScale = startScale;
            while (Time.time - startTime < duration)
            {
                float t = (Time.time - startTime) / duration;
                Arbre.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
                // Arbre.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                yield return null; // Wait for the next frame
            }


        }
        if (biomes.aUneFleur)
        {
            float startTime = Time.time;
            Vector3 targetScale = new Vector3(Random.Range(0.4f, 0.6f), Random.Range(0.4f, 0.6f), Random.Range(0.4f, 0.6f)); // Shrink only on the Y-axis
            float duration = 4.0f; // Shrink for 2 seconds
            Vector3 startScale = new Vector3(0f, 0f, 0f);
            GameObject fleur;
            fleur = Resources.Load<GameObject>("Item/FleurGlace");
            Fleur = GameObject.Instantiate(fleur, biomes.transform.position + new Vector3(0f,0.5f,0f), Quaternion.identity);
            float rotationRng = Random.Range(0f, 360f);
            Fleur.transform.localRotation = Quaternion.Euler(Fleur.transform.localRotation.x, Fleur.transform.localRotation.y + rotationRng, Fleur.transform.localRotation.z);
            
            Fleur.transform.localScale = startScale;
            while (Time.time - startTime < duration)
            {
                float t = (Time.time - startTime) / duration;
                Fleur.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
                // Arbre.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                yield return null; // Wait for the next frame
            }

        }
        if (biomes.aUnStag)
        {

            GameObject stalagmite;
            stalagmite = Resources.Load<GameObject>("Item/Stalactmite");
            int chanceStag = Random.Range(0, 5);
            if (chanceStag > 2)
            {


                GameObject Stag = GameObject.Instantiate(stalagmite, biomes.transform.position + Vector3.up, Quaternion.identity, biomes.ile);
                Stag.transform.localScale = new Vector3(.5f, .5f, .5f);
                Stag.transform.position = new Vector3(Stag.transform.position.x, Stag.transform.position.y - Random.Range(1f, 2f), Stag.transform.position.z);
                float rotationRng = Random.Range(0f, 360f);
                Stag.transform.localRotation = Quaternion.Euler(Stag.transform.localRotation.x, Stag.transform.localRotation.y + rotationRng, Stag.transform.localRotation.z);

            }

        }
        if (biomes.aUnCactus)
        {
            
        }

        yield return new WaitForSeconds(10f);



        biomes.peutChangerEtat = true;


        biomes.StopCoroutine(CoroutChangerEtat(biomes));

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

        // Debug.Log("you need this " + Arbre.transform.localScale);

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            Arbre.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            Arbre.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null; // Wait for the next frame
        }

        // Ensure that the final scale is exactly the target scale
        Arbre.transform.localScale = targetScale;

        // Destroy the GameObject after shrinking
        DestroyMoiCa(Arbre);
        yield break;
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
        
        if (biomes.peutChangerEtat == true)
        {
            
            // biomes.peutChangerEtat = false;
            if (biomes.aUnArbre == true || biomes.aUneFleur == true)
            {
                // Debug.Log("trigger etat cultivable step 3");
                

                
                if (other.CompareTag("Ennemis") && biomes.peutChangerEtat == true)
                {
                    biomes.peutChangerEtat = false;
                    if (biomes.aUnArbre == true)
                    {
                        biomes.StartCoroutine(CoroutArbreMeurt(biomes));
                    }
                    else if (biomes.aUneFleur == true)
                    {
                        biomes.StartCoroutine(CoroutFleurMeurt(biomes));
                    }
                    
            
                    

            
                    // Debug.Log("ennemie touche cultivable pour faire fondre");
                    biomes.ChangerEtat(biomes.fonte);

                }
                else if (other.CompareTag("Player")  && biomes.peutChangerEtat == true)
                {
                    biomes.peutChangerEtat = false;
                    if (biomes.aUnArbre == true)
                    {
                        biomes.StartCoroutine(CoroutArbreMeurt(biomes));
                    }
                    
                    
                    // Debug.Log("trigger etat cultivable step 4");
                    // Debug.Log("bye bye");
                    if (biomes.aUneFleur == true)
                    {
                        
                    }
                    else{

                    biomes.ChangerEtat(biomes.semable);
                    }
                }
            } 
            if(other.CompareTag("Ennemis") && biomes.peutChangerEtat == true && biomes.aUnArbre == false)
            {
                biomes.ChangerEtat(biomes.fonte);
            }
            
            




        }


    }
}
