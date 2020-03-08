namespace Librarian
{
    public class SingleInteraction : InteractableItemBody
    {

        public override bool Activate(Character character)
        {
            if (!character) return false;

            Deactivate(character);
            //return character.PickItem(new BookItem(null, "yes", 16));
            return false;
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