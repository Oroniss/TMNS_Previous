namespace RLEngine.Entities.EntityInterfaces
{
	public interface ITrait
	{
		bool HasTrait(Trait trait);
		void AddTrait(Trait trait);
		void RemoveTrait(Trait trait);
	}
}
