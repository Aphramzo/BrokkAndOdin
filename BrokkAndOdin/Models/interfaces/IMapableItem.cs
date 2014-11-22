using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace BrokkAndOdin.Models
{
	public interface IMapableItem
	{
		GeoCoordinate Coords { get; }
	}
}
