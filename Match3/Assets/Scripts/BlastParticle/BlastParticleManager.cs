using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BlastParticleManager : Singleton<BlastParticleManager>
{
    [SerializeField] private BlastParticle blastParticlePrefab;

    private IObjectPool<BlastParticle> _pool;
    private IObjectPool<BlastParticle> pool
    {
        get
        {
            if (_pool == null)
                _pool = new ObjectPool<BlastParticle>(OnCreateObject, OnGetObject,
                    OnReleaseObject, OnDestroyObject);

            return _pool;
        }
    }

    private BlastParticle OnCreateObject()
    {
        BlastParticle blastParticle = Instantiate(blastParticlePrefab, transform);
        blastParticle.Init(pool.Release);
        return blastParticle;
    }

    private void OnGetObject(BlastParticle blastParticle) =>
        blastParticle.gameObject.SetActive(true);

    private void OnReleaseObject(BlastParticle blastParticle) =>
        blastParticle.gameObject.SetActive(false);

    private void OnDestroyObject(BlastParticle blastParticle) =>
        Destroy(blastParticle.gameObject);

    public void Blast(int blastNumber, Vector2 position, Vector2 scale)
    {
        BlastParticle blastParticle = pool.Get();
        blastParticle.SetParticle(blastNumber, position, scale);
    }

}
