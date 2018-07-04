// Tidied for version 0.2.

namespace RLEngine.Entities.EntityInterfaces
{
	public interface ITrait
	{
		bool HasTrait(Trait trait);
		void AddTrait(Trait trait);
		void RemoveTrait(Trait trait);
	}
}
