using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokkAndOdin.Models
{
	public interface ITimeLineItem
	{
		DateTime ItemDateTime {get;}
		string Description {get;}
	}
}
