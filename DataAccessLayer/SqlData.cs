using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Classe interna (somente vista dentro deste projeto (DAL)) que
    /// contém dados a respeito da base que estamos utilizando
    /// </summary>
    internal class SqlData
    {
        public static string ConnectionString
        {
            get { return @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=XXXLOCADORATESTE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; }
        }
    }
}
