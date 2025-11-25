using FX5U_IOMonitor.Config;
using FX5U_IOMonitor.Models;
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
            string lang = LanguageManager.Currentlanguge;

            // SQL 不再含中文，改用事件 Key
            string sql = @"
                SELECT 
                    ""StartTime"" AS ""EventTime"",
                    'Connect_Disconnect' AS ""EventType""        -- 🔥 斷線事件
                FROM public.""DisconnectRecords""
                WHERE ""ConnectOriginate"" = @machine

                UNION ALL

                SELECT 
                    ""EndTime"" AS ""EventTime"",
                    'Connect_Reconnect' AS ""EventType""           -- 🔥 重新連線事件
                FROM public.""DisconnectRecords""
                WHERE ""ConnectOriginate"" = @machine
                    AND ""EndTime"" IS NOT NULL

                ORDER BY ""EventTime"";
            ";

            string connString =
                $"Host={DbConfig.Local.IpAddress};Port={DbConfig.Local.Port};Database=element;Username={DbConfig.Local.UserName};Password={DbConfig.Local.Password}";

            using var conn = new NpgsqlConnection(connString);
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("machine", machineName);

            using var da = new NpgsqlDataAdapter(cmd);
            var raw = new DataTable();
            da.Fill(raw);


            // DataTable 欄位名稱也改成可多語
            //------------------------------------------------------------------
            var dt = new DataTable();
            dt.Columns.Add(LanguageManager.Translate("Connect_title_Time"), typeof(string));
            dt.Columns.Add(LanguageManager.Translate("Connect_title_MachineStatus"), typeof(string));


            //--------- 依事件 Key -> 語系表轉換
            //------------------------------------------------------------------
            foreach (DataRow r in raw.Rows)
            {
                string eventKey = r["EventType"]?.ToString() ?? "";
                string eventText = LanguageManager.Translate(eventKey);       // 直接讀語系表

                if (r["EventTime"] is DateTime t)
                {
                    var local = (t.Kind == DateTimeKind.Unspecified)
                        ? DateTime.SpecifyKind(t, DateTimeKind.Utc).ToLocalTime()
                        : t.ToLocalTime();

                    dt.Rows.Add(local.ToString("yyyy/MM/dd HH:mm:ss"), eventText);
                }
                else
                {
                    dt.Rows.Add(r["EventTime"]?.ToString(), eventText);
                }
            }

            return dt;
        }
    }
}
