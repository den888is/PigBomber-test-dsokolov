using System.Collections;
using UnityEngine;

public class Dog : MonoBehaviour
{
    private bool mooving;//возможность передвижения
    private float speed, x, y, xR, yR;// скорость, пер-е координат, окр-еХ, окр-еУ, 
    private Direction dDog;
    private int r;//рандом, количество фреймов пересекаются
    private int maneuver;//возможность совершить маневр
    private const int MANEUVERABILITY = 2;//маневренность
    private const float DIRTY_TIME = 1.5f;// время загрязнения
    private const float ACCURACY = 0.01f;//погрешность поворота
    private SpriteRenderer spriteRenderer;

    public Sprite dogR;
    public Sprite dogB;
    public Sprite dogL;
    public Sprite dogT;
    public Sprite dogDirtyR;
    public Sprite dogDirtyB;
    public Sprite dogDirtyL;
    public Sprite dogDirtyT;
    void Start()
    {
        speed = 4f;
        dDog = Direction.LEFT;
        mooving = true;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    void Update()
    {
        if (mooving)
            MoveDog(dDog);
    }
    private void MoveDog(Direction d)
    {

        switch (d)
        {
            case Direction.TOP: { this.transform.Translate(0, speed * Time.deltaTime, 0); break; }
            case Direction.RIGHT: { this.transform.Translate(speed * Time.deltaTime, 0, 0); break; }
            case Direction.BOTTOM: { this.transform.Translate(0, -speed * Time.deltaTime, 0); break; }
            case Direction.LEFT: { this.transform.Translate(-speed * Time.deltaTime, 0, 0); break; }
        }
        Maneuver();
    }
    private void Maneuver()
    {
        x = this.transform.position.x;
        y = this.transform.position.y;
        xR = Mathf.Round(x);
        yR = Mathf.Round(y);
        if (x < xR + ACCURACY && x > xR - ACCURACY)
        {
            if (y < yR + ACCURACY && y > yR - ACCURACY)
            {
                maneuver++;
                if (maneuver == MANEUVERABILITY) { ChangeDirection(); maneuver = 0; }
            }
        }
    }

    private void ChangeDirection()
    {
        r = Random.Range(0, 4);
        switch (r)
        {
            case 0: { dDog = Direction.TOP; spriteRenderer.sprite = dogT; break; }
            case 1: { dDog = Direction.RIGHT; spriteRenderer.sprite = dogR; break; }
            case 2: { dDog = Direction.BOTTOM; spriteRenderer.sprite = dogB; break; }
            case 3: { dDog = Direction.LEFT; spriteRenderer.sprite = dogL; break; }
        }
    }
    private void PosEqualization(float xP, float yP)
    {
        x = Mathf.Round(xP);
        y = Mathf.Round(yP);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bomb") { StartCoroutine(DirtyDog()); }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Bomb")
        {
            PosEqualization(this.transform.position.x, this.transform.position.y);
            this.transform.position = new Vector2(x, y);
            ChangeDirection();
        }
    }
    private IEnumerator DirtyDog()
    {

        mooving = false;
        GetComponent<Rigidbody2D>().Sleep();
        GetComponent<CircleCollider2D>().enabled = false;
        //смена картинки на грязную
        ChangeDirtyDogImage();
        yield return new WaitForSeconds(DIRTY_TIME);

        mooving = true;
        GetComponent<Rigidbody2D>().WakeUp();
        GetComponent<CircleCollider2D>().enabled = true;
        //нач-я картинка
        ChangeDirtyDogImage();
        yield break;
    }
    private void ChangeDirtyDogImage()
    {
        if (!mooving)
        {
            Debug.Log("moving1: " + mooving);
            switch (dDog)
            {
                case Direction.TOP: { spriteRenderer.sprite = dogDirtyT; break; }
                case Direction.RIGHT: { spriteRenderer.sprite = dogDirtyR; break; }
                case Direction.BOTTOM: { spriteRenderer.sprite = dogDirtyB; break; }
                case Direction.LEFT: { spriteRenderer.sprite = dogDirtyL; break; }
            }
        }
        else
        {
            Debug.Log("moving2: " + mooving);
            switch (dDog)
            {
                case Direction.TOP: { spriteRenderer.sprite = dogT; break; }
                case Direction.RIGHT: { spriteRenderer.sprite = dogR; break; }
                case Direction.BOTTOM: { spriteRenderer.sprite = dogB; break; }
                case Direction.LEFT: { spriteRenderer.sprite = dogL; break; }
            }
        }
    }
}
