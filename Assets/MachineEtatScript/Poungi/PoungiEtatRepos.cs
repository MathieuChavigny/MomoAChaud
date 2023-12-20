using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PoungiEtatRepos : PoungiEtatBase
{
    private Vector3 _goal;
    private Transform _home;

    Coroutine routine;

    public override void InitEtat(PoungiEtatManager poungi)
    {
        Debug.Log("Je suis dans l'état repos");
        _home = poungi.home.transform;
        // routine = poungi.StartCoroutine(CoroutRoutineRepos(poungi));
    }

    IEnumerator CoroutRoutineRepos(PoungiEtatManager poungi)
    {
        while (true)
        {
            Debug.Log("Start Coroutine");
            int i = Random.Range(0, poungi.TousLesCubes.Count);
            _goal = poungi.TousLesCubes[i].transform.position;
            // Debug.Log(_goal);
            poungi.agent.SetDestination(_goal);



            float distanceJoueur = Vector3.Distance(poungi.cible.transform.position, poungi.agent.transform.position);
            while (poungi.agent.remainingDistance > 1f || poungi.agent.pathPending)
            {
                if (distanceJoueur < poungi.range)
                {
                    Debug.Log("Joueur détecté");
                    poungi.ChangerEtat(poungi.wave  );
                    yield break;
                }
                
                yield return null;
            }


            Debug.Log("ARRETE TOI");
            yield return new WaitForSeconds(3f);


        }
    }

    public override void UpdateEtat(PoungiEtatManager poungi)
    {


    }


    public override void TriggerEnter(PoungiEtatManager poungi, Collider other)
    {
        
    }






}
