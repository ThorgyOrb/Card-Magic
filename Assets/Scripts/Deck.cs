using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    //arrar object deck
    public GameObject[] deck = new GameObject[40];  
    public cards[] cardsScriptD = new cards[40]; 
    // Start is called before the first frame update
    void Start()
    {
        //opne file
        System.IO.StreamReader file = new System.IO.StreamReader("Assets/User/Deck.txt");
        //read the file
        for(int i = 0; i < 40; i++)
        {
            string line = file.ReadLine();
            //show the file
            //Debug.Log(line);

            deck[i] = Resources.Load<GameObject>("Prefabs/"+line);
            //save the name of the card
            //deck[i] = GameObject.Find(line);
            cardsScriptD[i] = deck[i].GetComponent<cards>();
        }
        //close the file
        file.Close();


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) ){
            
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    //nombre.text = hit.collider.gameObject.name;
                }
        }


        
    }
}
