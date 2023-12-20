using UnityEngine;

public abstract class PoungiEtatBase
{
    // abstact = ne peut pas être instancié
    public abstract void InitEtat(PoungiEtatManager poungi);
    public abstract void UpdateEtat(PoungiEtatManager poungi);
    public abstract void TriggerEnter(PoungiEtatManager poungi , Collider other);
}
