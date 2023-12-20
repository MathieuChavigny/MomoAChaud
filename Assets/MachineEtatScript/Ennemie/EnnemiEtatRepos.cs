using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnnemiEtatRepos : EnnemiEtatBase
{
    private Vector3 _goal;
    private Transform _home;

    Coroutine routine;

    private bool _joueurDetecte = false;
    public override void InitEtat(EnnemiEtatManager ennemi)
    {
        // Debug.Log("Je suis dans l'état repos");
        _home = ennemi.home.transform;
        routine = ennemi.StartCoroutine(CoroutRoutineRepos(ennemi));
    }

    IEnumerator CoroutRoutineRepos(EnnemiEtatManager ennemi)
    {
        while (true)
        {
            // Debug.Log("Start Coroutine");
            int i = Random.Range(0, ennemi.TousLesCubes.Count);
            _goal = ennemi.TousLesCubes[i].transform.position;
            // Debug.Log(_goal);
            ennemi.agent.SetDestination(_goal);



            float distanceJoueur = Vector3.Distance(ennemi.cible.transform.position, ennemi.agent.transform.position);
            while (ennemi.agent.remainingDistance > 1f || ennemi.agent.pathPending)
            {
                ennemi.agent.SetDestination(_goal);
                distanceJoueur = Vector3.Distance(ennemi.cible.transform.position, ennemi.agent.transform.position);
                if (distanceJoueur < ennemi.range)
                {
                    // Debug.Log("Momo detecte");
                    ennemi.ChangerEtat(ennemi.chasse);
                    yield break;
                }
                yield return new WaitForSeconds(0.1f);
            }


            // Debug.Log("ARRETE TOI");
            yield return new WaitForSeconds(3f);


        }
    }

    public override void UpdateEtat(EnnemiEtatManager ennemi)
    {


    }


    public override void TriggerEnter(EnnemiEtatManager ennemi, Collider other)
    {
        
    }






}
