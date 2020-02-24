namespace Librarian
{
    public class Chair : Interactable
    {
        public override bool StartUsing(Character character)
        {
            if (IsBeingUsed) return false;
            if (!character) return false;

            //bool sitting = !character.ExecuteBehavior(Behavior.Sit);

            IsBeingUsed = true;
            return true;
        }
    }
}