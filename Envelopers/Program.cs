using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Envelopers
{
	class Program
	{
		public static List<Envelope> Envelopes;
		static void Main(string[] args)
		{
			Envelopes = ProcessFile("envelopes.csv");
			Intro(Envelopes);
			Console.WriteLine("\n\nWhat would you like to do?\n" +
				"1. Add Funds To An Envelope\n" +
				"2. Delete Funds From An Envelope");
			var input = Console.ReadLine();
			Routing(input, Envelopes);

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
			
			Console.WriteLine("Current Envelope Balances");
			Console.WriteLine("Envelope\t\tAmount");
			Console.WriteLine("------------------------------");
			foreach (var e in env)
			{
				Console.WriteLine($"{e.Name}\t|\t${e.Amount}");
			}
		}

		public static void Routing(string choice, List<Envelope> envList)
		{
			switch (choice)
			{
				case "1":
					Console.WriteLine("Which Envelope would you like to add money to?");
					string envName = Console.ReadLine();
					AddMoney(envName, envList);
					break;

				//case "2":
				//	DeleteMoney();


			}

		}

		public static void AddMoney(string env, List<Envelope> envList)
		{
			var query =
				from envs in envList
				.Where(e => e.Name == env)
				select envs;

			foreach (var envelope in query)
			{
				Console.WriteLine($"You Selected {envelope.Name} and it currently has ${envelope.Amount}");
			}
			

		}
	}
}
