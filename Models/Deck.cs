namespace MonsterCardTrading.Models
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public Deck()
        {
            Cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            if (Cards.Count >= 5)
            {
                throw new InvalidOperationException("Man kann nicht mehr als 5 Karten im Deck haben!");
            }
            Cards.Add(card);
        }

        public void RemoveCard(Card card)
        {
            Cards.Remove(card);
        }
    }
}
