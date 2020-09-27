using OculusSampleFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MakeContactTP : MonoBehaviour
{
    [Header("Target Positions")]
    public Vector3[] xyzTarget;
    public Vector3[] rotTarget;
    public string[] nameOfObj;

    [Space(20)]
    [Header("Status indicator Materials")]
    public Material matCorrect;
    public Material matStandard;
    public Material matWrong;


    [Space(20)]
    [Header("Locations of Indicator")]
    public Vector3[] xyzIndicator;
    public Vector3[] scaleIndicator;


    [Space(20)]
    [Header("Steps")]
    public TextMeshPro tmp;
    public string[] steps;


    [Space(20)]
    [Header("Debug")]
    public bool debug;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals(nameOfObj[index]))
        {


            // Make UnMovable
            Destroy(collision.gameObject.GetComponent<BoxCollider>());
            Destroy(collision.gameObject.GetComponent<Rigidbody>());
            Destroy(collision.gameObject.GetComponent<DistanceGrabbable>());

            gameObject.GetComponent<MeshRenderer>().material = matCorrect;
            collision.gameObject.transform.eulerAngles = rotTarget[index];
            collision.gameObject.transform.position = xyzTarget[index];

            if (index == xyzIndicator.Length)
            {
                Destroy(gameObject);
                tmp.alignment = TextAlignmentOptions.Center;
                tmp.text = "Geschafft!\nHerzlichen Glückwunsch";
                return;
            }


            gameObject.transform.position = xyzIndicator[index];
            gameObject.transform.localScale = scaleIndicator[index];
            gameObject.GetComponent<MeshRenderer>().material = matStandard;

            
            if (debug)
            {
                string txt = "\n    XYZ: " + collision.gameObject.transform.position.x + " / " + collision.gameObject.transform.position.y + " / " + collision.gameObject.transform.position.z + "\n    ROT: " + collision.gameObject.transform.rotation.x + " / " + collision.gameObject.transform.rotation.y + " / " + collision.gameObject.transform.rotation.z;
                txt += "\n    XYZ: " + xyzTarget[index].x + " / " + xyzTarget[index].y + " / " + xyzTarget[index].z + "\n    ROT: " + rotTarget[index].x + " / " + rotTarget[index].y + " / " + rotTarget[index].z;
                tmp.text = txt;
            }
            else
            {
                tmp.text = "\n     Schritt " + (index + 2) + ":\n     " + steps[index].Replace('2', '\n');
            }

            index++;
            
        }
        else 
        {
            gameObject.GetComponent<MeshRenderer>().material = matWrong;
        }
    }


    void OnCollisionExit(Collision collision)
    {
        gameObject.GetComponent<MeshRenderer>().material = matStandard;
    }

}
