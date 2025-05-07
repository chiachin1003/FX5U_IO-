using FX5U_IOMonitor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX5U_IOMonitor.Models
{
    internal class Test_
    {  // 地址參考值
        void Test()
        {
            using var context = new ApplicationDB();

            var driilIOList = context.Drill_IO.ToList();
            var inputStartAddr = driilIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("X"))
                .Select(a => Convert.ToInt32(a.TrimStart('X'), 16))
                .Min();

            var inputEndAddress = driilIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("X"))
                .Select(a => Convert.ToInt32(a.TrimStart('X'), 16))
                .Max();

            var outputStartAddr = driilIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("Y"))
                .Select(a => Convert.ToInt32(a.TrimStart('Y'), 16))
                .Min();

            var outputEndAddress = driilIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("Y"))
                .Select(a => Convert.ToInt32(a.TrimStart('Y'), 16))
                .Max();

            int input_index = (int)Math.Ceiling((inputEndAddress - inputStartAddr + 1) / 256.0);

            int output_index = (int)Math.Ceiling((outputEndAddress - outputStartAddr + 1) / 256.0);


            if (input_index > 1)
            {
                var inputSplitPoints = Enumerable.Range(1, input_index - 1)
                    .Select(i => inputStartAddr + i * 256)
                    .Where(addr => addr <= inputEndAddress)
                    .ToList();

                foreach (var point in inputSplitPoints)
                {
                    MessageBox.Show($"X{Convert.ToString(point, 16).PadLeft(3, '0')}");
                }
            }

            if (output_index > 1)
            {
                var outputSplitPoints = Enumerable.Range(1, output_index - 1)
                    .Select(i => outputStartAddr + i * 256)
                    .Where(addr => addr <= outputEndAddress)
                    .ToList();

                foreach (var point in outputSplitPoints)
                {
                    MessageBox.Show($"Y{Convert.ToString(point, 16).PadLeft(3, '0')}");
                }
            }

        }
        void test2()
        {
            using var context = new ApplicationDB();

            var SawingIOList = context.Sawing_IO.ToList();
            var inputStartAddr = SawingIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("X"))
                .Select(a => Convert.ToInt32(a.TrimStart('X'), 8))
                .Min();

            var inputEndAddress = SawingIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("X"))
                .Select(a => Convert.ToInt32(a.TrimStart('X'), 8))
                .Max();

            var outputStartAddr = SawingIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("Y"))
                .Select(a => Convert.ToInt32(a.TrimStart('Y'), 8))
                .Min();

            var outputEndAddress = SawingIOList
                .Select(d => d.address)
                .Where(a => a.StartsWith("Y"))
                .Select(a => Convert.ToInt32(a.TrimStart('Y'), 8))
                .Max();

            double input_index = (int)Math.Ceiling((inputEndAddress - inputStartAddr + 1) / 256.0);

            double output_index = (int)Math.Ceiling((outputEndAddress - outputStartAddr + 1) / 256.0);


        }
        void test()
        {
            using var context = new ApplicationDB();

            var SawingIOList = context.Sawing_IO.ToList();
            var inputStartAddr = 0;

            var inputEndAddress = 265;

            var outputStartAddr = 10;

            var outputEndAddress = 700;

            int input_index = (int)Math.Ceiling((inputEndAddress - inputStartAddr + 1) / 256.0);

            int output_index = (int)Math.Ceiling((outputEndAddress - outputStartAddr + 1) / 256.0);

            if (input_index > 1)
            {
                var inputSplitPoints = Enumerable.Range(1, input_index - 1)
                    .Select(i => inputStartAddr + i * 256)
                    .Where(addr => addr <= inputEndAddress)
                    .ToList();

                foreach (var point in inputSplitPoints)
                {
                    MessageBox.Show($"X{Convert.ToString(point, 8).PadLeft(3, '0')}");
                }
            }

            if (output_index > 1)
            {
                var outputSplitPoints = Enumerable.Range(1, output_index - 1)
                    .Select(i => outputStartAddr + i * 256)
                    .Where(addr => addr <= outputEndAddress)
                    .ToList();

                Console.WriteLine("📌 Y 切斷點：");
                foreach (var point in outputSplitPoints)
                {
                    MessageBox.Show($"Y{Convert.ToString(point, 8).PadLeft(3, '0')}");
                }
            }

        }

        private static IOSectionInfo BuildSectionFormatted(string prefix, List<int> addrList, string baseType)
        {
            int start = addrList.Min();
            int end = addrList.Max();
            int blockCount = (int)Math.Ceiling((end - start + 1) / 256.0);

            var section = new IOSectionInfo
            {
                Prefix = prefix,
                StartAddress = start,
                EndAddress = end,
                BlockCount = blockCount
            };

            if (blockCount > 1)
            {
                section.SplitPoints = Enumerable.Range(1, blockCount - 1)
                    .Select(i => start + i * 256)
                    .Where(p => p <= end)
                    .Select(p => Calculate.Format(prefix, p, baseType))
                    .ToList();
            }

            return section;
        }

       


    }
}

   