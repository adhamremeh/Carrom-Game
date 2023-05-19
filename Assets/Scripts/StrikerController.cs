using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrikerController : MonoBehaviour
{
    private float StrikeLeftBoundry;
    private float StrikeRightBoundry;
    private float scale;
    private float dist;
    public float arrowScalingVal;
    public float powerThrust;

    private GameObject arrow;
    private GameObject TargetDrag;
    [SerializeField]
    private GameObject enemystriker;

    [SerializeField]
    private Text yourScoreText;

    private int yourScoreInt;

    private Vector3 direction;

    private Rigidbody2D rb;

    private bool MoveWithBar = true;

    private bool enemyTurn = false;

    public static bool IntoPocketBool = false;

    // Start is called before the first frame update
    void Start()
    {
        StrikeLeftBoundry = GameObject.Find("StrikerLeftBoundry").transform.position.x;
        StrikeRightBoundry = GameObject.Find("StrikerRightBoundry").transform.position.x;
        arrow = GameObject.Find("helper arrow");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position) * -1.0f;

        if (MoveWithBar)
            UpdatePos();

        IntoPocket();


        //////////////////////// drag on striker////////////////////////
        if (Input.GetMouseButton(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject && targetObject.name == "striker")
            {
                TargetDrag = targetObject.transform.gameObject;
                MoveWithBar = false;
            }
        }

        if (TargetDrag)
        {
            ///////////////////// scale help arrow //////////////////////
            dist = Vector3.Distance(mousePosition, transform.position) - 10;
            scale = (dist * arrowScalingVal);
            if (scale > 12)
                scale = 12;
            arrow.transform.localScale = new Vector3(scale, scale, scale);
        }

        if (Input.GetMouseButtonUp(0))
        {
            yourScoreInt = int.Parse(yourScoreText.text);

            ///// add force to striker /////
            if (TargetDrag != null)
            {
                TargetDrag = null;
                arrow.SetActive(false);
                
                // to set limit for thrust
                direction.Normalize();

                rb.AddForce(direction * powerThrust * scale);

                FindObjectOfType<Strike_Bar>().ChangeTurn();
                StartCoroutine(waitForTurn());
            }
        }
        ////////////////////////////////////////////////////////////////
    

        if (rb.velocity.x == 0 && rb.velocity.y == 0 && enemyTurn && !IntoPocketBool)
        {
            gameObject.SetActive(false);
            transform.position = new Vector3(-0.001f, -1.418f, 0);
            arrow.transform.localScale = new Vector3(1.49f, 1.49f, 1.49f);
            arrow.SetActive(true);
            if (FindObjectOfType<CheckContinueTurn>().CheckContinue(yourScoreInt))
            {
                FindObjectOfType<Strike_Bar>().ChangeTurn();
                gameObject.SetActive(true);
                GetTurn();
            }
            else
            {
                enemystriker.SetActive(true);
                enemystriker.GetComponent<EnemyAI>().playTurn();
            }
        }

    }

    void UpdatePos()
    {
        float xPos = StrikeLeftBoundry + ((StrikeRightBoundry - StrikeLeftBoundry) * Strike_Bar.StrikerMovePercent);
        if (xPos < StrikeLeftBoundry)
            xPos = StrikeLeftBoundry;
        else if (xPos > StrikeRightBoundry)
            xPos = StrikeRightBoundry;
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }

    IEnumerator waitForTurn()
    {
        yield return new WaitForSeconds(1.0f);
        enemyTurn = true;
    }

    public void GetTurn()
    {
        enemyTurn = false;
        MoveWithBar = true;
        IntoPocketBool = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    public void IntoPocket()
    {
        if (IntoPocketBool)
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        if (GetComponent<CircleCollider2D>().enabled == false)
        {
            gameObject.SetActive(false);
            transform.position = new Vector3(-0.001f, -1.418f, 0);
            transform.localScale = new Vector3(0.03833493f, 0.03833493f, 0.03833493f);
            arrow.transform.localScale = new Vector3(1.49f, 1.49f, 1.49f);
            arrow.SetActive(true);
            enemystriker.SetActive(true);
            enemystriker.GetComponent<EnemyAI>().playTurn();
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
