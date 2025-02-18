using System;
namespace SimpleTasks.Events
{
	public class UpdateStringArrayBooksHandler
	{
		private IDataStore _data;

		public UpdateStringArrayBooksHandler(IDataStore data)
		{
			_data = data;
		}

		public void Subscribe()
		{
			_data.UpdateStringsBooks += HandleUpdateStringsBooks;
        }

        public void Unsubscribe()
        {
            _data.UpdateStringsBooks -= HandleUpdateStringsBooks;
        }

        private void HandleUpdateStringsBooks(object sender)
        {
			_data.UpdateStringBooks(this);
        }
    }
}

