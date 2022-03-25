using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    private GameObject explosion;
    private const float FIRE_TIME = 1.5f;//время горения бомбы, радиус поражения
    private const float FIRE_RAD = 3f;
    void Start()
    {
        StartCoroutine(Boom());
    }
    private IEnumerator Boom()
    {
        yield return new WaitForSeconds(FIRE_TIME);
        Debug.Log("Boom");
        explosion = Instantiate(explosionPrefab);
        explosion.transform.position = this.transform.position;
        gameObject.AddComponent<CircleCollider2D>();
        GetComponent<CircleCollider2D>().radius = FIRE_RAD;
        Detonator.bomb = null;
        yield return new WaitForSeconds(.1f);

        Destroy(this.gameObject);//разрушение
        yield break;
    }
}
