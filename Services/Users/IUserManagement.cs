using System;
using MedScan.Models;

namespace MedScan.Services
{
	public interface IUserManagement //example of programming to interface
	{
		bool ValidateUserByUsernameAndPassword(User user);
		int CreateUser(User user);
		int DeleteUser(User user);
		List<User> GetUsers(); //List<T>
		bool CheckIfUserIsPresentInSystem(User user);
	}
}

