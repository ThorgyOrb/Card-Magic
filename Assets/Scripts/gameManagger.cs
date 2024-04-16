using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
//acces to deckManagger script



public class gameManagger : MonoBehaviour
{
    public int playerLife = 8000;
    public int cpuLife = 8000;
    public int turno = 1;
    public TMP_Text turnotxt;
    public TMP_Text lpPlayer;
    public TMP_Text lpCpu;
    public TMP_Text cardsPlayer;
    public TMP_Text cardsCpu;
    public GameObject Draw;
    public Animator mp1;
    public deckManagger deck;
    //array of two cards
    public GameObject[] battle = new GameObject[2];
    public cards[] cardsF = new cards[2];
    public string[] cardsName = new string[2];

    public cards cardScript;

    public bool battleP=false;
    public bool mainP = true;

    public bool isDefence = false;

    // Start is called before the first frame update
    void Start()
    {
       
        turnotxt.text = "" + turno;
        lpPlayer.text = "LP   " + playerLife;
        lpCpu.text = "LP   " + cpuLife;
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        cardsPlayer.text = "" + deck.cardsInDeck;
        lpPlayer.text = "LP   " + playerLife;
        lpCpu.text = "LP   " + cpuLife;
        if(turno % 2 == 0 || turno == 1  )
        {
           
           Draw.SetActive(false);
           
           
        }
        if(deck.cardsInHand == 5)
        {
            Draw.SetActive(false);
        }


        if (deck.cardsInHand != 5 && turno % 2 != 0 && turno != 1)

        {
            Draw.SetActive(true);
        }
 
         turnotxt.text = "" + turno;
      /*  //add clikef card to fusion
      
        if (Input.GetMouseButtonDown(0))
        {
            //raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "card")
            {
                //add card to fusion
                for (int i = 0; i < 5; i++)
                {
                    if (fusion[i] == null)
                    {
                        fusion[i] = hit.collider.gameObject;
                        break;
                    }
                }
            }
        }

        //remove card from fusion
        if (Input.GetMouseButtonDown(1))
        {
            //raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //remove card from fusion
                for (int i = 0; i < 5; i++)
                {
                    if (fusion[i] == hit.collider.gameObject)
                    {
                        fusion[i] = null;
                        break;
                    }
                }
            }
        }

        //move cards to click position
        if (Input.GetMouseButtonDown(2))
        {
            //raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //if click in object with tag "1" move card to position 1
                if (hit.collider.gameObject.tag == "1")
                {
                    //move card to position 1
                    for (int i = 0; i < 5; i++)
                    {
                        
                            fusion[i].transform.position = positions[0];
                            //change rotation
                            fusion[i].transform.rotation = Quaternion.Euler(-90,180, 0);
                          
                        fusion[i] = null;
                    }
                }
                //if click in object with tag "2" move card to position 2
                if (hit.collider.gameObject.tag == "2")
                {
                    //move card to position 2
                    for (int i = 0; i < 5; i++)
                    {
                        
                            fusion[i].transform.position = positions[1];
                            fusion[i].transform.rotation = Quaternion.Euler(-90,180, 0);
                        fusion[i] = null;
                    }
                }
                //if click in object with tag "3" move card to position 3
                if (hit.collider.gameObject.tag == "3")
                {
                    //move card to position 3
                    for (int i = 0; i < 5; i++)
                    {
                        
                            fusion[i].transform.position = positions[2];
                            fusion[i].transform.rotation = Quaternion.Euler(-90,180, 0);
                        fusion[i] = null;
                    }
                }
                //if click in object with tag "4" move card to position 4
                if (hit.collider.gameObject.tag == "4")
                {
                    //move card to position 4
                    for (int i = 0; i < 5; i++)
                    {
                        
                            fusion[i].transform.position = positions[3];
                            fusion[i].transform.rotation = Quaternion.Euler(-90,180, 0);
                        fusion[i] = null;
                    }
                }
                //if click in object with tag "5" move card to position 5
                if (hit.collider.gameObject.tag == "5")
                {
                    //move card to position 5
                    for (int i = 0; i < 5; i++)
                    {
                        
                            fusion[i].transform.position = positions[4];
                            fusion[i].transform.rotation = Quaternion.Euler(-90,180, 0);
                        fusion[i] = null;
                    }
                }

                
            }
        }



*/
    mainPase();
    battlePhase();
   
    }

    public void nextTurn()
    {   
        mainP = true;
        battleP = false;
        mp1.SetBool("inField", false);
        turno++;
        turnotxt.text = "Turno: " + turno;
        if(turno % 2 == 0)
        {
            Debug.Log("cpu turn");
            mp1.SetBool("cpuTurn", true);
           // mp1.SetBool("playerTurn", false);
            
        }
        if(turno % 2 != 0)
        {
            Debug.Log("player turn");
           
        }
    }

    public void battlePhase()
    {
        //mainP = false;
        //on mouse click 
        if(battleP == true)
        {
        if (Input.GetMouseButtonDown(0))
        {
            //raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //add card to battle
                for (int i = 0; i < 2; i++)
                {
                    if (battle[0] == null && hit.collider.gameObject.tag == "cardField")
                    {
                        i+=1;
                        battle[0] = hit.collider.gameObject;
                        cardsF[0] = battle[0].GetComponent<cards>();
                        Debug.Log(cardsF[0].name);
                        cardsName[0] = cardsF[0].name;
                        if(cardsF[0].atk == false)
                        {
                            battle[0]= null;
                            cardsF[0] = null;
                            cardsName[0] = null;
                        }
                     
                        
                        Debug.Log(i);
                        break;
                    }
                    if (battle[1] == null && hit.collider.gameObject.tag == "cardFieldEnemy")
                    {
                        battle[1] = hit.collider.gameObject;
                        cardsF[1] = battle[1].GetComponent<cards>();
                        cardsName[1] = cardsF[1].name;
                        if (cardsF[1].atk == false)
                        {
                            battle[1] = null;
                            cardsF[1] = null;
                            cardsName[1] = null;
                        }
                        break;
                    }
                    //if the card is already in battle remove it
                    if (battle[i] == hit.collider.gameObject)
                    {
                        battle[i] = null;
                        cardsF[i] = null;
                        cardsName[i] = null;
                        break;
                    }
                    
                }
                //if mouse right click erase card from battle
              
            }
            
        }
        if (Input.GetMouseButtonDown(1))
            {
                     
                battle[0] = null;
                         
            }
        //destroy card with less atk
        if (battle[0] != null && battle[1] != null)
        {
        
            if(cardsF[0].GetComponent<cards>().isDefending== false && cardsF[1].GetComponent<cards>().isDefending== false )
            {
            if (cardsF[0].GetComponent<cards>().atack < cardsF[1].GetComponent<cards>().atack)
            {       
                cardsF[0].atk = false;       
                playerLife -= cardsF[1].GetComponent<cards>().atack - cardsF[0].GetComponent<cards>().atack;  
                cardsF[0] = null;
                battle[0] = null;
                Destroy(GameObject.Find(cardsName[0]));
                cardsF[1] = null;
                battle[1] = null;
            }
            if(cardsF[0].GetComponent<cards>().atack == cardsF[1].GetComponent<cards>().atack)
            {
                cardsF[0].atk = false;
                cardsF[0] = null;
                battle[0] = null;
                Destroy(GameObject.Find(cardsName[0]));
                Destroy(GameObject.Find(cardsName[1]));
                cardsF[1] = null;
                battle[1] = null;
            }
            else
            {   
                cardsF[0].atk = false;            
                cpuLife -= cardsF[0].GetComponent<cards>().atack - cardsF[1].GetComponent<cards>().atack;  
                cardsF[1] = null;
                battle[1] = null;
                Destroy(GameObject.Find(cardsName[1]));
                cardsF[0] = null;
                battle[0] = null;         
            }
            }

            if (cardsF[0].GetComponent<cards>().isDefending == false  && cardsF[1].GetComponent<cards>().isDefending == true)
            {
                if (cardsF[0].GetComponent<cards>().atack < cardsF[1].GetComponent<cards>().defence)
                {
                    playerLife -=  cardsF[1].GetComponent<cards>().defence- cardsF[0].GetComponent<cards>().atack;
                    cardsF[0].atk = false;
                    cardsF[0] = null;
                    battle[0] = null;
                    cardsF[1] = null;
                    battle[1] = null;
                }
                else
                {
                   
                    cardsF[0].atk = false;
                    cardsF[0] = null;
                    battle[0] = null;
                    Destroy(GameObject.Find(cardsName[1]));
                    cardsF[1] = null;
                    battle[1] = null;
                }
                
            }
        }
    }  
        
    }

    public void activeBattle()
    {
        battleP = true;
        mainP = false;
    }

    public void mainPase()
    {       
        if (mainP == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "cardField")
            {
                //get the script of the card
                cardScript = hit.collider.gameObject.GetComponent<cards>();
                if (Input.GetMouseButtonDown(0))
                {
                cardScript.isDefending = true;
                //change the rotation of the card
                hit.collider.gameObject.transform.rotation = Quaternion.Euler(90, -90, 0);
                }

            }
         }    //if click mouse set defence
           


    }
}
