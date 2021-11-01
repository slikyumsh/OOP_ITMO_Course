using System.Collections.Generic;
using System.Net.Sockets;
using System.Xml.Linq;

namespace Backups
{
    public interface IRepository
    {
        public void SavePoint(RestorePoint point);
    }
}