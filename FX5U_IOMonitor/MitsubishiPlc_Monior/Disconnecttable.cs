using FX5U_IOMonitor.Config;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FX5U_IOMonitor.MitsubishiPlc_Monior
{
    public class Disconnecttable
    {

        public static DataTable GetDisconnectEvent(string machineName)
        {
            string sql = @"
            SELECT 
                ""StartTime"" AS ""EventTime"",
                '斷線' AS ""EventType""
            FROM public.""DisconnectRecords""
            WHERE ""ConnectOriginate"" = @machine

            UNION ALL

            SELECT 
                ""EndTime"" AS ""EventTime"",
                '重新連線' AS ""EventType""
            FROM public.""DisconnectRecords""
            WHERE ""ConnectOriginate"" = @machine
                AND ""EndTime"" IS NOT NULL

            ORDER BY ""EventTime"";";

            string connString =
                $"Host={DbConfig.Local.IpAddress};Port={DbConfig.Local.Port};Database=element;Username={DbConfig.Local.UserName};Password={DbConfig.Local.Password}";

            using var conn = new NpgsqlConnection(connString);
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("machine", machineName);

            using var da = new NpgsqlDataAdapter(cmd);
            var raw = new DataTable();
            da.Fill(raw);

            // 輸出表（只有兩欄，時間已是 24h 字串）
            var dt = new DataTable();
            dt.Columns.Add("時間", typeof(string));
            dt.Columns.Add("機台狀態", typeof(string));

            foreach (DataRow r in raw.Rows)
            {
                if (r["EventTime"] is DateTime t)
                {
                    // 若 DB 欄位是 timestamptz，Npgsql 通常給 Kind=Utc；timestamp without time zone 可能是 Unspecified
                    var local = (t.Kind == DateTimeKind.Unspecified)
                        ? DateTime.SpecifyKind(t, DateTimeKind.Utc).ToLocalTime()
                        : t.ToLocalTime();

                    dt.Rows.Add(local.ToString("yyyy/MM/dd HH:mm:ss"), r["EventType"]?.ToString());
                }
                else
                {
                    // 非 DateTime 的保底處理
                    dt.Rows.Add(r["EventTime"]?.ToString(), r["EventType"]?.ToString());
                }
            }

            return dt;
        }
    }
}
