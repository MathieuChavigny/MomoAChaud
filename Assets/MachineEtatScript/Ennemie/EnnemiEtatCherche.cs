using System.Collections;
using UnityEngine;

public class EnnemiEtatCherche : EnnemiEtatBase
{

    private Vector3 _player;
    float distanceJoueur;

    public override void InitEtat(EnnemiEtatManager ennemi)
    {
        ennemi.mainWinnieCollider.enabled = false;
        // Debug.Log("Je suis dans l'état cherche");
        ennemi.animator.SetTrigger("IsSearching");
        ennemi.agent.isStopped = true;
        Coroutine RoutineCherche = ennemi.StartCoroutine(ChercheJoueur(ennemi));
    }
    public override void UpdateEtat(EnnemiEtatManager ennemi)
    {
        _player = ennemi.cible.transform.position;
        distanceJoueur = Vector3.Distance(ennemi.cible.transform.position, ennemi.agent.transform.position);
    }



    public override void TriggerEnter(EnnemiEtatManager ennemi, Collider other)
    {

    }




    IEnumerator ChercheJoueur(EnnemiEtatManager ennemi)
    {
        yield return new WaitForSeconds(5f);
        // Debug.Log("Cherche joueur");
        if (distanceJoueur < ennemi.range / 2)
        {
            // Debug.Log("Momo detecte");
            ennemi.agent.isStopped = false;
            ennemi.animator.SetBool("IsIdling", true);
            ennemi.ChangerEtat(ennemi.chasse);
            yield break;
        }
        else
        {
            ennemi.agent.isStopped = false;
            ennemi.animator.SetBool("IsIdling", true);
            ennemi.ChangerEtat(ennemi.repos);
            yield break;
        }
    }


}
