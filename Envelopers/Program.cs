using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Envelopers
{
	class Program
	{
		public static List<Envelope> Envelopes;
		static void Main(string[] args)
		{
			Envelopes = ProcessFile("envelopes.csv");
			Intro(Envelopes);
			
			int input;
			do
			{
				Console.WriteLine("\n\nWhat Would You Like To Do?\n" +
				"1. Add Money To An Envelope\n" +
				"2. Spend Money From an Envelope\n" +
				"3. Create a New Envelope\n" +
				"4. Delete an Envelope\n" +
				"5. Exit");
				input = int.Parse(Console.ReadLine());
				Routing(input, Envelopes);
				Envelopes = ProcessFile("envelopes.csv");
				Intro(Envelopes);
			}
			while (input != 5);
		}

		private static List<Envelope> ProcessFile(string filePath)
		{
			var query =
				File.ReadAllLines(filePath)
				.Where(l => l.Length > 1)
				.ToEnv();
				
			return query.ToList();
		}

		public static void Intro(List<Envelope> env)
		{
			Console.Clear();
			Console.WriteLine("Current Envelope Balances");
			Console.WriteLine("\n{0,-30} {1, -30} {2,20}", "Date Modified", "Envelope", "Amount");
			Console.WriteLine("----------------------------------------------------------------------------------");
			var query =
				from envelope in env
				group envelope by envelope.Name into groups
				orderby groups.Key
				select groups;

			foreach (var item in query)
			{
				foreach ( var envelope in item.OrderByDescending(e => e.Date).Take(1))
				{
					Console.WriteLine("{0,-30} {1,-30} {2,20}", $"{envelope.Date.ToShortDateString()}", $"{envelope.Name}", $"{envelope.Amount:C}");
				}
			}
		}

		public static void Routing(int choice, List<Envelope> envList)
		{
			string envName;
			switch (choice)
			{
				case 1:
					Console.WriteLine("Which Envelope would you like to add money to?");
					envName = Console.ReadLine();
					AddMoney(envName, envList);
					break;

				case 2:
					Console.WriteLine("Which Envelope did you spend money out of?");
					envName = Console.ReadLine();
					DeleteMoney(envName, envList);
					break;

				case 3:
					CreateEnvelope();
					break;
			}
		}

		public static void AddMoney(string env, List<Envelope> envList)
		{
			var query =
				from envs in envList
				.Where(e => e.Name == env)
				select envs;

			string envName = query.FirstOrDefault().Name;
			DateTime date = DateTime.Now;

			foreach (var envelope in query.OrderByDescending(e => e.Date).Take(1))
			{
				Console.WriteLine($"You Selected {envelope.Name} and it currently has ${envelope.Amount}");
			}

			Console.WriteLine("\nHow much would you like to add to this envelope?");
			var addAmt = Console.ReadLine();

			var query2 =
				from envs in envList
				.Where(e => e.Name == env)
				select envs.Amount;

			double amount = query2.LastOrDefault();

			double saveAmt = double.Parse(addAmt) + amount;

			StringBuilder sb = new StringBuilder();
			sb.Append(date);
			sb.Append(",");
			sb.Append(envName);
			sb.Append(",");
			sb.Append(saveAmt);
			List<String> AddTo = new List<string>();
			AddTo.Add(sb.ToString());
			File.AppendAllLines("envelopes.csv", AddTo);

			Console.WriteLine("There is currently {0} in the envelope", saveAmt);

			Console.WriteLine("Press any key to go back to the main screen");
			Console.ReadKey();
		}

		public static void DeleteMoney(string env, List<Envelope> envList)
		{
			var query =
				from envs in envList
				.Where(e => e.Name == env)
				select envs;

			string envName = query.FirstOrDefault().Name;
			DateTime date = DateTime.Now;

			foreach (var envelope in query.OrderByDescending(e => e.Date).Take(1))
			{
				Console.WriteLine($"You Selected {envelope.Name} and it currently has ${envelope.Amount}");
			}

			Console.WriteLine("\nHow much did you spend out of this envelope?");
			var addAmt = Console.ReadLine();

			var query2 =
				from envs in envList
				.Where(e => e.Name == env)
				select envs.Amount;

			double amount = query2.LastOrDefault();

			double saveAmt = amount - double.Parse(addAmt) ;

			StringBuilder sb = new StringBuilder();
			sb.Append(date);
			sb.Append(",");
			sb.Append(envName);
			sb.Append(",");
			sb.Append(saveAmt);
			List<String> AddTo = new List<string>();
			AddTo.Add(sb.ToString());
			File.AppendAllLines("envelopes.csv", AddTo);

			Console.WriteLine("There is currenty {0} left in the envelope", saveAmt);

			Console.WriteLine("Press any key to go back to the main screen");
			Console.ReadKey();
		}

		public static void CreateEnvelope()
		{
			
			List<string> AddTo = new List<string>();
			StringBuilder sb = new StringBuilder();
			
			Console.Clear();
			Console.WriteLine("Creating A New Envelope");
			Console.WriteLine("-----------------------");

			Console.WriteLine("\nWhat would you like to name the envelope");
			sb.Append(DateTime.Now);
			sb.Append(",");
			sb.Append(Console.ReadLine());
			sb.Append(",");
			sb.Append(0.00);
			AddTo.Add(sb.ToString());
			File.AppendAllLines("envelopes.csv", AddTo);

			Console.WriteLine("Press any key to go back to the main screen");
			Console.ReadKey();



		}
	}
}
