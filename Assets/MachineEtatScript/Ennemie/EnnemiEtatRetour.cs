using UnityEngine;

public class EnnemiEtatRetour : EnnemiEtatBase
{
    
    public override void InitEtat(EnnemiEtatManager ennemi)
    {
        Debug.Log("Je suis dans l'état retour");
        Vector3 _home = ennemi.home.transform.position;
        ennemi.agent.SetDestination(_home);
    }
    public override void UpdateEtat(EnnemiEtatManager ennemi)
    {
        if (ennemi.agent.remainingDistance <= 1f)
        {
            ennemi.ChangerEtat(ennemi.repos);
        }
    }
    public override void TriggerEnter(EnnemiEtatManager ennemi , Collider other)
    {

    }
}
