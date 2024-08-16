using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talaria.Models;
using OfficeOpenXml;

namespace Talaria.Services
{
    public class SensorDataForExcel
    {
        private string excelFilePath;
        public SensorDataForExcel(string filePath) 
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            excelFilePath = filePath;
        }
        public void AddSensorData(SensorData sensorData)
        {
            FileInfo fileInfo = new FileInfo(excelFilePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Count == 0
                    ? package.Workbook.Worksheets.Add("SensorData")
                    : package.Workbook.Worksheets[0];

                int currentRow = worksheet.Dimension == null ? 1 : worksheet.Dimension.End.Row + 1;

                if (currentRow == 1)
                {
                    // Başlıkları yaz
                    worksheet.Cells[1, 1].Value = "MyDataId";
                    worksheet.Cells[1, 2].Value = "packageNumber";
                    worksheet.Cells[1, 3].Value = "satelliteStatus";
                    worksheet.Cells[1, 4].Value = "ErrorCode";
                    worksheet.Cells[1, 5].Value = "sendTime";
                    worksheet.Cells[1, 6].Value = "pressure1";
                    worksheet.Cells[1, 7].Value = "pressure2";
                    worksheet.Cells[1, 8].Value = "height1";
                    worksheet.Cells[1, 9].Value = "height2";
                    worksheet.Cells[1, 10].Value = "altitudeDif";
                    worksheet.Cells[1, 11].Value = "descentSpeed";
                    worksheet.Cells[1, 12].Value = "tempature";
                    worksheet.Cells[1, 13].Value = "batteryVoltage";
                    worksheet.Cells[1, 14].Value = "gps1Latitude";
                    worksheet.Cells[1, 15].Value = "gps1Longitude";
                    worksheet.Cells[1, 16].Value = "gps1altitude";
                    worksheet.Cells[1, 17].Value = "roll";
                    worksheet.Cells[1, 18].Value = "pitch";
                    worksheet.Cells[1, 19].Value = "yaw";
                    worksheet.Cells[1, 20].Value = "rhrh";
                    worksheet.Cells[1, 21].Value = "IoTData";
                    worksheet.Cells[1, 22].Value = "TeamNumber";
                    currentRow++;
                }

                // Veriyi yaz
                worksheet.Cells[currentRow, 1].Value = sensorData.MyDataId;
                worksheet.Cells[currentRow, 2].Value = sensorData.packageNumber;
                worksheet.Cells[currentRow, 3].Value = sensorData.satelliteStatus;
                worksheet.Cells[currentRow, 4].Value = sensorData.ErrorCode;
                worksheet.Cells[currentRow, 5].Value = sensorData.sendTime;
                worksheet.Cells[currentRow, 6].Value = sensorData.pressure1;
                worksheet.Cells[currentRow, 7].Value = sensorData.pressure2;
                worksheet.Cells[currentRow, 8].Value = sensorData.height1;
                worksheet.Cells[currentRow, 9].Value = sensorData.height2;
                worksheet.Cells[currentRow, 10].Value = sensorData.altitudeDif;
                worksheet.Cells[currentRow, 11].Value = sensorData.descentSpeed;
                worksheet.Cells[currentRow, 12].Value = sensorData.tempature;
                worksheet.Cells[currentRow, 13].Value = sensorData.batteryVoltage;
                worksheet.Cells[currentRow, 14].Value = sensorData.gps1Latitude;
                worksheet.Cells[currentRow, 15].Value = sensorData.gps1Longitude;
                worksheet.Cells[currentRow, 16].Value = sensorData.gps1altitude;
                worksheet.Cells[currentRow, 17].Value = sensorData.roll;
                worksheet.Cells[currentRow, 18].Value = sensorData.pitch;
                worksheet.Cells[currentRow, 19].Value = sensorData.yaw;
                worksheet.Cells[currentRow, 20].Value = sensorData.rhrh;
                worksheet.Cells[currentRow, 21].Value = sensorData.IoTData;
                worksheet.Cells[currentRow, 22].Value = sensorData.TeamNumber;

                package.Save();
            }
        }
    }
}
