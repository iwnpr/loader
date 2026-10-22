using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Xml.Linq;

namespace loader_5_0_lib.DataBase
{
    public interface IBureauData
    {
        XElement? GetDownloadInfo(long uploadKeyId);
        XElement? GetCountEntity(long uploadKeyId);
        List<ulong> GetSendOrders(long uploadKeyId);
    }

    public class BureauData : IBureauData
    {
        private readonly IConfiguration _config;
        private readonly ILogger<BureauData> _logger;
        private readonly string _configSection = "BureauData";
        private readonly string _conn;
        private readonly string _schema;

        public BureauData(IConfiguration config, ILogger<BureauData> logger)
        {
            _config = config;
            _logger = logger;
            _conn = _config.GetConnectionString(_configSection) ?? string.Empty;
            _schema = _config.GetSection(_configSection).GetValue<string>("SchemaName") ?? string.Empty;
        }

        public XElement? GetCountEntity(long uploadKeyId)
        {
            string _procedure = _config.GetSection(_configSection).GetValue<string>(nameof(GetCountEntity)) ?? string.Empty;

            _logger.LogDebug("execute: {FuncName} procedure: {SqlProcedure}", nameof(GetCountEntity), _procedure);

            using var pgconn = new NpgsqlConnection(_conn);

            string pgcmd = $"SELECT {_schema}.{_procedure}(@upload_key_id)";

            using var cmd = new NpgsqlCommand(pgcmd, pgconn);
            cmd.Parameters.Add(new("upload_key_id", uploadKeyId));
            pgconn.Open();

            using var reader = cmd.ExecuteReader();

            string res = string.Empty;

            while (reader.Read())
            {
                res = reader.IsDBNull(reader.GetOrdinal(_procedure)) ? string.Empty : reader.GetString(reader.GetOrdinal(_procedure));
                break;
            }

            return string.IsNullOrEmpty(res) ? null : XElement.Parse(res);

        }
        public XElement? GetDownloadInfo(long uploadKeyId)
        {
            string _procedure = _config.GetSection(_configSection).GetValue<string>(nameof(GetDownloadInfo)) ?? string.Empty;

            _logger.LogDebug("execute: {FuncName} procedure: {SqlProcedure}", nameof(GetDownloadInfo), _procedure);

            string? res = null;

            if (string.IsNullOrEmpty(_procedure))
            {
                string errMsg = $"procedure {nameof(GetDownloadInfo)} not found in config";
                throw new NotImplementedException(errMsg);
            }

            using var pgconn = new NpgsqlConnection(_conn);

            string pgcmd = $"SELECT {_schema}.{_procedure}(@upload_key_id)";

            using var cmd = new NpgsqlCommand(pgcmd, pgconn);
            cmd.Parameters.Add(new("upload_key_id", uploadKeyId));
            pgconn.Open();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                res = reader.IsDBNull(0) ? null : reader.GetString(0);
                break;
            }
            return res != null ? XElement.Parse(res) : null;
        }
        public List<ulong> GetSendOrders(long uploadKeyId)
        {
            using var pgconn = new NpgsqlConnection(_conn);
            string _tableName = "tp_download_info";

            string pgcmd = $"SELECT order_num FROM {_schema}.{_tableName} WHERE upload_id = @upload_key_id";

            using var cmd = new NpgsqlCommand(pgcmd, pgconn);
            cmd.Parameters.Add(new("upload_key_id", uploadKeyId));
            pgconn.Open();

            using var reader = cmd.ExecuteReader();

            List<ulong> res = new();

            while (reader.Read())
            {
                res.Add(Convert.ToUInt64(reader.GetInt64(reader.GetOrdinal("order_num"))));
            }
            return res;
        }
    }
}
