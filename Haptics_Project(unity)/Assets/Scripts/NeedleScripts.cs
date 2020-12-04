using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleScripts : MonoBehaviour
{
    public GameObject CollidedObject;
    public bool CollisionHappend = false;

    public bool isButtonClicked = false;
       

    public GameObject NeedleThreadPoint;

    public Transform NeedDleStopTransform;
    public GameObject NeedleStopPoint;

    public GameObject ThreadFollowPoint;

    public GameObject ForcepPoint;
    

    public bool isPositionFixed = false;


    void OnCollisionEnter(Collision collision)
    {
        CollidedObject = null;
        
        
        CollidedObject = collision.gameObject;
        CollisionHappend = true;
        Debug.Log("Someone Hit me" + CollidedObject.name);
        
    }

    private void OnCollisionExit(Collision collision)
    {
        CollisionHappend = false;
        CollidedObject = null;
    }

    void Update()
    {
        ThreadFollowPoint.transform.position = NeedleThreadPoint.transform.position;
        if(CollisionHappend)
        {
            Debug.Log("Collision Occured" + this.gameObject.name);
        }
        else
        {
            //Debug.Log("No Collision");
        }

        
        if(PluginImport.GetButton1State() == true)
        {
            Debug.Log("Button1 Pressed");
            isButtonClicked = true;
        }
        else
        {
            isButtonClicked = false;
        }

        //if(PluginImport.GetButtonState(1,2))
        //{
        //    Debug.Log("Button1 Pressed");
        //    isButtonClicked = true;            
        //}
        //else
        //{
        //    isButtonClicked = false;
        //}

        if (PluginImport.GetButtonState(1, 1))
        {
            Debug.Log("Button2Pressed");
            
            //GameObject NeedleObject = Instantiate(NeedleStopPoint, NeedDleStopTransform.position, Quaternion.identity, NeedleParent);
            //NeedleObject.name = NeedleParent.transform.childCount.ToString();
        }

        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //isButtonClicked = true;
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            //isButtonClicked = false;
        }

        if(isButtonClicked)
        {
            if(CollisionHappend)
            {                
                if (CollidedObject.name == "forcepNeedlePoint")
                {
                    
                    transform.position = ForcepPoint.transform.position;
                    this.transform.SetParent(ForcepPoint.transform);
                }
                
            }            
        }
        else
        {

            this.transform.SetParent(GameObject.Find("ButtonResetPoint").transform);
        }
    }
}
