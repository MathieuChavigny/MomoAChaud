using UnityEngine;

public abstract class EnnemiEtatBase
{
    // abstact = ne peut pas être instancié
    public abstract void InitEtat(EnnemiEtatManager ennemi);
    public abstract void UpdateEtat(EnnemiEtatManager ennemi);
    public abstract void TriggerEnter(EnnemiEtatManager ennemi , Collider other);
}
