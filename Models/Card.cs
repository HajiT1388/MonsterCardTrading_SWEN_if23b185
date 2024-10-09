namespace MonsterCardTrading.Models
{
    public enum ElementType
    {
        Water,
        Fire,
        Normal
    }

   
    public abstract class Card
    {
        
        public string Name { get;  set; }
        public int Damage { get;  set; }
        public ElementType ElementType { get;  set; }  

        protected Card(string name, int damage, ElementType elementType)
        {
            Name = name;
            Damage = damage;  
            ElementType = elementType;
        }
    }
}
