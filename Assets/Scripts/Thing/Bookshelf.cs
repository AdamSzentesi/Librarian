namespace Librarian
{
    public class Bookshelf : Interactable
    {

        public override bool Activate(Character character)
        {
            if (!character) return false;

            Deactivate(character);
            return character.PickItem(new BookItem(null, "yes", 16));
        }

        public override bool Deactivate(Character character)
        {
            return true;
        }

        public override float GetBonus(Feeling feeling)
        {
            throw new System.NotImplementedException();
        }
    }
}