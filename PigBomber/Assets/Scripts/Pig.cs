using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pig : MonoBehaviour
{
    private const float SPEED = 5f;
    private GameObject detonatorO, ui;
    private bool twist, alive;//кручение свинки, жива
    private const float DEATH_TIME = 1.2f;
    private const float ROTATE_Z = 2f;
    private const float DIRTY_TIME = 1f;// время загрязнения
    private Detonator detonator;
    private SpriteRenderer spriteRenderer;
    public Sprite pigR;
    public Sprite pigB;
    public Sprite pigL;
    public Sprite pigT;

    void Awake()
    {
        detonatorO = GameObject.Find("Detonator");
        detonator = detonatorO.GetComponent<Detonator>();
        ui = GameObject.Find("UI");
        alive = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (twist) { transform.Rotate(0, 0, ROTATE_Z, Space.Self); }
        else if (alive)
        {
            if (Input.anyKey)
            {
                if (Input.GetKey("w")) { MoveTop(); }
                if (Input.GetKey("s")) { MoveBottom(); }
                if (Input.GetKey("a")) { MoveLeft(); }
                if (Input.GetKey("d")) { MoveRight(); }
                if (Input.GetKeyDown(KeyCode.Space)) { detonator.PutBomb(); }
            }
        }
    }
    public void MoveTop() { spriteRenderer.sprite = pigT; this.transform.Translate(0, SPEED * Time.deltaTime, 0); }
    public void MoveRight() { spriteRenderer.sprite = pigR; this.transform.Translate(SPEED * Time.deltaTime, 0, 0); }
    public void MoveBottom() { spriteRenderer.sprite = pigB; this.transform.Translate(0, -SPEED * Time.deltaTime, 0); }
    public void MoveLeft() { spriteRenderer.sprite = pigL; this.transform.Translate(-SPEED * Time.deltaTime, 0, 0); }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dog") { StartCoroutine(PigDeath()); }
        if (collision.gameObject.tag == "Bomb") { StartCoroutine(DirtyPig()); }
    }
    private IEnumerator PigDeath()
    {
        ui.SetActive(false);
        twist = true;
        yield return new WaitForSeconds(DEATH_TIME);
        twist = false;
        alive = false;
        yield return new WaitForSeconds(DEATH_TIME);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield break;
    }
    private IEnumerator DirtyPig()
    {
        ui.SetActive(false);
        //обнулить active для всех кнопок
        ui.GetComponentInChildren<T>().Active(false);
        ui.GetComponentInChildren<R>().Active(false);
        ui.GetComponentInChildren<L>().Active(false);
        ui.GetComponentInChildren<B>().Active(false);
        //
        alive = false;
        GetComponent<Rigidbody2D>().Sleep();
        GetComponent<CircleCollider2D>().enabled = false;
        //смена цвета на грязную
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.582f, 0.44f, 0.05f, 1f);
        yield return new WaitForSeconds(DIRTY_TIME);

        ui.SetActive(true);
        GetComponent<Rigidbody2D>().WakeUp();
        GetComponent<CircleCollider2D>().enabled = true;
        alive = true;
        //нач-я картинка
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield break;
    }
}
