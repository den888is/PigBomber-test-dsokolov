using UnityEngine;

public class MooveArrows : MonoBehaviour
{
    private GameObject pigO;
    private Pig pig;
    void Start()
    {
        pigO = GameObject.Find("Pig");
        pig = pigO.GetComponent<Pig>();
    }
    public void Go(Direction d)
    {
        switch (d)
        {
            case Direction.TOP: { pig.MoveTop(); break; }
            case Direction.RIGHT: { pig.MoveRight(); break; }
            case Direction.BOTTOM: { pig.MoveBottom(); break; }
            case Direction.LEFT: { pig.MoveLeft(); break; }
        }
    }
}
