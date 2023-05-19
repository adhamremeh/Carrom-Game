using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    private float StrikeLeftBoundry;
    private float StrikeRightBoundry;

    public float arrowScalingVal;
    public float powerThrust;

    [SerializeField]
    private GameObject yourStriker;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private GameObject nearestPuck;
    [SerializeField]
    private GameObject[] pucks;

    [SerializeField]
    private Text enemyScoreText;

    private int enemyScoreInt;

    private Rigidbody2D rb;

    private bool endTurn = false;

    public static bool IntoPocketBool = false;

    // Start is called before the first frame update
    void Start()
    {
        StrikeLeftBoundry = GameObject.Find("StrikerLeftBoundry").transform.position.x;
        StrikeRightBoundry = GameObject.Find("StrikerRightBoundry").transform.position.x;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (endTurn)
            CheckForEndTurn();
    }

    public void playTurn()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        IntoPocketBool = false;
        nearestPuck = GameObject.Find("black_far_puck");
        pucks = GameObject.FindGameObjectsWithTag("BlackPuck");
        foreach(GameObject puck in pucks)
        {
            float puckDist = Vector3.Distance(puck.transform.position, transform.position);
            float nearestDist = Vector3.Distance(nearestPuck.transform.position, transform.position);
            if (puckDist < nearestDist)
            {
                nearestPuck = puck;
            }
        }
        LookAtPuck();
    }

    void LookAtPuck()
    {
        Vector3 direction = (nearestPuck.transform.position - transform.position);

        arrow.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        float dist = Vector3.Distance(nearestPuck.transform.position, transform.position);

        float scale = (dist * arrowScalingVal);
        if (scale > 12)
            scale = 12;
        arrow.transform.localScale = new Vector3(scale, scale, scale);

        // to set limit for thrust
        direction.Normalize();
        
        StartCoroutine(HitPuck(direction, scale));
    }

    IEnumerator HitPuck(Vector3 direction, float scale)
    {
        enemyScoreInt = int.Parse(enemyScoreText.text);

        yield return new WaitForSeconds(1.0f);

        arrow.SetActive(false);

        direction.Normalize();

        rb.AddForce(direction * powerThrust * scale);

        yield return new WaitForSeconds(1.0f);

        endTurn = true;
    }

    void CheckForEndTurn()
    {
        if (rb.velocity.x == 0 && rb.velocity.y == 0 && !IntoPocketBool)
        {
            gameObject.SetActive(false);
            transform.position = new Vector3(-0.001f, 1.418f, 0);
            arrow.transform.localScale = new Vector3(1.49f, 1.49f, 1.49f);
            arrow.SetActive(true);
            FindObjectOfType<Strike_Bar>().ChangeTurn();
            endTurn = false;
            if (FindObjectOfType<CheckContinueTurn>().CheckContinue(enemyScoreInt))
            {
                gameObject.SetActive(true);
                FindObjectOfType<Strike_Bar>().ChangeTurn();
                playTurn();
            }
            else
            {
                yourStriker.SetActive(true);
                yourStriker.GetComponent<StrikerController>().GetTurn();
            }
        }

        if (IntoPocketBool)
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        if (GetComponent<CircleCollider2D>().enabled == false)
        {
            gameObject.SetActive(false);
            transform.position = new Vector3(-0.001f, 1.418f, 0);
            transform.localScale = new Vector3(0.03833493f, 0.03833493f, 0.03833493f);
            arrow.transform.localScale = new Vector3(1.49f, 1.49f, 1.49f);
            arrow.SetActive(true);
            FindObjectOfType<Strike_Bar>().ChangeTurn();
            endTurn = false;
            yourStriker.SetActive(true);
            yourStriker.GetComponent<StrikerController>().GetTurn();
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }


}
