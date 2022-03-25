using UnityEngine;
using UnityEngine.EventSystems;

public class Detonator : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject bombPrefab;

    private GameObject pig;
    public static GameObject bomb;//переменная созданной бомбы

    void Start()
    {
        pig = GameObject.Find("Pig");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        PutBomb();
    }
    public void PutBomb()
    {
        if (bomb == null)
        {
            bomb = Instantiate(bombPrefab);
            Debug.Log("bomb2 =: " + bomb);
            bomb.transform.position = new Vector2(pig.transform.position.x, pig.transform.position.y);
        }
    }
}
