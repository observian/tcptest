using System.Collections.Generic;
using System.ComponentModel;

namespace TelnetTest.Models
{
	public class Request
	{
		private int _timeout = 1;
		public List<Resource> Resources { get; set; }
		
		/// <summary>
		/// Anything less than or equal to zero will be set to 1 second
		/// </summary>
		[DefaultValue(1)]
		
		public int Timeout 
		{
			get
			{
				if (_timeout <= 0)
				{
					_timeout = 1;
				}
				return _timeout;

			}
			set => _timeout = value;
		}
	}
}