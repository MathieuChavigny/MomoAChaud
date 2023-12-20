using System.Collections;

using UnityEngine;

public class BiomeEtatFonte : BiomeEtatBase
{

    private Material _fonte;

    GameObject Arbre;
    GameObject Fleur;
    public override void InitEtat(BiomeEtatManager biomes)
    {
        biomes.tousLesCubes.Add(biomes.gameObject);
        // Debug.Log("je suis fonte");
        _fonte = Resources.Load<Material>("Biomes/Fonte");
        // Coroutine coroutineArbre = biomes.StartCoroutine(CoroutArbreMeurt(biomes));
        Coroutine coroutineChangerEtat = biomes.StartCoroutine(CoroutChangerEtat(biomes));
        biomes.GetComponent<Renderer>().material = _fonte;
        
    }

    private IEnumerator CoroutChangerEtat(BiomeEtatManager biomes)
    {

        // initialisation de la variable de temps
        float timer = 0;
        //attend 2 secondes
        yield return new WaitForSeconds(2f);

        // tant que le temps est inférieur à 2 secondes
        while (timer < 2)
        {
            // augmenter le temps selon le temps passé
            timer += Time.deltaTime;
            // faire tourner le cube sur lui même en diagonale
            biomes.transform.Rotate(Vector3.one, 5);
            yield return null;
        }
        biomes.transform.rotation = Quaternion.identity;
        
        
        
        yield return new WaitForSeconds(1f);





        yield return new WaitForSeconds(10f);
        if (biomes.aUnArbre)
        {

            biomes.peutChangerEtat = true;

        }

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
        // Debug.Log(startScale);
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
    
    private void DestroyMoiCa(GameObject objet)
    {
        GameObject.Destroy(objet);
    }

    
    public override void UpdateEtat(BiomeEtatManager biomes)
    {

    }
    public override void TriggerEnterEtat(BiomeEtatManager biomes , Collider other )
    {
        if (other.CompareTag("Ennemis"))
        {
            // Debug.Log("allo");
            

        }
        else if (other.CompareTag("Player")){
            biomes.ChangerEtat(biomes.activable);
        }
    }
}
