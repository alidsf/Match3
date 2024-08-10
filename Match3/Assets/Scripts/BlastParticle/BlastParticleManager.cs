using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastParticleManager : Singleton<BlastParticleManager>
{
    [SerializeField] private BlastParticle blastParticlePrefab;

    private ObjectPoolBase<BlastParticle> _pool = new ObjectPoolBase<BlastParticle>();

    private void Start()
    {
        _pool.Init(blastParticlePrefab, transform, Instantiate, Destroy);
    }

    public void Blast(int blastNumber, Vector2 position, Vector2 scale)
    {
        BlastParticle blastParticle = _pool.GetNewObject();
        blastParticle.SetParticle(blastNumber, position, scale);
    }

}
