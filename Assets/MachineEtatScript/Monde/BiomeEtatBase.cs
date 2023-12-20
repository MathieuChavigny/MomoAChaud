using UnityEngine;

public abstract class BiomeEtatBase
{
    // abstact = ne peut pas être instancié
    public abstract void InitEtat(BiomeEtatManager biomes);
    public abstract void UpdateEtat(BiomeEtatManager biomes);
    public abstract void TriggerEnterEtat(BiomeEtatManager biomes , Collider other);
}
