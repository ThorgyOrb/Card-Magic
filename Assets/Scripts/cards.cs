using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class cards : MonoBehaviour
{
    //atack
    public int atack;
    //defence
    public int defence;
    //guardian star 
    public string guardianStar;
    //card name
    public string cardName;
    //card type
    public string cardType;
    //card attribute
    public string cardAttribute;
    //card level
    public int cardLevel;
    //card genre
    public string cardGenre;
    //card description
    public string cardDescription;

    public int cardPosition;
    public int cardNumber;

    public int id;

    public bool isDefending = false;
   
    public bool atk = true;
    public bool at = true;

    public TMP_Text atak;
    public TMP_Text def;


    //animator controller
   
 

    // Start is called before the first frame update
    void Start()
    {
        
        def.text = defence.ToString();
        atak.text = atack.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        def.text = defence.ToString();
        atak.text = atack.ToString();
        
        
        
    
                //check if there are 2 or more cards in fusion
      /*  if (cardsInFusion >= 2)
        {
            //compare in pairs of 2 cards in fusion 
            
            for (int i = 0; i < cardsInFusion; i++)
            {
                for (int j = 1; j < cardsInFusion; j++)
                {
                    //cat fusion
                    if (cardsScriptF[i].atack < 800 && cardsScriptF[j].atack < 800)
                    {
                        //check if the cards are the same
                        if (((cardsScriptF[i].cardType=="Fairy" && cardsScriptF[j].cardType=="Best"))||(cardsScriptF[i].cardType=="Best" && cardsScriptF[j].cardType=="Fairy") )
                        {
                            //check if the cards are not the same card
                            if (i != j )
                            {
                                //add the atack of the cards
                                cardsScriptF[i].atack += cardsScriptF[j].atack;
                                Debug.Log("Cat fusion");
                                //add the health of the cards
                              
                                //break loop
                                break;
                            }
                        }
                    }

                   
                }
            }
        }*/
        
    }
}
