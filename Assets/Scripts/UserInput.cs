using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private Solitaire solitaire;

    // Start is called before the first frame update
    void Start()
    {
        solitaire = FindObjectOfType<Solitaire>();
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
                    Card();
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

    void Card()
    {
        // Card click actions
        print("Clicked on card");
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
