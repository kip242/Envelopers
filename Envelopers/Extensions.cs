using System;
using System.Collections.Generic;
using System.Text;

namespace Envelopers
{
	public static class Extensions
	{
		public static IEnumerable<Envelope> ToEnv(this IEnumerable<string> source)
		{
			foreach (var line in source)
			{
				var columns = line.Split(',');
				yield return new Envelope
				{
					Name = columns[0],
					Amount = double.Parse(columns[1])
				};
			}
		}
	}
}
