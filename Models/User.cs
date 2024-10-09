namespace MonsterCardTrading.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Stack CardStack { get; set; }
        public Deck CardDeck { get; set;}

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            CardStack = new Stack();
        }

        public void AddCardToStash(Card card)
        {
            CardStack.AddCard(card);
        }

        public void RemoveCardFromStash(Card card)
        {
            CardStack.RemoveCard(card);
        }

        public void AddCardToDeck(Card card)
        {
            CardDeck.AddCard(card);
        }

        public void RemoveCardFromDeck(Card card)
        {
            CardDeck.RemoveCard(card);
        }
    }

}
