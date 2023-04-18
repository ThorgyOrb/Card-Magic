using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;



public class deckManagger : MonoBehaviour
{
    public GameObject newCard;
    //text mesh pro for the text
    public TextMeshPro c1;
    //array of 5 positions
    public Vector3[] positions = new Vector3[5];
    //array of 5 position for hand
    public Vector3[] handPositions = new Vector3[5];
    //array of 5 cards hand 
    public GameObject[] hand = new GameObject[5];
    public GameObject[] field = new GameObject[5];
    //array of 5 cards hand aux 
    
    public GameObject[] fusion = new GameObject[5];
    //array of 40 cards in deck
    public GameObject[] deck = new GameObject[40];
    public GameObject[] deckGame = new GameObject[40];
    //array of 5 bools to check if card is in hand
    public bool[] inHand = new bool[5];
    public bool[] inHandAux = new bool[5];
    public int cardsInDeck = 40;

    //array variable of cards script
    public cards[] cardsScriptH = new cards[5];
    public cards[] cardsScriptH1 = new cards[5];
    public cards[] cardsScriptF = new cards[5];
    public cards[] cardsField = new cards[5];
    public cards[] cardsScriptD = new cards[40];
    public bool[]  inField = new bool[5];

    //list of cards
    public List<GameObject> cardsList = new List<GameObject>();
    //array of 6 cards to save atack

    //array of 5 bools to check if card is in fusion
    public bool[] inFusion = new bool[5];
    public int cardsInFusion = 0;
    public int ran = 0;

    //bool is fusion
    public bool isFusion = false;

    public Animator mp1;
    public int posicion =6 ;
    public int cardsInHand = 0;

    

    // Start is called before the first frame update
    void Start()
    {  
        //array deck empty
        for(int i = 0; i < 40; i++)
        {
            deck[i] = null;
        }
 
        System.IO.StreamReader file = new System.IO.StreamReader("Assets/User/Deck.txt");
        //read the file
        for(int i = 0; i < 40; i++)
        {
            string line = file.ReadLine();
            //save the name of the card whit other name

            deck[i] = Resources.Load<GameObject>("Prefabs/"+line) ;
            //change the name of the object in deck array

            
        }
        //close the file
        file.Close();

        List<int> numerosGuardados = new List<int>();
        int posicionAleatoria;
        for (int i = 0; i < 40; i++)
        {
            do {
                posicionAleatoria = Random.Range (0, 40);
            } while (numerosGuardados.Contains(posicionAleatoria));
            numerosGuardados.Add(posicionAleatoria);
            //Debug.Log(posicionAleatoria);
            deckGame[i] = deck[posicionAleatoria];
            //spawn cards in deck
            newCard = Instantiate(deckGame[i], positions[0], Quaternion.identity);
            newCard.name = ""+(40+i);
            cardsList.Add(newCard);
            //destroy the cards in deckGame 
           
            //set false newCard
            
            
            
      
            //deckGame[i].name = ""+ i;

            //fill the list of cards and change the name of the cards

        }
       
        for (int i = 0; i < 40; i++)
        {
           cardsScriptD[i] = deckGame[i].GetComponent<cards>();
           cardsScriptD[i].id = i;
          
        }
        
        

               
         //set positions
        positions[0] = new Vector3(-2.89f, 2.31f, -3.37f);
        positions[1] = new Vector3(-1.43f, 2.31f, -3.37f);
        positions[2] = new Vector3(-0.02f, 2.31f, -3.37f);
        positions[3] = new Vector3(1.43f,2.31f, -3.37f);
        positions[4] = new Vector3(2.86f, 2.31f, -3.37f);

        //set hand positions
        handPositions[0] = new Vector3(-2.89f, 2.31f, -7.44f);
        handPositions[1] = new Vector3(-1.43f, 2.31f, -7.44f);
        handPositions[2] = new Vector3(-0.02f, 2.31f, -7.44f);
        handPositions[3] = new Vector3(1.43f, 2.31f, -7.44f);
        handPositions[4] = new Vector3(2.86f, 2.31f, -7.44f);

   



        //set onFusion to false
        for (int i = 0; i < 5; i++)
        {
            inFusion[i] = false;
        }


     

        
       

        //fill hand with cards from deck and remove them from deck 
        for (int i = 0; i < 5; i++)
        {   
            
            hand[i] = cardsList[i];  
            cardsList[i] = null;
            cardsInDeck--;
            
        
        }
         //hand[0].GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/" + 1);
         //hand[1].GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/" + 2);
        //fill the arrat of cards script with the cards script of the cards in hand
        for (int i = 0; i < 5; i++)
        {
           cardsScriptH[i] = hand[i].GetComponent<cards>();
          
           
        }
        //spawn cards in hand and add id to card
       //call courutine
        StartCoroutine(draw1());
        

        //count cards in hand
        for (int i = 0; i < 5; i++)
        {
            if (hand[i] != null)
            {
                cardsInHand++;
            }
        }
        
           
    }

    // Update is called once per frame
    void Update()
    {   
       
                
 

        //check if card is in hand and no repeat cards in hand
        
        for (int i = 0; i < 5; i++)
        {
            if (hand[i] != null)
            {
                inHand[i] = true;
                //cardsInHand++;
            }

        }
        

        //fill fusion array with cards from card clicked in hand
          if (Input.GetMouseButtonDown(0))
        {
           
            //raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "card" )
            {
                //add card to fusion
                for (int i = 0; i < 5; i++)
                {
                    if (fusion[i] == null  )
                    {
                        //active all child of the prefab
                        for (int j = 0; j < hit.collider.gameObject.transform.childCount; j++)
                        {
                            //hit.collider.gameObject.transform.GetChild(j).gameObject.SetActive(true);
                            posicion=6;
                            //active the child 1 of the prefab
                            hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        }
                        
                       //get text mesh pro of the prefab
                        TextMeshPro text = hit.collider.gameObject.GetComponentInChildren<TextMeshPro>();
                        //write 1 in the text mesh pro
                        text.text = ""+(i+1);
                        fusion[i] = hit.collider.gameObject;
                        //add card to cardscriptF
                        cardsScriptF[i] = fusion[i].GetComponent<cards>();
                        cardsInFusion++;
                        inFusion[i] = true;
                        
                        Debug.Log(cardsScriptF[i].atack);
                        //Debug.Log(cardsScriptH[i].id);
                        //get the index of the card clicked
                      

                        break;

                        

                    }

                    //check if card is already in fusion
                    else if (Physics.Raycast(ray, out hit) && fusion[i] == hit.collider.gameObject && i != cardsInFusion-1)
                    {
                        Debug.Log("card already in fusion");
                        break;
                    }
                    //remove card from list if clicked again and only in reverse order

                    else if (Physics.Raycast(ray, out hit) && fusion[i] == hit.collider.gameObject && i == cardsInFusion-1&& inFusion[i]==true) 
                    {
                        fusion[i] = null;
                        cardsInFusion--;
                        inFusion[i] = false;
                        //set false child of the prefab
                        for (int j = 0; j < hit.collider.gameObject.transform.childCount; j++)
                        {
                            hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        }
                        break;
                    }
                   
                }
                
            } 
           

        }

       if (Input.GetKeyDown(KeyCode.Space))
        {
           StartCoroutine( spawn());
           //set true in field 
           //mp1.SetBool("inField", true);
            
        }

     
       

        OnMouseDown();
       

        selectPosition();
      

        
    }

   

    //function to draw card
    public void drawCard()
    {
        //check if there is a card in deck
        for (int i = 0; i < 40; i++)
        {
            if (cardsList[i] != null)
            {
                //check if there is a free space in hand
                for (int j = 0; j < 5; j++)
                {
                    if (inHand[j] == false)
                    {
                        //add card to hand
                        hand[j] = cardsList[i];
                        //add card to card script array
                        cardsScriptH[j] =cardsScriptD[i].GetComponent<cards>();

                        //remove card from deck
                        cardsList[i] = null;
                        //spawn card in hand
                        Instantiate(hand[j], handPositions[j], Quaternion.identity);
                        cardsInHand++;
                        cardsInDeck--;
                        //break loop
                        break;
                    }
                }
                //break loop
                break;
            }
        }
    }

    //move cards in hand to the left and set the last card to null
   
   

    //search the cards in fusion and then remove them from hand
 public void removeCardFromHand()
    {
        int sameCards = 0;
        //search the cards in fusion
        for (int i = 0; i < 5; i++)
        {
            if (fusion[i] != null)
            {
                //search the cards in hand
                for (int j = 0; j < 5; j++)
                {
                   
                    if (hand[j] != null)
                    {
                        


                        if (fusion[i].name == hand[j].name+"(Clone)")
                        {
                            for (int k = 0; k < 5; k++)
                            {
                                if (hand[k] != null)
                                {
                                    if (hand[j].name == hand[k].name)
                                    {
                                        //identify the same cards
                                        sameCards++;
                                    }
                                }
                            }
                            //remove card from hand
                            hand[j] = null;
                            //inHand[j] = false;
                            //set false in hand
                            inHand[j] = false;
                            //cardsScriptH[j] =cardsScriptH1[j];
                            //remove card from card script array
                            
                            //break loop
                            break;
                        }
                    }
                }
            }
        }
    
    }
//identify the cards repeated in hand
    public void identifySameCards()
    {
        int sameCards = 0;
        //search the cards in fusion
        for (int i = 0; i < 5; i++)
        {
            if (fusion[i] != null)
            {
                //search the cards in hand
                for (int j = 0; j < 5; j++)
                {
                    if (hand[j] != null)
                    {
                        if (fusion[i].name == hand[j].name + "(Clone)")
                        {
                            for (int k = 0; k < 5; k++)
                            {
                                if (hand[k] != null)
                                {
                                    if (hand[j].name == hand[k].name)
                                    {
                                        //identify the same cards
                                        sameCards++;
                                    }
                                }
                            }
                            //remove card from hand
                            hand[j] = null;
                            //inHand[j] = false;
                            //set false in hand
                            inHand[j] = false;
                            //cardsScriptH[j] =cardsScriptH1[j];
                            //remove card from card script array
                            //cardsScriptH[j] = null;
                            //break loop
                            break;
                        }
                    }
                }
            }
        }
    }

    public void OnMouseDown()
    {
        if (transform.position.x == -4.68f && transform.position.y == 0.5f && transform.position.z == 0.5f)
        {
            Debug.Log("Position 1");
        }
    }




    //compare all cards in fusion 2 by 2 to compare the stats and combine them
    public void Fusion1()
    {
        int x = 1;
        //check if there ara more of 2 cards in fusion
        if (cardsInFusion >= 2)
        {
            for(int i =0; i < cardsInFusion; i++)
            {

            
                if ((cardsScriptF[i].cardType == "Rock" && cardsScriptF[x].cardType =="Fairy") || (cardsScriptF[i].cardType == "Fairy" && cardsScriptF[x].cardType =="Rock"))
                {
                    Debug.Log("Fusion");

                }else
                {
                    Debug.Log("No Fusion");
                }
                if(x != cardsInFusion)
                {
                    x++;
                    Debug.Log("x = " + x);
                }
                if(x==5){
                    break;
                }
            }
        }   
    }
    
    //courutine to select the position of the card
    public IEnumerator spawn()
    {
        // Fusion2();
            //Fusion1();
           mp1.SetBool("inField", true);
            //set false child of the prefab
            for (int i = 0; i < 5; i++)
            {
                if (fusion[i] != null)
                {
                    for (int j = 0; j < fusion[i].transform.childCount; j++)
                    {
                        fusion[i].transform.GetChild(0).gameObject.SetActive(false);
                        
                    }
                }
            }
            //removeCardFromHand();
            identifySameCards();
            
            //spawn cards in fusion

            for (int i = 0; i < 5; i++)
            {
                
                yield return new WaitUntil(() => posicion != 6);
                if (fusion[i] != null)
                {  
                   yield return new WaitUntil(() =>  posicion != 6);
        
                    int x = cardsInFusion; 
     

                    fusion[i].transform.position = positions[posicion];
                    inField[posicion] = true;
                    field[posicion] = fusion[i];
                    cardsField[posicion] = field[posicion].GetComponent<cards>();
                    fusion[i].transform.rotation = Quaternion.Euler(90,0, 0);
                    //asign new tag
                    fusion[i].tag = "cardField";   
                                 
 
                    for (int j = 0; j < x-1; j++)
                    {   
                        
                        if (fusion[j] != null)
                        {
                            Destroy(fusion[j]);
                            
                            
                                                
                        }
                    }
                    
                    fusion[i]=null;
                    inFusion[i] = false;
                    //cardsScriptF[i] = cardsScriptH1[i];
                    x=0;
                    cardsInHand-=cardsInFusion;
                    cardsInFusion = 0;  
                    yield return new WaitForSeconds(0.5f); 
                    //count cards in hand
                    
                            
                     
                    
                }
            }
       
                

          
        
    }  

    //select the field position
    public bool selectPosition()
    {
         if (Input.GetMouseButtonDown(0))
        {
        //raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "1")
            {
                posicion = 0;
               
            }
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "2")
            {
                posicion = 1;
                
                
            }
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "3")
            {
                posicion = 2;
                
            }
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "4")
            {
                posicion = 3;
                
            }
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "5")
            {
                posicion = 4;
                
            }
        }
        return true;
    }

 public IEnumerator draw1(){
    for (int i = 0; i < 5; i++)
        {
            //change the name of the object in deckGame array
           // hand[i].name = "card" + i;


            Instantiate(hand[i], handPositions[i], Quaternion.identity);
            //newCard.name = i+"(Clone)" ;
            //cardsList.Add(newCard);
            //hand[i].name = "card" + i;
            yield return new WaitForSeconds(.5f);
        }
    
 }
  

   /* public void Fusion2()
    {
        //variables
        int x = 1;
        //check if there are 2 or more cards in fusion
        if (cardsInFusion >= 2)
        {
            int z = cardsInFusion;
            int y = 0;
            //compare card 1 with card 2
            for(int i =0; i < cardsInFusion; i++)
            {    
                
                if ((cardsScriptF[i].cardType == "Rock" && cardsScriptF[x].cardType =="Fairy") || (cardsScriptF[i].cardType == "Fairy" && cardsScriptF[x].cardType =="Rock"))
                {
                    Debug.Log("Fusion");
                    isFusion = true;
                    //destroy x to fusion
                    Destroy(fusion[x]);  
                    //cardsScriptH[x]= cardsScriptH1[x]; 
                    //cardsScriptF[x]= cardsScriptH1[x];     
                    Instantiate(Resources.Load<GameObject>("Prefabs/9"), positions[0], Quaternion.Euler(-90, 180, 0));                  
                    y = x;
                    z-=1;
                    if(x==y){
                        
                        isFusion = true;
                        Debug.Log("y = " + y);
                        Debug.Log("x = " + x);
                        Debug.Log("z = " + z);
                        break;
                        
                    }
                }
                if(x  <cardsInFusion)
                {
                    x++;                 
                } 
            }
            if(z!=y)
            {
                //Destroy prefab call 6(Clone)
                Destroy(GameObject.Find("9(Clone)"));
            }
        }
    }

*/

}
