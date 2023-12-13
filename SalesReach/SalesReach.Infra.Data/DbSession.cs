using Microsoft.Extensions.Options;
using SalesReach.Infra.Data.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SalesReach.Infra.Data
{
    public class DbSession : IDisposable
    {
        private readonly SettingsDataBase _settingsDataBase;

        public DbSession(IOptions<SettingsDataBase> options)
        {
            _settingsDataBase = options.Value;
            Connection = new SqlConnection(_settingsDataBase.ConnectionString);
            Connection.Open();
        }

        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public void Dispose() => Connection.Dispose();
    }
}
