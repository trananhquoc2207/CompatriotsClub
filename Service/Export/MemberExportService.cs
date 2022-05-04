using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using Service.Catalogue;
using Service.common;
using Service.Common;
using ViewModel;

namespace Service.Export
{
    public interface IMemberExportService
    {
        Task<ResultModel> ExportMember(MemberFilter filter);
    }
    public class MemberExportService : IMemberExportService
    {
        private readonly IMemberService _service;
        public MemberExportService(IMemberService service)
        {
            _service = service;
        }
#nullable disable
        public async Task<ResultModel> ExportMember(MemberFilter filter)
        {
            filter.PageSize = 1000;
            filter.PageIndex = 0;
            var resRow = await _service.GetPagedResult(filter);
            #region Define sheet
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFFont myFont = (XSSFFont)workbook.CreateFont();
            myFont.FontHeightInPoints = 13;
            myFont.FontName = "Times Roman";
            XSSFFont headerFont = (XSSFFont)workbook.CreateFont();
            headerFont.IsBold = true;
            headerFont.FontHeightInPoints = 14;
            headerFont.FontName = "Times Roman";
            XSSFFont headerFontTotal = (XSSFFont)workbook.CreateFont();
            headerFontTotal.IsBold = true;
            headerFontTotal.FontHeightInPoints = 16;
            headerFontTotal.FontName = "Times Roman";

            XSSFCellStyle headerCellStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            headerCellStyle.SetFont(headerFont);
            headerCellStyle.WrapText = true;
            headerCellStyle.BorderLeft = BorderStyle.Thin;
            headerCellStyle.BorderTop = BorderStyle.Thin;
            headerCellStyle.BorderRight = BorderStyle.Thin;
            headerCellStyle.BorderBottom = BorderStyle.Thin;
            headerCellStyle.VerticalAlignment = VerticalAlignment.Center;
            headerCellStyle.Alignment = HorizontalAlignment.Center;
            XSSFCellStyle headerTopCellStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            headerTopCellStyle.SetFont(headerFont);
            headerTopCellStyle.WrapText = true;
            headerTopCellStyle.VerticalAlignment = VerticalAlignment.Center;
            headerTopCellStyle.Alignment = HorizontalAlignment.Center;



            XSSFCellStyle borderedCellStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            borderedCellStyle.SetFont(myFont);



            XSSFCellStyle borderedCellNoneStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            borderedCellNoneStyle.SetFont(myFont);
            borderedCellNoneStyle.VerticalAlignment = VerticalAlignment.Center;
            borderedCellNoneStyle.Alignment = HorizontalAlignment.Left;
            borderedCellStyle.BorderLeft = BorderStyle.Thin;
            borderedCellStyle.BorderTop = BorderStyle.Thin;
            borderedCellStyle.BorderRight = BorderStyle.Thin;
            borderedCellStyle.BorderBottom = BorderStyle.Thin;
            borderedCellStyle.VerticalAlignment = VerticalAlignment.Center;
            borderedCellStyle.Alignment = HorizontalAlignment.Left;


            XSSFCellStyle borderedCellStyleTotal = (XSSFCellStyle)workbook.CreateCellStyle();
            borderedCellStyleTotal.SetFont(headerFontTotal);
            borderedCellStyleTotal.BorderLeft = BorderStyle.Thin;
            borderedCellStyleTotal.BorderTop = BorderStyle.Thin;
            borderedCellStyleTotal.BorderRight = BorderStyle.Thin;
            borderedCellStyleTotal.BorderBottom = BorderStyle.Thin;
            borderedCellStyleTotal.VerticalAlignment = VerticalAlignment.Center;
            borderedCellStyleTotal.Alignment = HorizontalAlignment.Center;
            ISheet sheet = workbook.CreateSheet("Tổng thành viên");

            #endregion
            #region Create header
            byte[] data = ResourceUtilities.GetLogoFromResources();
            int pictureIndex = workbook.AddPicture(data, PictureType.PNG);
            ICreationHelper helper = workbook.GetCreationHelper();
            IDrawing drawing = sheet.CreateDrawingPatriarch();
            IClientAnchor anchor = helper.CreateClientAnchor();
            anchor.Col1 = 1;//0 index based column
            anchor.Row1 = 1;//0 index based row
            IPicture picture = drawing.CreatePicture(anchor, pictureIndex);
            picture.Resize();

            IRow titleRow = sheet.CreateRow(0);
            //IRow titleRow1 = sheet.CreateRow(1);

            //IRow titleRow2 = sheet.CreateRow(2);
            //IRow titleRow3 = sheet.CreateRow(3);
            //CreateCell(titleRow1, 13, $"BM:", borderedCellNoneStyle);
            //CreateCell(titleRow2, 13, $"Ngày BH:", borderedCellNoneStyle);
            //CreateCell(titleRow3, 13, $"Rev:", borderedCellNoneStyle);
            IRow titleRowNgay = sheet.CreateRow(4);
            titleRow.Height = 300;

            CreateCell(titleRow, 3, $"BẢNG TỔNG THÀNH VIÊN", headerTopCellStyle);
            CreateCell(titleRow, 6, "", headerTopCellStyle);
            sheet.AddMergedRegion(new CellRangeAddress(0, 3, 3, 10));
            sheet.AddMergedRegion(new CellRangeAddress(4, 4, 3, 10));
            //CreateCell(titleRowNgay, 3, filter.Date.ToString("dd/MM/yyyy"), headerTopCellStyle);

            IRow headerRow = sheet.CreateRow(6);
            CreateCell(headerRow, 0, "Stt", headerCellStyle);
            CreateCell(headerRow, 1, "Mã Bộ Phận", headerCellStyle);
            CreateCell(headerRow, 2, "BỘ PHẬN/PHÒNG BAN/PHÂN XƯỞNG", headerCellStyle);
            CreateCell(headerRow, 3, "Ca ngày", headerCellStyle);
            CreateCell(headerRow, 4, "", headerCellStyle);
            CreateCell(headerRow, 5, "Tăng ca ngày", headerCellStyle);
            CreateCell(headerRow, 6, "", headerCellStyle);
            //CreateCell(headerRow, 7, "CA2  (23H00) \n 10Tiếng", headerCellStyle);
            //CreateCell(headerRow, 8, "", headerCellStyle);
            CreateCell(headerRow, 7, "Ca đêm", headerCellStyle);
            CreateCell(headerRow, 8, "", headerCellStyle);
            CreateCell(headerRow, 9, "Tăng Ca đêm", headerCellStyle);
            CreateCell(headerRow, 10, "", headerCellStyle);
            CreateCell(headerRow, 11, "TỔNG NCC", headerCellStyle);
            CreateCell(headerRow, 12, "KÝ VÀ GHI RÕ \n HỌ TÊN", headerCellStyle);
            CreateCell(headerRow, 13, "Ghi Chú", headerCellStyle);

            IRow headerRow2 = sheet.CreateRow(7);
            CreateCell(headerRow2, 0, "", headerCellStyle);
            CreateCell(headerRow2, 1, "", headerCellStyle);
            CreateCell(headerRow2, 2, "", headerCellStyle);
            CreateCell(headerRow2, 3, "Cơm chay", headerCellStyle);
            CreateCell(headerRow2, 4, "Cơm Mặn", headerCellStyle);
            CreateCell(headerRow2, 5, "Cơm chay", headerCellStyle);
            CreateCell(headerRow2, 6, "Cơm Mặn", headerCellStyle);
            //CreateCell(headerRow2, 7, "Cơm chay", headerCellStyle);
            //CreateCell(headerRow2, 8, "Cơm Mặn", headerCellStyle);
            CreateCell(headerRow2, 7, "Cơm chay", headerCellStyle);
            CreateCell(headerRow2, 8, "Cơm Mặn", headerCellStyle);
            CreateCell(headerRow2, 9, "Cơm chay", headerCellStyle);
            CreateCell(headerRow2, 10, "Cơm Mặn", headerCellStyle);
            CreateCell(headerRow2, 11, "       ", headerCellStyle);
            CreateCell(headerRow2, 12, "       ", headerCellStyle);
            CreateCell(headerRow2, 13, "        ", headerCellStyle);



            sheet.AddMergedRegion(new CellRangeAddress(6, 7, 0, 0));
            sheet.AddMergedRegion(new CellRangeAddress(6, 7, 1, 1));
            sheet.AddMergedRegion(new CellRangeAddress(6, 7, 2, 2));
            sheet.AddMergedRegion(new CellRangeAddress(6, 7, 11, 11));
            sheet.AddMergedRegion(new CellRangeAddress(6, 7, 12, 12));
            sheet.AddMergedRegion(new CellRangeAddress(6, 7, 13, 13));

            sheet.AddMergedRegion(new CellRangeAddress(6, 6, 3, 4));
            sheet.AddMergedRegion(new CellRangeAddress(6, 6, 5, 6));
            //sheet.AddMergedRegion(new CellRangeAddress(6, 6, 7, 8));
            sheet.AddMergedRegion(new CellRangeAddress(6, 6, 7, 8));
            sheet.AddMergedRegion(new CellRangeAddress(6, 6, 9, 10));
            headerRow.HeightInPoints = 60;

            #endregion
            #region Tạo Body
            int rowIndex = 8;

            if (resRow != null)
            {
                int tongCa1Chay = 0;
                int tongCa1Man = 0;
                int tongTCa1Chay = 0;
                int tongTCa1Man = 0;
                //int tongCa2Chay = 0;
                //int tongCa2Man = 0;
                int NightOvertimeShiftChay = 0;
                int TongNightOvertimeShiftMan = 0;
                int TongNightShiftChay = 0;
                int TongNightShiftMan = 0;
                int tongTongNCC = 0;
                int index = 1;

                foreach (var item in resRow.Data as List<MemberResponseViewModel>)
                {
                    //tongCa1Chay += item.DayShift.VegetarianRice;
                    //tongCa1Man += item.DayShift.MeatRice;

                    //tongTCa1Chay += item.DayOvertimeShift.VegetarianRice;
                    //tongTCa1Man += item.DayOvertimeShift.MeatRice;

                    ////tongCa2Chay += item.Ca2.VegetarianRice;
                    ////tongCa2Man += item.Ca2.MeatRice;



                    //TongNightShiftChay += item.NightShift.VegetarianRice;
                    //TongNightShiftMan += item.NightShift.MeatRice;

                    //NightOvertimeShiftChay += item.NightOvertimeShift.VegetarianRice;
                    //TongNightOvertimeShiftMan += item.NightOvertimeShift.MeatRice;

                    //tongTongNCC += item.Total;

                    //headerRow = sheet.CreateRow(rowIndex);
                    //rowIndex++;

                    //CreateCell(headerRow, 0, index, borderedCellStyle);
                    //CreateCell(headerRow, 1, item.UnitCode, borderedCellStyle);
                    //CreateCell(headerRow, 2, item.UnitName, borderedCellStyle);
                    //CreateCell(headerRow, 3, item.DayShift.VegetarianRice, borderedCellStyle);
                    //CreateCell(headerRow, 4, item.DayShift.MeatRice, borderedCellStyle);
                    //CreateCell(headerRow, 5, item.DayOvertimeShift.VegetarianRice, borderedCellStyle);
                    //CreateCell(headerRow, 6, item.DayOvertimeShift.MeatRice, borderedCellStyle);
                    ////CreateCell(headerRow, 7, item.Ca2.VegetarianRice, borderedCellStyle);
                    ////CreateCell(headerRow, 8, item.Ca2.MeatRice, borderedCellStyle);

                    //CreateCell(headerRow, 7, item.NightShift.VegetarianRice, borderedCellStyle);
                    //CreateCell(headerRow, 8, item.NightShift.MeatRice, borderedCellStyle);

                    //CreateCell(headerRow, 9, item.NightOvertimeShift.VegetarianRice, borderedCellStyle);
                    //CreateCell(headerRow, 10, item.NightOvertimeShift.MeatRice, borderedCellStyle);

                    //CreateCell(headerRow, 11, item.Total, borderedCellStyle);
                    //CreateCell(headerRow, 12, "", borderedCellStyle);
                    //CreateCell(headerRow, 13, "", borderedCellStyle);
                    //index++;
                }

                headerRow = sheet.CreateRow(rowIndex);
                CreateCell(headerRow, 0, "Tổng", borderedCellStyle);
                CreateCell(headerRow, 1, "", borderedCellStyle);
                CreateCell(headerRow, 2, "", borderedCellStyle);
                CreateCell(headerRow, 3, tongCa1Chay, borderedCellStyle);
                CreateCell(headerRow, 4, tongCa1Man, borderedCellStyle);
                CreateCell(headerRow, 5, tongTCa1Chay, borderedCellStyle);
                CreateCell(headerRow, 6, tongTCa1Man, borderedCellStyle);
                //CreateCell(headerRow, 7, tongCa2Chay, borderedCellStyle);
                //CreateCell(headerRow, 8, tongCa2Man, borderedCellStyle);

                CreateCell(headerRow, 7, NightOvertimeShiftChay, borderedCellStyle);
                CreateCell(headerRow, 8, TongNightOvertimeShiftMan, borderedCellStyle);
                CreateCell(headerRow, 9, TongNightShiftChay, borderedCellStyle);
                CreateCell(headerRow, 10, TongNightShiftMan, borderedCellStyle);
                CreateCell(headerRow, 11, tongTongNCC, borderedCellStyle);
                CreateCell(headerRow, 12, "", borderedCellStyle);
                CreateCell(headerRow, 13, "", borderedCellStyle);
                headerRow.HeightInPoints = 25;
                sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 0, 2));
                rowIndex++;
            }

            headerRow = sheet.CreateRow(rowIndex);
            CreateCell(headerRow, 0, "TỔNG GIÁM ĐỐC", borderedCellStyle);
            CreateCell(headerRow, 1, "", borderedCellStyle);
            CreateCell(headerRow, 2, "", borderedCellStyle);
            CreateCell(headerRow, 3, "", borderedCellStyle);
            CreateCell(headerRow, 4, "", borderedCellStyle);
            CreateCell(headerRow, 5, "P.NSHC - QT", borderedCellStyle);
            CreateCell(headerRow, 6, "", borderedCellStyle);
            //CreateCell(headerRow, 7, "", borderedCellStyle);
            //CreateCell(headerRow, 8, "", borderedCellStyle);
            CreateCell(headerRow, 7, "", borderedCellStyle);
            CreateCell(headerRow, 8, "", borderedCellStyle);
            CreateCell(headerRow, 9, "NGƯỜI LẬP BIỂU", borderedCellStyle);
            CreateCell(headerRow, 10, "", borderedCellStyle);
            CreateCell(headerRow, 11, "", borderedCellStyle);
            CreateCell(headerRow, 12, "", borderedCellStyle);
            CreateCell(headerRow, 13, "", borderedCellStyle);

            headerRow.HeightInPoints = 30;
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 0, 4));
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 5, 10));
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 11, 13));
            rowIndex++;
            headerRow = sheet.CreateRow(rowIndex);
            CreateCell(headerRow, 0, "", borderedCellStyle);
            CreateCell(headerRow, 1, "", borderedCellStyle);
            CreateCell(headerRow, 2, "", borderedCellStyle);
            CreateCell(headerRow, 3, "", borderedCellStyle);
            CreateCell(headerRow, 4, "", borderedCellStyle);
            CreateCell(headerRow, 5, "", borderedCellStyle);
            CreateCell(headerRow, 6, "", borderedCellStyle);
            //CreateCell(headerRow, 7, "", borderedCellStyle);
            //CreateCell(headerRow, 8, "", borderedCellStyle);
            CreateCell(headerRow, 7, "", borderedCellStyle);
            CreateCell(headerRow, 8, "", borderedCellStyle);
            CreateCell(headerRow, 9, "", borderedCellStyle);
            CreateCell(headerRow, 10, "", borderedCellStyle);
            CreateCell(headerRow, 11, "", borderedCellStyle);
            CreateCell(headerRow, 12, "", borderedCellStyle);
            CreateCell(headerRow, 13, "", borderedCellStyle);
            headerRow.HeightInPoints = 150;
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 0, 4));
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 5, 10));
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 11, 13));
            rowIndex++;
            headerRow = sheet.CreateRow(rowIndex);
            CreateCell(headerRow, 0, "Họ và Tên", borderedCellStyle);
            CreateCell(headerRow, 1, "", borderedCellStyle);
            CreateCell(headerRow, 2, "", borderedCellStyle);
            CreateCell(headerRow, 3, "", borderedCellStyle);
            CreateCell(headerRow, 4, "", borderedCellStyle);
            CreateCell(headerRow, 5, "Họ và Tên:", borderedCellStyle);
            CreateCell(headerRow, 6, "", borderedCellStyle);
            //CreateCell(headerRow, 7, "", borderedCellStyle);
            //CreateCell(headerRow, 8, "", borderedCellStyle);
            CreateCell(headerRow, 7, "", borderedCellStyle);
            CreateCell(headerRow, 8, "", borderedCellStyle);
            CreateCell(headerRow, 9, "Họ và Tên:", borderedCellStyle);
            CreateCell(headerRow, 10, "", borderedCellStyle);
            CreateCell(headerRow, 11, "", borderedCellStyle);
            CreateCell(headerRow, 12, "", borderedCellStyle);
            CreateCell(headerRow, 13, "", borderedCellStyle);
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 0, 4));
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 5, 10));
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 11, 13));
            rowIndex++;
            headerRow = sheet.CreateRow(rowIndex);
            CreateCell(headerRow, 0, "Ngày:", borderedCellStyle);
            CreateCell(headerRow, 1, "", borderedCellStyle);
            CreateCell(headerRow, 2, "", borderedCellStyle);
            CreateCell(headerRow, 3, "", borderedCellStyle);
            CreateCell(headerRow, 4, "", borderedCellStyle);
            CreateCell(headerRow, 5, "Ngày:", borderedCellStyle);
            CreateCell(headerRow, 6, "", borderedCellStyle);
            //CreateCell(headerRow, 7, "", borderedCellStyle);
            //CreateCell(headerRow, 8, "", borderedCellStyle);
            CreateCell(headerRow, 7, "", borderedCellStyle);
            CreateCell(headerRow, 8, "", borderedCellStyle);
            CreateCell(headerRow, 9, "Ngày:", borderedCellStyle);
            CreateCell(headerRow, 10, "", borderedCellStyle);
            CreateCell(headerRow, 11, "", borderedCellStyle);
            CreateCell(headerRow, 12, "", borderedCellStyle);
            CreateCell(headerRow, 13, "", borderedCellStyle);
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 0, 4));
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 5, 10));
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 11, 13));

            #endregion
            for (int i = 1; i <= 13; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            sheet.SetColumnWidth(11, 25 * 135);
            sheet.SetColumnWidth(12, 25 * 177);
            // sheet.ce = 283;
            var result = new ResultModel();
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    workbook.Write(memoryStream);
                    result.Succeed = true;
                    result.Data = memoryStream.ToArray();
                }
            }
            catch (Exception e)
            {
                result.ErrorMessages = e.Message;
            }
            return result;
        }
        void CreateCell(IRow currentRow, int cellIndex, object value, XSSFCellStyle style)
        {
            ICell Cell = currentRow.CreateCell(cellIndex);
            if (value != null)
            {
                Cell.SetCellValue(value.ToString());
            }
            else
            {
                Cell.SetCellValue("");
            }
            Cell.CellStyle = style;
        }
    }
}
