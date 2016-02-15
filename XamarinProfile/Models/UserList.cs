using System;

namespace XamarinProfile
{
	public class UserList
	{
		public UserList(string name, string location, string profilePicture)
		{
			this.Name = name;
			this.Location = location;
			this.ProfilePicture = profilePicture;
		}

	public string Name { private set; get; }

	public string Location { private set; get; }

	public string ProfilePicture { private set; get; }
	}
}
