using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BlastParticle : MonoBehaviour
{
    [SerializeField] private GameObject[] particles;

    public IObjectPool<BlastParticle> pool;

    private Action<BlastParticle> _releaseAction;

    private void OnEnable() =>
        StartCoroutine(DisableIE());

    private IEnumerator DisableIE()
    {
        yield return new WaitForSeconds(0.8f);
        _releaseAction(this);
    }

    public void Init(Action<BlastParticle> releaseAction) =>
        _releaseAction = releaseAction;

    public void SetParticle(int blastNumber, Vector3 position, Vector3 scale)
    {
        int maxNumber = particles.Length - 1;
        blastNumber = (blastNumber > maxNumber) ? maxNumber : blastNumber;

        for (int i = 0; i < particles.Length; i++)
            particles[i].SetActive(false);

        particles[blastNumber].SetActive(true);
        transform.position = position;
        particles[blastNumber].transform.localScale = new Vector3(scale.x, scale.y, scale.y);
    }
}
