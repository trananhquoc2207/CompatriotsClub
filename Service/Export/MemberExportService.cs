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
            CreateCell(headerRow, 1, "Mã hội viên", headerCellStyle);
            CreateCell(headerRow, 2, "Tên hội viên", headerCellStyle);
            CreateCell(headerRow, 3, "Giới tính", headerCellStyle);
            CreateCell(headerRow, 4, "Số điện thoại", headerCellStyle);
            CreateCell(headerRow, 5, "Chứng minh nhân dân", headerCellStyle);
            headerRow.HeightInPoints = 60;

            #endregion
            #region Tạo Body
            int rowIndex = 7;

            if (resRow != null)
            {
                int index = 1;
                foreach (var item in resRow.Data as List<MemberResponseViewModel>)
                {

                    headerRow = sheet.CreateRow(rowIndex);
                    rowIndex++;

                    CreateCell(headerRow, 0, index, borderedCellStyle);
                    CreateCell(headerRow, 1, item.Code, borderedCellStyle);
                    CreateCell(headerRow, 2, item.Name, borderedCellStyle);
                    CreateCell(headerRow, 3, item.Gender, borderedCellStyle);
                    CreateCell(headerRow, 4, item.PhoneNumber, borderedCellStyle);
                    CreateCell(headerRow, 5, item.Idcard, borderedCellStyle);
                    CreateCell(headerRow, 12, "", borderedCellStyle);
                    CreateCell(headerRow, 13, "", borderedCellStyle);
                    index++;
                }

                headerRow.HeightInPoints = 25;
                sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 0, 2));
                rowIndex++;
            }


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
