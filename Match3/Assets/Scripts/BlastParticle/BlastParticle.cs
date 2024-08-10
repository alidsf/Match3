using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BlastParticle : PooledObject<BlastParticle>
{
    [SerializeField] private GameObject[] particles;

    private void OnEnable() =>
        StartCoroutine(DisableIE());

    private IEnumerator DisableIE()
    {
        yield return new WaitForSeconds(0.8f);
        _release(this);
    }

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
