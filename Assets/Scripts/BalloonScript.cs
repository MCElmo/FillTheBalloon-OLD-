using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonScript : MonoBehaviour
{
    [SerializeField] private float maxScale = 5f;
    [SerializeField] private float scaleTime = 5f;
    [SerializeField] private float minScale = 2;

    [SerializeField] private float stayScaledTime = 5f;

    [SerializeField] private AnimationCurve balloonUpCurve;
    [SerializeField] private AnimationCurve balloonDownCurve;

    private bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(scaleBalloon());
    }

    // Update is called once per frame
    void Update()
    {
        // //Detect Click
        // if(Input.GetMouseButtonDown(0))
        // {
        //     if(active)
        //     {
        //         StopCoroutine(scaleBalloon());
        //         StopAllCoroutines();
        //     }
        //     else {
        //         StartCoroutine(scaleBalloon());
        //     }
        //     active = !active;
        // }
    }

    private IEnumerator scaleBalloon()
    {
        float time = 0, scale;
        while(true)
        {
           // time = 0;
            //Scale up to sizetime = 0;
            while(time < 1)
            {
              //  print("Delta Time: " + Time.deltaTime);
               // print("Scale: " + gameObject.transform.localScale);
                time += Time.deltaTime/scaleTime;
                scale = minScale + (maxScale - minScale)*balloonUpCurve.Evaluate(time);
                gameObject.transform.localScale = new Vector3(scale,scale,scale);
                yield return null;
            }
            // wait
            yield return new WaitForSeconds(stayScaledTime);
            print("Descaling");
            //Scale down to size 
           // time = 1;
            while(time > 0)
            {
                time -= Time.deltaTime/scaleTime;
                scale = minScale + (maxScale - minScale)*balloonDownCurve.Evaluate(time);
                gameObject.transform.localScale = new Vector3(scale,scale,scale);
                yield return null;
            }
            yield return new WaitForSeconds(stayScaledTime);
            print("Finished Scaling");
        }
    }
}
