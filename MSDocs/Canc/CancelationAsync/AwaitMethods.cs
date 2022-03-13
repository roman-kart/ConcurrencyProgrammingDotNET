using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CancelationAsync
{
	class AwaitMethods
	{
		public static async Task WriteSomethingAsync()
		{
			for (int i = 0; i < 100; i++)
			{
				Console.WriteLine($"Step{i}");
				await Task.Delay(1000);
			}
		}
	}
}
