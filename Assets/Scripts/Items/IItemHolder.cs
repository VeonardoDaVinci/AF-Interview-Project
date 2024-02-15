namespace AFSInterview.Items
{
	public interface IItemHolder
	{
		IItem GetItem(bool disposeHolder);
	}
}