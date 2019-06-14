using System;
using System.Collections.Generic;
using System.Text;

namespace Envelopers
{
	public class Envelope
	{
		public DateTime Date { get; set; }
		public string Name { get; set; }
		public double Amount { get; set; }

		public Envelope()
		{
		}
		public Envelope(DateTime date, string name, double amt)
		{
			Date = date;
			Name = name;
			Amount = amt;
		}
	}
}
