
namespace RPG.My.Saving
{
	public interface IMySaveable
	{
		object CaptureState();

		void RestoreState(object state);
	}
}