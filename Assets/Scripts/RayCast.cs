using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)== true){
               if(hit.collider.gameObject.tag == "card")
                {
                    hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    
                }
  
        }
     
      
     
    }
}
