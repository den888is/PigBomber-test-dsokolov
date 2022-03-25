using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private const float EXP_TIME = 0.3f;
    void Start()
    {
        StartCoroutine(DestroyExplosion());
    }
    private IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(EXP_TIME);
        Destroy(this.gameObject);//разрушение
        yield break;
    }
}
