using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;

namespace RosyChopper.Models
{
	public class People : ObservableCollection<Person>
	{
		private const string FileName = "people.txt";

		private readonly StorageFolder storageFolder;

		public People(StorageFolder storageFolder) : base()
		{
			this.storageFolder = storageFolder;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public bool TryAdd(Person participant)
		{
			if (!this.Contains(participant))
			{
				this.Add(participant);
				return true;
			}

			return false;
		}

		public void AddRange(IEnumerable<Person> people)
		{
			foreach (var person in people)
			{
				this.Add(person);
			}
		}

		internal async Task LoadState()
		{
			var file = await this.storageFolder.CreateFileAsync(FileName, CreationCollisionOption.OpenIfExists);
			var historyString = await FileIO.ReadTextAsync(file);

			if (!string.IsNullOrEmpty(historyString))
			{
				this.AddRange(await JsonConvert.DeserializeObjectAsync<IEnumerable<Person>>(historyString));
			}
		}

		internal async Task SaveState()
		{
			var serializedCollection = await JsonConvert.SerializeObjectAsync(this);

			var file = await this.storageFolder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);

			await FileIO.WriteTextAsync(file, serializedCollection);
		}
	}
}