namespace MonsterCardTrading.Models
{
    public class Stack
    {
        public List<Card> Cards { get; set; }

        public Stack()
        {
            Cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }
        public void RemoveCard(Card card)
        {
            Cards.Remove(card);
        }
    }
}
