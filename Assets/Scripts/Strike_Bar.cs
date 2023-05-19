using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike_Bar : MonoBehaviour
{
    [SerializeField]
    private GameObject YourBarStriker;
    [SerializeField]
    private GameObject YourMoveBar;

    private GameObject TargetDrag; 

    private float LeftBoundry;
    private float RightBoundry;

    private float yourXpos;
    private float enemyXpos;

    public static float StrikerMovePercent;

    public static bool yourTurn = true;

    // Start is called before the first frame update
    void Start()
    {
        LeftBoundry = GameObject.Find("LeftBoundry").transform.position.x;
        RightBoundry = GameObject.Find("RightBoundry").transform.position.x;

        YourBarStriker.SetActive(yourTurn);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //////////////////////// drag bar striker ////////////////////////
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject && targetObject.name == "BarStriker")
            {
                TargetDrag = targetObject.transform.gameObject;
            }
        }
        if (TargetDrag)
        {
            TargetDrag.transform.position = new Vector3(mousePosition.x, TargetDrag.transform.position.y, TargetDrag.transform.position.z);
        }
        if (Input.GetMouseButtonUp(0))
        {
            TargetDrag = null;
        }
        ///////////////////////////////////////////////////////////////

        yourXpos = YourBarStriker.transform.position.x;

        if (yourTurn)
        {
            LimitBarMovement(YourBarStriker);
            StrikerMovePercent = (yourXpos - LeftBoundry)/(RightBoundry - LeftBoundry);
        }
            
    }

    void LimitBarMovement(GameObject barStriker)
    {
        if (barStriker.transform.position.x > RightBoundry)
        {
            barStriker.transform.position = new Vector3(RightBoundry, barStriker.transform.position.y, barStriker.transform.position.z);
        }
        else if (barStriker.transform.position.x < LeftBoundry)
        {
            barStriker.transform.position = new Vector3(LeftBoundry, barStriker.transform.position.y, barStriker.transform.position.z);
        }
    }

    public void ChangeTurn()
    {
        yourTurn = !yourTurn;
        YourBarStriker.SetActive(yourTurn);
        int alpha = yourTurn ? 1 : 0;
        YourMoveBar.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f + (0.5f*alpha));
        YourBarStriker.transform.position = new Vector3(0, YourBarStriker.transform.position.y, 0);
    }
}
