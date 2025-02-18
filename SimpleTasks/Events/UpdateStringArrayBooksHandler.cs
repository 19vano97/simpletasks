using System;
namespace SimpleTasks.Events
{
	public class UpdateStringArrayUsersHandler
    {
		private IDataStore _data;

		public UpdateStringArrayUsersHandler(IDataStore data)
		{
			_data = data;
		}

		public void Subscribe()
		{
			_data.UpdateStringsUsers += HandleUpdateStringsBooks;
        }

        public void Unubscribe()
        {
            _data.UpdateStringsUsers -= HandleUpdateStringsBooks;
        }

        private void HandleUpdateStringsBooks(object sender)
        {
			_data.UpdateStringUsers(this);
        }
    }
}

