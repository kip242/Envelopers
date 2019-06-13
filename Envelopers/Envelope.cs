using System;
using System.Collections.Generic;
using System.Text;

namespace Envelopers
{
	public class Envelope
	{
		public string Name { get; set; }
		public double Amount { get; set; }

		public Envelope()
		{
		}
		public Envelope(string name, double amt)
		{
			Name = name;
			Amount = amt;
		}
	}
}
