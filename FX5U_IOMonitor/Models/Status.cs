using FX5U_IOMonitor.Data;
using Microsoft.EntityFrameworkCore;
using SLMP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace FX5U_IOMonitor.Models
{
    internal class Status
    {

        private static List<ushort> samples = new List<ushort>();
        public static Swing_Status update_swing_Status(SlmpClient plcClient ) 
        {
            Swing_Status swingstatus = new Swing_Status();
            ushort Sawing_V = plcClient.ReadWordDevice("R20004"); //設定電壓值

            ushort MotorCurrent = plcClient.ReadWordDevice("R20000");
            ushort CuttingSpeed = plcClient.ReadWordDevice("R20002");
            ushort Oil_Pressure = plcClient.ReadWordDevice("R20008");
            ushort Remaining_Dutting_tools = plcClient.ReadWordDevice("R20016");
            
            ushort Sawing_countdown_time = plcClient.ReadWordDevice("R20018");
            string Sawing_time = ConvertSecondsToDHMS(Sawing_countdown_time);

            ushort Sawing_current = plcClient.ReadWordDevice("D2004"); //當前電流值平均

            double avg = AddAverageSample(samples, Sawing_current, 240);    //(每秒讀取一次數據，每2min平均一次後要寫回去)



            ushort time = plcClient.ReadWordDevice("R20014");
            string Sawingtotaltime = ConvertSecondsToDHMS(time);

            //plcClient.WriteString("R20014", "00");//總運轉時間
            DBfunction.Set_Machine_string("Sawing_current", avg.ToString("F2"));
            DBfunction.Set_Machine_string("Sawing_voltage", Sawing_V.ToString());

            DBfunction.Set_Machine_string("Sawingcuttingspeed", plcClient.ReadWordDevice("R20002").ToString());
            DBfunction.Set_Machine_string("Sawing_oil_pressure", plcClient.ReadWordDevice("R20006").ToString());
            DBfunction.Set_Machine_string("Sawing_power", plcClient.ReadWordDevice("R20008").ToString());

            DBfunction.Set_Machine_string("Sawing_countdown_time", ConvertSecondsToDHMS(plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Sawing_countdown_time"))));//要換成從資料庫取資料
            DBfunction.Set_Machine_string("Sawing_remain_tools", plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Sawing_remain_tools")).ToString());//要換成從資料庫取資料
            
            DBfunction.Set_Machine_string("Sawing_total_time", ConvertSecondsToDHMS(plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Sawing_total_time"))));//要換成從資料庫取資料



            //swingstatus.motorcurrent = MotorCurrent.ToString();
            //swingstatus.oil_pressure = Oil_Pressure.ToString();
            //swingstatus.cuttingspeed = CuttingSpeed.ToString();
            //swingstatus.avg_mA = avg.ToString("F2");
            //swingstatus.avg_V = Sawing_V.ToString();
            //swingstatus.Runtime = Sawingtotaltime; //總運轉時間，要修改
            //swingstatus.Remaining_Dutting_tools = Remaining_Dutting_tools.ToString();
            //swingstatus.Sawing_countdown_time = Sawing_time.ToString();

           
            return swingstatus;
        
        }
        private static double AddAverageSample(List<ushort> currentSamples, ushort sample, int MaxSamples)
        {
            currentSamples.Add(sample);

            // 維持最大長度
            if (currentSamples.Count > MaxSamples)
            {
                currentSamples.RemoveAt(0); // FIFO 移除最舊的
            }

            // 計算平均
            double averageSample = currentSamples.Average(s => (double)s);
            return averageSample;
        }
        public static void Write_Swing_Status(SlmpClient plcClient)
        {
            //Random rand = new Random();
            //ushort Average = StringToUShort(DBfunction.Get_Machine_string("Sawing_current"));
            //ushort Voltage = StringToUShort(DBfunction.Get_Machine_string("Sawing_voltage"));
            //ushort total_time = (ushort)ParseDHMSStringToSeconds(DBfunction.Get_Machine_string("Sawing_total_time"));

            //Average = (ushort)rand.Next(10, 40);
            //Voltage = (ushort)rand.Next(200, 250);
            //plcClient.WriteWordDevice("R20006", Voltage); //電壓平均
            //plcClient.WriteWordDevice("R20006", Average); //電流平均
            //plcClient.WriteWordDevice("R20010", (ushort)(Average * Voltage)); //消耗功率
            //plcClient.WriteWordDevice("R20012", (ushort)((Average * Voltage * total_time) / 3600000.0)); //累積用電度數


            //ushort time = plcClient.ReadWordDevice("R20014");
            //string Sawingtotaltime = ConvertSecondsToDHMS(time);

            //plcClient.WriteString("R20014", "00");//總運轉時間

            //return swingstatus;

        }
        public static void Write_Drill_Status(SlmpClient plcClient)
        {
            Random rand = new Random();


            ushort Average = StringToUShort(DBfunction.Get_Machine_now_string("Drill_current"));
            ushort Voltage = StringToUShort(DBfunction.Get_Machine_now_string("Drill_voltage"));
            ushort total_time = (ushort)ParseDHMSStringToSeconds(DBfunction.Get_Machine_now_string("Drill_total_Time"));

            DBfunction.Set_Machine_string("Drill_electricity", ((ushort)(Average * Voltage * total_time) / 3600000.0).ToString());//累積用電度數
            DBfunction.Set_Machine_string("Drill_power", ((ushort)(Average * Voltage )).ToString());//累積用電度數

            //ushort Average = StringToUShort(Drillstatus.Current);
            //ushort Voltage = StringToUShort(Drillstatus.Voltage);
            //ushort total_time = (ushort)ParseDHMSStringToSeconds(Drillstatus.Runtime);
            Average = (ushort)rand.Next(10, 40);
            Voltage = (ushort)rand.Next(200, 250);
            plcClient.WriteWordDevice("R20006", Voltage); //電壓平均
            plcClient.WriteWordDevice("R20006", Average); //電流平均
            plcClient.WriteWordDevice("R20010", (ushort)(Average * Voltage)); //消耗功率
            plcClient.WriteWordDevice("R20012", (ushort)((Average * Voltage * total_time) / 3600000.0)); //累積用電度數

            ushort time = plcClient.ReadWordDevice("R20014");
            string Sawingtotaltime = ConvertSecondsToDHMS(time);

            //plcClient.WriteString("R20014", "00");//總運轉時間

            //return Drillstatus;

        }

       
        public static Drill_status update_drill_Status(SlmpClient plcClient)
        {
            DBfunction.Set_Machine_string("Drill_current", plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Drill_current")).ToString());
            DBfunction.Set_Machine_string("Drill_voltage", plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Drill_voltage")).ToString());
            DBfunction.Set_Machine_string("Drill_electricity", plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Drill_electricity")).ToString());

            DBfunction.Set_Machine_string("Drill_power", plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Drill_power")).ToString());//要換成從資料庫取資料

            DBfunction.Set_Machine_string("Drill_servo_usetime", ConvertSecondsToDHMS(plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Drill_servo_usetime"))));
            DBfunction.Set_Machine_string("Drill_spindle_usetime", ConvertSecondsToDHMS(plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Drill_spindle_usetime"))));
            DBfunction.Set_Machine_string("Drill_inverter", ConvertSecondsToDHMS(plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Drill_inverter"))));
            DBfunction.Set_Machine_string("Drill_total_Time", ConvertSecondsToDHMS(plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Drill_total_Time"))));

            DBfunction.Set_Machine_now_number("Drill_origin", plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Drill_origin")));
            DBfunction.Set_Machine_now_number("Drill_loose_tools", plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Drill_loose_tools")));
            DBfunction.Set_Machine_now_number("Drill_measurement", plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Drill_measurement")));
            DBfunction.Set_Machine_now_number("Drill_clamping", plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Drill_clamping")));
            DBfunction.Set_Machine_now_number("Drill_feeder", plcClient.ReadWordDevice(DBfunction.Get_Machine_read_address("Drill_feeder")));

            Drill_status status = new Drill_status();

            //ushort MotorCurrent = plcClient.ReadWordDevice("R20000");
            //ushort CuttingSpeed = plcClient.ReadWordDevice("R20002");
            //ushort Oil_Pressure = plcClient.ReadWordDevice("R20008");
            //ushort Remaining_Dutting_tools = plcClient.ReadWordDevice("R20016");
            //ushort countdown_time = plcClient.ReadWordDevice("R20018"); //會有問題的

            //ushort Servo_drives_usetime = plcClient.ReadWordDevice("D2080");
            //string Servo_usetime = ConvertSecondsToDHMS(Servo_drives_usetime);
            //ushort Spindle_usetime = plcClient.ReadWordDevice("D2082");
            //string spindle_usetime = ConvertSecondsToDHMS(Spindle_usetime);
            //ushort PLC_usetime = plcClient.ReadWordDevice("D2084");
            //string plc_usetime = ConvertSecondsToDHMS(PLC_usetime);
            //ushort Frequency_Converter_usetime = plcClient.ReadWordDevice("D2086");
            //string Converter_usetime = ConvertSecondsToDHMS(Frequency_Converter_usetime);
            //ushort Runtime = plcClient.ReadWordDevice("D2088");
            //string Runtime_machine = ConvertSecondsToDHMS(Runtime);
            //ushort origin = plcClient.ReadWordDevice("D2090");
            //ushort loose_tools = plcClient.ReadWordDevice("D2092");
            //ushort measurement = plcClient.ReadWordDevice("D2094");
            //ushort clamping = plcClient.ReadWordDevice("D2096");
            //ushort feeder = plcClient.ReadWordDevice("D2098");

            //status.Current = plcClient.ReadWordDevice("R20006").ToString();
            //status.Voltage = plcClient.ReadWordDevice("R20006").ToString();
            //status.power = plcClient.ReadWordDevice("R20010").ToString(); 
            //status.du = plcClient.ReadWordDevice("R20012").ToString();
            //status.Servo_drives_usetime = Servo_usetime;
            //status.Spindle_usetime = spindle_usetime;
            //status.PLC_usetime = plc_usetime;
            //status.Frequency_Converter_usetime = Converter_usetime;
            //status.Runtime = Runtime_machine;
            //status.origin = origin;
            //status.loose_tools = loose_tools;
            //status.measurement = measurement;
            //status.clamping = clamping;
            //status.feeder = feeder;

            return status;

        }
        public static SawBand_Status  update_SawBand_Status(SlmpClient plcClient )
        {
            SawBand_Status sawbandstatus = new SawBand_Status();
            ushort []Sawband_Brand = plcClient.ReadWordDevice("R4010",20);
            ushort []Saw_Blade_material = plcClient.ReadWordDevice("R4004", 2);
            ushort []Saw_teeth = plcClient.ReadWordDevice("R20042",2);
            ushort Sawband_speed = plcClient.ReadWordDevice("R20046");
            ushort power = plcClient.ReadWordDevice("R20050"); 
            ushort Maximum_current = plcClient.ReadWordDevice("R20006"); 
            ushort area = plcClient.ReadWordDevice("R20054");
            ushort tension = plcClient.ReadWordDevice("R20058");

            string Sawband_brand = ConvertUShortArrayToAsciiString(Sawband_Brand);
            string Saw_blade_material = ConvertUShortArrayToAsciiString(Saw_Blade_material);

            DBfunction.Set_Machine_string("Sawband_brand",Sawband_brand.ToString());
            DBfunction.Set_Machine_string("Sawblade_teeth", Saw_teeth[0].ToString() + " / " + Saw_teeth[1].ToString());
            DBfunction.Set_Machine_string("Sawblade_material", Saw_blade_material);
            DBfunction.Set_Machine_string("Sawband_speed", Sawband_speed.ToString());
            DBfunction.Set_Machine_string("Sawband_power", power.ToString());
            DBfunction.Set_Machine_string("Sawband_area", area.ToString());
            DBfunction.Set_Machine_string("Sawband_tension",tension.ToString());

            //sawbandstatus.Sawband_brand = Sawband_brand.ToString();
            //sawbandstatus.Saw_teeth = Saw_teeth[0].ToString() + " / " + Saw_teeth[1].ToString();
            //sawbandstatus.Saw_blade_material = Saw_blade_material.ToString();
            //sawbandstatus.Sawband_speed = Sawband_speed.ToString();
            //sawbandstatus.power = power.ToString();
            //sawbandstatus.Maximum_current = Maximum_current.ToString();
            //sawbandstatus.area = area.ToString();

            return sawbandstatus;

        }
        // 將秒數轉換為 日、時、分、秒 的字串
        private static string ConvertSecondsToDHMS(ushort totalSeconds)
        {
            int days = totalSeconds / 86400;
            int hours = (totalSeconds % 86400) / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            return $"{days} 天 {hours} 時 {minutes} 分 {seconds} 秒";
        }
        // 將字串 日、時、分、秒 轉換為秒數
        private static int ParseDHMSStringToSeconds(string dhms)
        {
            int days = 0, hours = 0, minutes = 0, seconds = 0;

            // 使用正則表達式抓出每一個時間單位
            Match match = Regex.Match(dhms, @"(?:(\d+)\s*天)?\s*(?:(\d+)\s*時)?\s*(?:(\d+)\s*分)?\s*(?:(\d+)\s*秒)?");

            if (match.Success)
            {
                if (match.Groups[1].Success) days = int.Parse(match.Groups[1].Value);
                if (match.Groups[2].Success) hours = int.Parse(match.Groups[2].Value);
                if (match.Groups[3].Success) minutes = int.Parse(match.Groups[3].Value);
                if (match.Groups[4].Success) seconds = int.Parse(match.Groups[4].Value);
            }

            return days * 86400 + hours * 3600 + minutes * 60 + seconds;
        }
        // 將數值轉字串 日、時、分
        private static string ConvertMinutesToDHM(ushort totalMinutes)
        {
            int days = totalMinutes / (24 * 60);
            int hours = (totalMinutes % (24 * 60)) / 60;
            int minutes = totalMinutes % 60;

            return $"{days} 天 {hours} 時 {minutes} 分";
        }
        // 將字串 日、時、分轉數值
        private static int ParseDHMStringToSeconds(string dhm)
        {
            int days = 0, hours = 0, minutes = 0;

            // 只處理「天、時、分」格式
            Match match = Regex.Match(dhm, @"(?:(\d+)\s*天)?\s*(?:(\d+)\s*時)?\s*(?:(\d+)\s*分)?");

            if (match.Success)
            {
                if (match.Groups[1].Success) days = int.Parse(match.Groups[1].Value);
                if (match.Groups[2].Success) hours = int.Parse(match.Groups[2].Value);
                if (match.Groups[3].Success) minutes = int.Parse(match.Groups[3].Value);
            }

            return days * 86400 + hours * 3600 + minutes * 60;
        }

        // 顯示當前秒數計算結果
        private static string AddSecondsAndUpdate(ushort orignal_second, ushort secondsToAdd)
        {
            orignal_second += secondsToAdd;
            return ConvertSecondsToDHMS(orignal_second);
        }
        private static ushort StringToUShort(string input)
        {
            if (double.TryParse(input, out double value))
            {
                return (ushort)Math.Round(value); // 轉為最近整數
            }
            else
            {
                throw new ArgumentException("輸入字串無法轉換為數字");
            }
        }
        public static string ConvertUShortArrayToAsciiString(ushort[] data, bool lowFirst = true)
        {
            List<byte> bytes = new List<byte>();
            foreach (ushort word in data)
            {
                byte low = (byte)(word & 0xFF);
                byte high = (byte)(word >> 8);
                if (lowFirst)
                {
                    bytes.Add(low);
                    bytes.Add(high);
                }
                else
                {
                    bytes.Add(high);
                    bytes.Add(low);
                }
            }

            return Encoding.ASCII.GetString(bytes.ToArray()).TrimEnd('\0');
        }
    }
}
