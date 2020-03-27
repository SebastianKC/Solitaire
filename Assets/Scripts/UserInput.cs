using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public GameObject slot1;
    private Solitaire solitaire;

    // Start is called before the first frame update
    void Start()
    {
        solitaire = FindObjectOfType<Solitaire>();
        slot1 = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseClick();
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Casting ray for interacting
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            // Checking if ray hits anything
            if (hit)
            {
                // What has been hit? Deck/Card/EmptySlot...
                if (hit.collider.CompareTag("Deck"))
                {
                    // Clicked deck
                    Deck();
                }
                else if (hit.collider.CompareTag("Card"))
                {
                    // Clicked card
                    Card(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Top"))
                {
                    // Clicked top
                    Top();
                }
                else if (hit.collider.CompareTag("Bottom"))
                {
                    // Clicked bottom
                    Bottom();
                }
            }
        }
    }

    void Deck()
    {
        // Deck click actions
        print("Clicked on deck");
        solitaire.DealFromDeck();
    }

    void Card(GameObject selected)
    {
        // Card click actions
        print("Clicked on card");

        // If the card clicked on is facedown
        // If it is not blocked
        // Flip it over

        // If the card clicked on is in the deck pile with the trips
        // If it is not locked
        // Select it

        // If the card is face up
        // If there is no card currently selected
        // Select the card

        if (slot1 = this.gameObject) // Not null because we pass in this gameObject instead
        {
            slot1 = selected;
        }

            // If there is already a card selected (and it is not the same card)
                // If the new card is eligible to stack on the old card
                    // Stack it
                // Else
                    // Select the new card

            // Else if there is already a card selected and it is the same card
                // If the time is short enough then it is a double click
                    // If the card is eligible to fly up to the top then do it
    }

    void Top()
    {
        // Top click actions
        print("Clicked on top");
    }

    void Bottom()
    {
        // Bottom click actions
        print("Clicked on bottom");
    }
}
