using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Solitaire : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject deckButton;
    public GameObject[] bottomPos;
    public GameObject[] topPos;

    // Card arrays from Ace of Clubs to King of Spades
    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    public List<string>[] bottoms;
    public List<string>[] tops;
    public List<string> tripsOnDisplay = new List<string>();
    public List<List<string>> deckTrips = new List<List<string>>();

    // Bottom card columns
    private List<string> bottom0 = new List<string>();
    private List<string> bottom1 = new List<string>();
    private List<string> bottom2 = new List<string>();
    private List<string> bottom3 = new List<string>();
    private List<string> bottom4 = new List<string>();
    private List<string> bottom5 = new List<string>();
    private List<string> bottom6 = new List<string>();

    // Deck related variables
    public List<string> deck;
    public List<string> discardPile = new List<string>();
    private int deckLocation;
    private int trips;
    private int tripsRemainder;

    // Start is called before the first frame update
    void Start()
    {
        // Initiate list for bottom objects
        bottoms = new List<string>[] { bottom0, bottom1, bottom2, bottom3, bottom4, bottom5, bottom6 };
        // LET'S GOOOOOOO
        PlayCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Ready the cards to be placed
    public void PlayCards()
    {
        deck = GenerateDeck();
        Shuffle(deck);

        // Print the card names for debugging
        foreach (string card in deck)
        {
            print(card);
        }

        SolitaireSort();
        StartCoroutine(SolitaireDeal());
        SortDeckIntoTrips();
    }

    // Adding the suits and values together to make a card (club + 3 = 3 of clubs)
    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string s in suits)
        {
            foreach (string v in values)
            {
                newDeck.Add(s + v);
            }
        }

        // Returns new deck with suits and values put together
        return newDeck;
    }

    // Shuffle method
    void Shuffle<T>(List<T> list)
    {
        // Uses Random function to randomise the order of the deck
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            // Iterate through the list of cards
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    // Dealing method
    IEnumerator SolitaireDeal()
    {
        // Iterates through the cards until 7 columns are built with a maximum of 1, 2, 3, 4, 5, 6, 7 cards in each column respectively
        for (int i = 0; i < 7; i++)
        {
            // Base offset for the cards
            float yOffset = 0;
            float zOffset = 0.03f;

            foreach (string card in bottoms[i])
            {
                // Delay between each card being placed
                yield return new WaitForSeconds(0.05f);

                // Card's location
                GameObject newCard = Instantiate(cardPrefab, new Vector3(bottomPos[i].transform.position.x, bottomPos[i].transform.position.y - yOffset, bottomPos[i].transform.position.z - zOffset), Quaternion.identity, bottomPos[i].transform);
                newCard.name = card;

                // Tells the bottom card of each column to be face up
                if (card == bottoms[i][bottoms[i].Count -1])
                {
                    newCard.GetComponent<Selectable>().faceUp = true;
                }

                // Each new card's offset on the board
                yOffset = yOffset + 0.3f;
                zOffset = zOffset + 0.03f;
                discardPile.Add(card);
            }
        }
        // Add used cards to the discard pile
        foreach (string card in discardPile)
        {
            if (deck.Contains(card))
            {
                deck.Remove(card);
            }
        }
        discardPile.Clear();
    }

    // Remove each placed card from the remaining list of cards
    void SolitaireSort()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = i; j < 7; j++)
            {
                bottoms[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
            }
        }
    }

    // Preparing the deck to draw 3 cards at a time
    public void SortDeckIntoTrips()
    {
        trips = deck.Count / 3;
        tripsRemainder = deck.Count % 3;
        deckTrips.Clear();

        // Iterate through sets of 3 cards
        int modifier = 0;
        for (int i = 0; i < trips; i++)
        {
            List<string> myTrips = new List<string>();
            for (int j = 0; j < 3; j++)
            {
                myTrips.Add(deck[j + modifier]);
            }
            deckTrips.Add(myTrips);
            modifier = modifier + 3;
        }
        // If less than 3 cards remain
        if (tripsRemainder != 0)
        {
            List<string> myRemainders = new List<string>();
            modifier = 0;
            for (int k = 0; k < tripsRemainder; k++)
            {
                myRemainders.Add(deck[deck.Count - tripsRemainder + modifier]);
                modifier++;
            }
            deckTrips.Add(myRemainders);
            trips++;
        }
        deckLocation = 0;
    }

    // Draw 3 cards from the deck
    public void DealFromDeck()
    {
        // Add remaining cards to discard pile
        foreach (Transform child in deckButton.transform)
        {
            // If it's a card
            if (child.CompareTag("Card"))
            {
                deck.Remove(child.name);
                discardPile.Add(child.name);
                Destroy(child.gameObject);
            }
        }
        
        if (deckLocation < trips)
        {
            // Draw 3 new cards
            tripsOnDisplay.Clear();
            float xOffset = 2.5f;
            float zOffset = -0.2f;

            foreach (string card in deckTrips[deckLocation])
            {
                GameObject newTopCard = Instantiate(cardPrefab, new Vector3(deckButton.transform.position.x + xOffset, deckButton.transform.position.y, deckButton.transform.position.z + zOffset), Quaternion.identity, deckButton.transform);
                xOffset = xOffset + 0.5f;
                zOffset = zOffset - 0.2f;
                newTopCard.name = card;
                tripsOnDisplay.Add(card);
                newTopCard.GetComponent<Selectable>().faceUp = true;
            }
            deckLocation++;
        }
        else
        {
            // Restack the deck
            RestackTopDeck();
        }
    }

    // Deck restacking method
    void RestackTopDeck()
    {
        foreach (string card in discardPile)
        {
            deck.Add(card);
        }
        discardPile.Clear();
        SortDeckIntoTrips();
    }
}
