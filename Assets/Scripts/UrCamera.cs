using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrCamera : MonoBehaviour
{
     //What the camera is looking at
    Vector3 cameraFocus;

    //Position of camera
    Vector3 cameraPosition;
    //values for angled coordinants of camera vector
    //theta Angle from +x-axis

    //phi angle from +y-axis
    float theta,phi;

    //values of horizontal and verticle panning speed
    float thetaIncrement,phiIncrement;
    //radius of camera sphere
    int radius;
    //zoom speed
    int radiusIncrement;
    // Start is called before the first frame update
    void Start()
    {
        //Initial postions of circle.
        radius=10;
        theta=3.0f*Mathf.PI/2.0f;
        phi= Mathf.PI / 4.0f; 

        //panning speeds
        thetaIncrement=0.025f;
        phiIncrement=0.015f;
        radiusIncrement=1;

        //Declare initial values for variables
        cameraFocus = new Vector3(0,0,0);

        //Declare Initial Values using the set parameters
        //Considering the coordinant system. The board rests in the xz plain.
        //above the board is a postive y direciton.
        cameraPosition= new Vector3(radius * Mathf.Sin(phi)*Mathf.Cos(theta),radius * Mathf.Cos(phi),radius* Mathf.Sin(phi)*Mathf.Sin(theta));
        
        //Set camera to intial position
        this.transform.position=cameraPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Camera Must look at orgin.
        this.transform.LookAt(cameraFocus);

        //Rotate
        //Rotate the camera around a sphere on rails.
        if(Input.GetKey("up")){
            Debug.Log("Up arrow Key is being held...");
            //Incremnt the angle
            if(phi>0.01f){
             phi-=phiIncrement;   
            }
            

            //Adjust postion vector
            cameraPosition.x=radius*Mathf.Sin(phi)*Mathf.Cos(theta);
            cameraPosition.y=radius*Mathf.Cos(phi);
            cameraPosition.z=radius*Mathf.Sin(phi)*Mathf.Sin(theta);

            //Set camera to postion vector
            this.transform.position=cameraPosition;
        }
         if(Input.GetKey("down")){
            Debug.Log("Down arrow Key is being held...");
             //Increment the angle within the limits
            if(phi<Mathf.PI / 2.0f){
                phi+=phiIncrement;  
            }

            //Adjust postion vector
            cameraPosition.x=radius * Mathf.Sin(phi)*Mathf.Cos(theta);
            cameraPosition.y=radius * Mathf.Cos(phi);
            cameraPosition.z=radius* Mathf.Sin(phi)*Mathf.Sin(theta);

            //Set camera to postion vector
            this.transform.position=cameraPosition;
        }
         if(Input.GetKey("right")&&theta< 3* Mathf.PI){
             //Incremnt the angle
            theta+=thetaIncrement;

            //Adjust postion vector
            cameraPosition.x=radius * Mathf.Sin(phi)*Mathf.Cos(theta);
            cameraPosition.z=radius* Mathf.Sin(phi)*Mathf.Sin(theta);

            //Set camera to postion vector
            this.transform.position=cameraPosition;
            Debug.Log("Right arrow Key is being held...");
        }
         if(Input.GetKey("left")&&theta> -3* Mathf.PI){
              //Incremnt the angle
            theta-=thetaIncrement;

            //Adjust postion vector
            cameraPosition.x=radius * Mathf.Sin(phi)*Mathf.Cos(theta);
            cameraPosition.z=radius* Mathf.Sin(phi)*Mathf.Sin(theta);

            //Set camera to postion vector
            this.transform.position=cameraPosition;
            Debug.Log("left arrow Key is being held...");
        }

        //Zooms
        if(Input.GetKeyDown("w")){
            if(radius>6){
                Debug.Log("w Key is being held...");
                //increment zoom
                radius-=radiusIncrement; 
             
                //adjust set position
                cameraPosition.x=radius * Mathf.Sin(phi)*Mathf.Cos(theta);
                cameraPosition.y=radius * Mathf.Cos(phi);
                cameraPosition.z=radius* Mathf.Sin(phi)*Mathf.Sin(theta); 

                this.transform.position=cameraPosition;
            }
           
        }
         if(Input.GetKeyDown("s")){
            Debug.Log("d Key is being held...");
            if(radius<11){

                //Increment Radius
                radius+=radiusIncrement;  

                //adjust set position
                cameraPosition.x=radius * Mathf.Sin(phi)*Mathf.Cos(theta);
                cameraPosition.y=radius * Mathf.Cos(phi);
                cameraPosition.z=radius* Mathf.Sin(phi)*Mathf.Sin(theta); 

                this.transform.position=cameraPosition;
            }
        }
    }
}
