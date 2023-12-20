using System.Collections;
using System.Data.Common;
using UnityEngine;

public class BiomeEtatSemable : BiomeEtatBase
{

    private Material _semable;
    private Material _transform;

    public override void InitEtat(BiomeEtatManager biomes)
    {
        if (biomes.aUnArbre)
        {
            Debug.Log("jte donne un bois");
            biomes._donneePerso.Bois++;
        }

        Debug.Log("semable");
        _transform = Resources.Load<Material>("Biomes/Transform");

        _semable = Resources.Load<Material>("Biomes/Semable");
        // biomes.peutChangerEtat = false;
        Coroutine coroutineChangerEtat = biomes.StartCoroutine(CoroutChangerEtat(biomes));
        // Debug.Log("hello je suis initialFonte");
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
        biomes.GetComponent<Renderer>().material = _transform;
        yield return new WaitForSeconds(1f);
        biomes.GetComponent<Renderer>().material = _semable;
        yield return new WaitForSeconds(1f);





        yield return new WaitForSeconds(10f);
        if (biomes.aUnArbre || biomes.aUneFleur)
        {

            biomes.peutChangerEtat = true;

        }

        biomes.StopCoroutine(CoroutChangerEtat(biomes));

    }
    public override void UpdateEtat(BiomeEtatManager biomes)
    {

    }
    public override void TriggerEnterEtat(BiomeEtatManager biomes, Collider other)
    {
        Debug.Log("trigger etat semable &&" + biomes.peutChangerEtat + " ");



        if (other.CompareTag("Ennemis") && biomes.peutChangerEtat == true)
        {
            biomes.peutChangerEtat = false;
            // Debug.Log("allo");
            biomes.ChangerEtat(biomes.fonte);

        }
        else if (other.CompareTag("Player") && biomes.peutChangerEtat == true)
        {


            biomes.peutChangerEtat = false;
            biomes.ChangerEtat(biomes.cultivable);


        }
    }
}
