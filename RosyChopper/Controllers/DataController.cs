using System;
using System.Threading.Tasks;
using RosyChopper.Models;
using Windows.Storage;

namespace RosyChopper.Controllers
{
	public static class DataController
	{
		public static event EventHandler DataLoaded;
		public static bool IsDataLoaded { get; private set; }

		static DataController()
		{
			LoadData();
		}

		private async static void LoadData()
		{
			IsDataLoaded = false;
			await Task.WhenAll(new Task[] { LoadPeople() });
			IsDataLoaded = true;
			RaiseDataLoaded();
		}

		private static void RaiseDataLoaded()
		{
			var handler = DataLoaded;
			if (handler != null)
			{
				handler(null, new EventArgs());
			}
		}

		public static People People { get; private set; }

		private async static Task LoadPeople()
		{
			People = new People(ApplicationData.Current.LocalFolder);
			await People.LoadState();
		}

		public async static void SaveState()
		{
			await Task.WhenAll(new Task[] { People.SaveState() }); 
		}
	}
}
