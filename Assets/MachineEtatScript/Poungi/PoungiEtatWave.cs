using UnityEngine;

public class PoungiEtatWave : PoungiEtatBase
{
    
    public override void InitEtat(PoungiEtatManager poungi)
    {
        Debug.Log("Je suis dans l'état retour");
    }
    public override void UpdateEtat(PoungiEtatManager poungi)
    {
        float distanceJoueur = Vector3.Distance(poungi.cible.transform.position, poungi.agent.transform.position);
        if (distanceJoueur < poungi.range)
        {
            poungi.animator.SetBool("IsWaving", true);
            poungi.animator.SetBool("IsIdling", false);
        }
        else{
            poungi.ChangerEtat(poungi.repos);
        }
    }
    public override void TriggerEnter(PoungiEtatManager poungi , Collider other)
    {

    }
}
