using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazellaMobile.Interfaces
{
    public interface ISQLConnection
    {
        SQLite.SQLiteConnection GetConnection();
    }
}
