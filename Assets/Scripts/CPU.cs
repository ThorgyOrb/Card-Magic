using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU : MonoBehaviour
{
    //deck
    public GameObject[] deck = new GameObject[40];
    //hand
    public GameObject[] hand = new GameObject[5];
    //deckGame
    public GameObject[] deckGame = new GameObject[40];
    public cards[] cardsScriptD = new cards[40];
   
    public cards[] cardsScriptH = new cards[5];

    public bool mainPhase1 = false;
    public gameManagger gameManaggerScript;
    public Animator mp1;
    public cards cardSpawn;
    public deckManagger player;
    public GameObject[] fieldPlayer = new GameObject[5];
    public cards[] cardsScriptF = new cards[5];
    public bool timeR = false;
    



    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.Find("deckManagger").GetComponent<deckManagger>();
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
          

        }
       
        for (int i = 0; i < 40; i++)
        {
           cardsScriptD[i] = deckGame[i].GetComponent<cards>();
           cardsScriptD[i].id = i;
          
        }
         for (int i = 0; i < 5; i++)
        {   
            
           hand[i] = deckGame[i];
           deckGame[i] = null;
            
        
        }
        for (int i = 0; i < 5; i++)
        {
           cardsScriptH[i] = hand[i].GetComponent<cards>();
          
           
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(gameManaggerScript.turno % 2 == 0)
       // {
            
            
        //search for the highest attack card
        if(mainPhase1 == true)
        {
           
            StartCoroutine(wait());
            StartCoroutine(spa());
        }	
        draw();
        
       // }

        
    }

    public void spawnCard()
    {
        
        mainPhase1 = false;
        //get arrat field from player
        fieldPlayer = player.field;
        //get array cardsScriptF from player
        cardsScriptF = player.cardsField;
       


        
        int max = 0;
        int id = 0;
        for (int i = 0; i < 5; i++)
        {
            if(cardsScriptH[i].atack > max)
            {
                max = cardsScriptH[i].atack;
                id = i;
                
            }
       
        }
        //attack
        Debug.Log(max);
        Debug.Log(id);
        //spawn card
        StartCoroutine(wait());
        
      
        
     
        
        GameObject card = Instantiate(hand[id], new Vector3(2.98f, 2.37f, -0.33f), Quaternion.identity); 
        cardSpawn = card.GetComponent<cards>();
        //rotate card
        card.transform.Rotate(-90, 0, 0);
        //change the tag of the card
        card.tag = "cardFieldEnemy";
        //compare cardSpawn attack with fieldPlayer
        for (int i = 0; i < 5; i++)
        {
            if(fieldPlayer[i]!= null)
            {
                if(cardsScriptF[i].atack > cardSpawn.atack)
                {
                    cardSpawn.isDefending = true;
                    //cange position of the card
                    card.transform.rotation = Quaternion.Euler(90, 90, 0);
                    break;
                }
            }
        }
        hand[id] = null;
        cardsScriptH[id] = null;
        gameManaggerScript.turno++;
        
        //call courutine
       

    }
    public void draw() {
        for (int i = 0; i < 5; i++)
        {
            if(hand[i] == null)
            {
                for (int j = 0; j < 40; j++)
                {
                    if(deckGame[j] != null)
                    {
                        hand[i] = deckGame[j];
                        deckGame[j] = null;
                        cardsScriptH[i] = hand[i].GetComponent<cards>();
                       
                        break;
                    }
                }
            }
        }
        
        
    }

    public void mainPhase1Button()
    {
        mainPhase1 = true;
    }
IEnumerator spa(){
    mainPhase1 = false;
        //get arrat field from player
        fieldPlayer = player.field;
        //get array cardsScriptF from player
        cardsScriptF = player.cardsField;
       


        
        int max = 0;
        int id = 0;
        for (int i = 0; i < 5; i++)
        {
            if(cardsScriptH[i].atack > max)
            {
                max = cardsScriptH[i].atack;
                id = i;
                
            }
       
        }
        //attack
        Debug.Log(max);
        Debug.Log(id);
        //spawn card
        StartCoroutine(wait());
        
      
        yield return new WaitForSeconds(3);
     
       
        
            
             
        
        GameObject card = Instantiate(hand[id], new Vector3(2.98f, 2.37f, -0.33f), Quaternion.identity); 
        cardSpawn = card.GetComponent<cards>();
        //rotate card
        card.transform.Rotate(-90, 0, 0);
        //change the tag of the card
        card.tag = "cardFieldEnemy";
        //compare cardSpawn attack with fieldPlayer
        for (int i = 0; i < 5; i++)
        {
            if(fieldPlayer[i]!= null)
            {
                if(cardsScriptF[i].atack > cardSpawn.atack)
                {
                    cardSpawn.isDefending = true;
                    //cange position of the card
                    card.transform.rotation = Quaternion.Euler(90, 90, 0);
                    break;
                }
            }
        }
        hand[id] = null;
        cardsScriptH[id] = null;
        gameManaggerScript.turno++;
        
        //call courutine
}

//courutine to wait animation
    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
        mp1.SetBool("cpuTurn", false);
        mp1.SetBool("playerTurn", true);
    }
}
