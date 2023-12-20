using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnnemiEtatChasse : EnnemiEtatBase
{
    private Vector3 _goal;
    private bool _isAttacking = false;
    private bool _aDistance = false;
    public override void InitEtat(EnnemiEtatManager ennemi)
    {
        // Debug.Log("Je suis dans l'état chasse");
        Coroutine updatePosJoueur = ennemi.StartCoroutine(CoroutUpdatePosJoueur(ennemi));

    }


    public override void UpdateEtat(EnnemiEtatManager ennemi)
    {
        ennemi.agent.SetDestination(_goal);

    }

    public override void TriggerEnter(EnnemiEtatManager ennemi, Collider other)
    {

    }



    IEnumerator CoroutUpdatePosJoueur(EnnemiEtatManager ennemi)
    {
        while (true)
        {
            Transform _player = ennemi.cible.transform;
            _goal = _player.position;
            ennemi.agent.SetDestination(_goal);
            if (ennemi.agent.remainingDistance <= 1f && _isAttacking == false)
            {
                ennemi.animator.SetTrigger("IsAttacking");
                _isAttacking = true;
                ennemi.agent.isStopped = true;
                ennemi.agent.ResetPath();
                yield return new WaitForSeconds(1.5f);
                ennemi.mainWinnieCollider.enabled = true;
                yield return new WaitForSeconds(2.5f);
                ennemi.ChangerEtat(ennemi.cherche);
                _isAttacking = false;
                yield break;
            }
            // Debug.Log( "Position joueur : "+ _goal);
            yield return new WaitForSeconds(0.1f);

        }
    }
}
