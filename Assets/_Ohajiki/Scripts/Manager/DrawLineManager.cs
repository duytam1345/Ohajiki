using HyperCasualTemplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour
{
    public static DrawLineManager instance;

    public TouchEvent touch;

    public LineRenderer lineRend;

    public bool draw;

    public float strenght;
    public int maxCountPoint;

    public List<Vector3> pointsDraw;

    public bool b = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (draw) {
            lineRend.positionCount = pointsDraw.Count;
            lineRend.SetPositions(pointsDraw.ToArray());

            if (lineRend.positionCount < maxCountPoint) {
                Ray r = Camera.main.ScreenPointToRay(touch.GetTouchPos());
                RaycastHit hit;
                if (Physics.Raycast(r, out hit, 1000, 1 << 7)) {
                    Vector3 currPoint = new Vector3(hit.point.x, .05f, hit.point.z);
                    if (pointsDraw.Count == 0) {
                        pointsDraw.Add(currPoint);
                    } else {
                        if (Vector3.Distance(currPoint, pointsDraw[pointsDraw.Count - 1]) >= strenght) {
                            pointsDraw.Add(currPoint);
                        }
                    }
                }
            }
        }

        //Check Marble hit Line
        if (GameManager.instance.stateGame == GameManager.StateGame.Moving) {
            foreach (var item in pointsDraw) {
                Collider[] colliders = Physics.OverlapSphere(item, .1f, 1 << 6);
                if (colliders.Length > 0 && colliders[0].name == "Player") {
                    b = true;
                }
            }
        }
    }

    public void StartDraw()
    {
        draw = true;
    }

    public void EndDraw()
    {
        draw = false;

        foreach (var item in pointsDraw) {
            Collider[] colliders = Physics.OverlapSphere(item, .05f, 1 << 6);
            //if hit marble -> Red
            if (colliders.Length > 0) {
                lineRend.startColor = new Color(1, 0, 0);
                lineRend.endColor = new Color(1, 0, 0);

                GameManager.instance.SetState(GameManager.StateGame.Lose);
                return;
            }
        }

        //else-> Blue
        lineRend.startColor = new Color(0, 0, 1);
        lineRend.endColor = new Color(0, 0, 1);

        //GameManager.instance.Delay(1, () => {
        //    ResetLine();
        //});

        //StartCoroutine(FadeLine());

        GameManager.instance.SetState(GameManager.StateGame.DragToMove);
    }

    IEnumerator FadeLine()
    {
        Color c = lineRend.startColor;

        float t = 1;
        while (t > 0) {
            c.a -= .02f;

            lineRend.startColor = c;
            lineRend.endColor = c;

            yield return new WaitForSeconds(.02f);
            t -= .02f;
        }

        ResetLine();
    }

    public void ResetLine()
    {
        lineRend.startColor = new Color(1, 1, 1, 1);
        lineRend.endColor = new Color(1, 1, 1, 1);
        lineRend.positionCount = 0;
        pointsDraw.Clear();
        b = false;
    }
}
