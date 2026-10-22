using loader_5_0_lib.Classes;
using loader_5_0_lib.Service.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace loader_5_0_lib.Service.Implementation
{
    public class BureauWebUi : IBureauWebUi
    {
        private readonly ILogger<IBureauWebUi> _logger;
        private readonly IConfiguration _config;
        private readonly IAppParam _appParam;
        private readonly string _constr;
        private readonly string _schema;
        private readonly string _database;
        class ActiveUploadParam
        {
            public long KeyId { get; set; }
            public int StatusId { get; set; }
            public string UploadStrXml { get; set; }
            public string UploadResultXML { get; set; }
            public int PriorityLoad { get; set; }
            public int LegalIdDayPos { get; set; }
        }

        public BureauWebUi(IConfiguration config, ILogger<IBureauWebUi> logger, IAppParam appParam)
        {
            _logger = logger;
            _config = config;
            _appParam = appParam;

            _schema = _config.GetSection("DataBase").GetSection("SchemaName").Get<string>() ?? string.Empty;
            _database = _config.GetSection("DataBase").GetSection("DataBaseName").Get<string>() ?? string.Empty;
            _constr = _config.GetConnectionString("BureauWebUi") ?? string.Empty;
        }

        public UploadPackageInfoExt? GetPreviousPackages(long legalPersonId, DateTime dateDoc, DateTime created, string outgoingDocDate, string outgoingDocNumber)
        {
            List<UploadPackageInfo> ret = [];

            using SqlConnection conn = new(_constr);
            using SqlCommand cmd = conn.CreateCommand();
            {
                string _procName = GetProcName(nameof(GetPreviousPackages));

                cmd.CommandText = $"EXEC " +
                    $"[{_database}].[{_schema}].[{_procName}] @legalPersonId, @created, @dateDoc, @outgoingDocNumber, @outgoingDocDate";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@legalPersonId", legalPersonId);
                cmd.Parameters.AddWithValue("@created", created);
                cmd.Parameters.AddWithValue("@dateDoc", dateDoc);
                cmd.Parameters.AddWithValue("@outgoingDocDate", outgoingDocDate);
                cmd.Parameters.AddWithValue("@outgoingDocNumber", outgoingDocNumber);

                cmd.CommandTimeout = 0;

                long dbUploadKeyId;
                string dbUploadId;
                long dbLegalPersonId;
                string dbFileName;
                string dbOutgoingDocDate;
                string dbOutgoingDocNumber;

                conn.Open();
                using var reader01 = cmd.ExecuteReader();
                {
                    while (reader01.Read())
                    {
                        dbUploadKeyId = reader01.GetInt64(reader01.GetOrdinal("keyId"));
                        dbUploadId = reader01.GetString(reader01.GetOrdinal("id"));
                        dbLegalPersonId = reader01.GetInt64(reader01.GetOrdinal("legalPersonId"));
                        dbFileName = reader01.GetString(reader01.GetOrdinal("fileName"));
                        dbOutgoingDocDate = reader01.GetString(reader01.GetOrdinal("outgoingDocDate"));
                        dbOutgoingDocNumber = reader01.GetString(reader01.GetOrdinal("outgoingDocNumber"));

                        return new(dbUploadKeyId, dbUploadId, dbLegalPersonId, dbFileName, dbOutgoingDocDate, dbOutgoingDocNumber);
                    }

                    reader01.Close();
                    conn.Close();

                    return null;
                }
            }
        }

        public void GetTickets(long uploadKeyId)
        {
            _logger.LogDebug($"Executing {nameof(GetTickets)}");

            string uploadingXML = string.Empty;

            using SqlConnection conn = new(_constr);
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"select UploadingXml from dbo.te_requestsOfUploadPostgresNew where keyId = @uploadKeyId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@uploadKeyId", uploadKeyId);
            cmd.CommandTimeout = 0;
            conn.Open();
            using var reader01 = cmd.ExecuteReader();
            while (reader01.Read())
            {
                uploadingXML = reader01.GetString(reader01.GetOrdinal("uploadingXML"));
            }
        }

        public void UpdateDocStatus(long keyId, int statusId, string? sReceipt, string? sAcceptance)
        {
            string _procName = string.Empty;

            using SqlConnection conn = new(_constr);
            using SqlCommand cmd = conn.CreateCommand();
            {
                conn.Open();

                _procName = _config.GetSection("DataBase").GetSection("proc_setUploadProcess").Get<string>() ?? "not found proc_setUploadProcess";

                cmd.CommandText = $"EXEC " +
                    $"[{_database}].[{_schema}].[{_procName}] @keyId, @inProcess";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@keyId", keyId);
                cmd.Parameters.AddWithValue("@inProcess", 0);

                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();

                _procName = _config.GetSection("DataBase").GetSection("proc_setUploadStatus").Get<string>() ?? "not found proc_setUploadStatus";

                cmd.CommandText = $"EXEC " +
                    $"[{_database}].[{_schema}].[{_procName}] @keyId, @statusId";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@keyId", keyId);
                cmd.Parameters.AddWithValue("@statusId", statusId);

                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();

                if (!string.IsNullOrEmpty(sReceipt))
                {
                    _procName = _config.GetSection("DataBase").GetSection("proc_setUploadRequestRegResult").Get<string>() ?? "not found proc_setUploadRequestRegResult";

                    cmd.CommandText = $"EXEC " +
                        $"[{_database}].[{_schema}].[{_procName}] @keyId, @regResultXML";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@keyId", keyId);
                    cmd.Parameters.AddWithValue("@regResultXML", sReceipt);

                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();
                }

                if (!string.IsNullOrEmpty(sAcceptance))
                {
                    _procName = _config.GetSection("DataBase").GetSection("proc_setUploadRequestUploadResult").Get<string>() ?? "not found proc_setUploadRequestUploadResult";

                    cmd.CommandText = $"EXEC " +
                        $"[{_database}].[{_schema}].[{_procName}] @keyId, @uploadResult";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@keyId", keyId);
                    cmd.Parameters.AddWithValue("@uploadResult", sAcceptance);

                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public ActiveUpload? GetActiveUploadRequest()
        {
            string _procName = string.Empty;

            using SqlConnection conn = new(_constr);
            using SqlCommand cmd = conn.CreateCommand();
            {
                _procName = GetProcName(nameof(GetActiveUploadRequest));

                cmd.CommandText = $"EXEC " +
                    $"[{_database}].[{_schema}].[{_procName}]";

                cmd.CommandTimeout = 0;

                long uploadKeyId = default;
                DateTime created = default;
                int legalPersonId = default;
                string sourceItn = default;
                string sourcePsrn = default;
                int legalPosition = default;
                string sUploadingXML = default;

                conn.Open();
                using var reader01 = cmd.ExecuteReader();
                {
                    while (reader01.Read())
                    {
                        uploadKeyId = reader01.GetInt64(reader01.GetOrdinal("keyId"));
                        created = reader01.GetDateTime(reader01.GetOrdinal("created"));
                        legalPersonId = reader01.GetInt32(reader01.GetOrdinal("legalPersonId"));
                        sourceItn = reader01.GetString(reader01.GetOrdinal("sourceItn"));
                        sourcePsrn = reader01.GetString(reader01.GetOrdinal("sourcePsrn"));
                        legalPosition = reader01.GetInt32(reader01.GetOrdinal("legalPosInDay"));
                        sUploadingXML = reader01.GetString(reader01.GetOrdinal("uploadingXML"));
                        break;
                    }

                    reader01.Close();
                    conn.Close();

                    created =  DateTime.SpecifyKind(created, DateTimeKind.Local);

                    return uploadKeyId > 0
                        ? string.IsNullOrEmpty(sUploadingXML) ? null : new ActiveUpload(uploadKeyId, created, sourceItn, sourcePsrn, legalPosition, XElement.Parse(sUploadingXML))
                        : null;
                }
            }
        }

        public void UpdateUploadingXml(long keyId, XElement uploadingXml)
        {
            string _procName = GetProcName(nameof(UpdateUploadingXml));

            using SqlConnection conn = new(_constr);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $"EXEC " +
                $"[{_database}].[{_schema}].[{_procName}] @keyId, @uploadingXml";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@keyId", keyId);
            cmd.Parameters.AddWithValue("@uploadingXml", uploadingXml.ToString());

            conn.Open();

            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private string GetProcName(string configProcName)
        {
            return _config.GetSection("DataBase").GetSection(configProcName).Get<string>();
        }

        public void UpdateNotificationOfReceipt(long uploadKeyId, XElement sReceipt)
        {
            string? _procedure = GetProcName(nameof(UpdateNotificationOfReceipt));
            using SqlConnection conn = new(_constr);
            using SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $"EXEC [{_database}].[{_schema}].[{_procedure}] @uploadKeyId, @sReceipt";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@uploadKeyId", uploadKeyId);
            cmd.Parameters.AddWithValue("@sReceipt", sReceipt.ToString());
            cmd.CommandTimeout = 0;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }
    }
}