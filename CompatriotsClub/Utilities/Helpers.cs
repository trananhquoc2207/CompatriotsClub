using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using System.Text.RegularExpressions;

namespace CompatriotsClub.Utilities
{
    public static class Helpers
    {
        public static string AppName { get; set; }
        public static string getAddressesFilePath(string fileName)
        {
            return AppDomain.CurrentDomain.GetData("DataDirectory").ToString()
                  + "/" + fileName;
        }

        public static string ReadFileContent(string json_path)
        {
            StreamReader reader = new StreamReader(json_path);
            string content = reader.ReadToEnd();
            reader.Close();
            return content;
        }

        public static DateTime ConvertTimeSpanToDateTime(TimeSpan item)
        {
            return new DateTime(item.Ticks);
        }

        public static string HashPassword(string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes("sv7m7LDHommIfUG1lqLkyA==");

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
           password: password,
           salt: salt,
           prf: KeyDerivationPrf.HMACSHA1,
           iterationCount: 10000,
           numBytesRequested: 256 / 8));
            return hashed;
        }
        public static DateTime ConvertStringToDate(string date)
        {
            return
                DateTime.ParseExact(date, new string[] {
                    "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy" ,
                    "dd/MM/yyyy hh:mm tt", "dd/M/yyyy hh:mm tt", "d/M/yyyy hh:mm tt", "d/MM/yyyy hh:mm tt" ,
                    "dd/MM/yyyy hh:mm:ss tt", "dd/M/yyyy hh:mm:ss tt", "d/M/yyyy hh:mm:ss tt", "d/MM/yyyy hh:mm:ss tt" ,
                }, null);
        }
        public static string ConvertDateToShow(this DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy");
        }

        public static string ConvertDateTimeToShow(this DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy HH:mm:ss");
        }

        public static string ConvertDoBToPassword(DateTime DoB)
        {
            return DoB.ToString("ddMMyyyy");
        }

        private static byte[] AppendByteArray(byte[] byteArray1, byte[] byteArray2)
        {
            var byteArrayResult =
                new byte[byteArray1.Length + byteArray2.Length];

            for (var i = 0; i < byteArray1.Length; i++)
                byteArrayResult[i] = byteArray1[i];
            for (var i = 0; i < byteArray2.Length; i++)
                byteArrayResult[byteArray1.Length + i] = byteArray2[i];

            return byteArrayResult;
        }



        public static string CreateUsernameAndPassword(int id, string prefix)
        {
            return $"{prefix}{id}";
        }




        public static T FindById<T>(this IEnumerable<T> items, string propertyName, object value)
        {
            var selectors = items
                 .Select(a => new { a, v = a.GetType().GetProperty(propertyName).GetValue(a, null) });
            var item = selectors
                 .FirstOrDefault(x => x.v.Equals(value));
            return item.a;
        }

        public static Dictionary<string, string> GetValuesFromHTML(string htmlSource)
        {
            var values = htmlSource.Split(@"<input");
            var result = new Dictionary<string, string>();
            foreach (var item in values)
            {
                if (item.Contains("value="))
                {
                    var name = GetValueFromHTMLKey(item, "name=\"");

                    var value = GetValueFromHTMLKey(item, "value=\"");

                    result.Add(name, value);
                }
            }
            return result;
        }
        private static string GetValueFromHTMLKey(string item, string key)
        {
            var index = item.IndexOf(key) + key.Length;
            var lastIndex = item.IndexOf("\" ", index);
            var len = lastIndex - index;
            string value = item.Substring(index, len);
            value = value.Replace("\"", "");
            return value;
        }

        public static string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }

        public static int GetCountDayOfMonth(DateTime date)
        {
            int count = 0;
            if (date.Month >= 1 && date.Month <= 12)
            {
                switch (date.Month)
                {
                    case 1: count = 31; break;
                    case 3: count = 31; break;
                    case 4: count = 30; break;
                    case 5: count = 31; break;
                    case 6: count = 30; break;
                    case 7: count = 31; break;
                    case 8: count = 31; break;
                    case 9: count = 30; break;
                    case 10: count = 31; break;
                    case 11: count = 30; break;
                    case 12: count = 31; break;

                    case 2:
                        if (date.Year % 400 == 0 || (date.Year % 4 == 0 && date.Year % 100 != 0))    // nam nhuan
                            count = 29;
                        else
                            count = 28;
                        break;
                }
                return count;
            }
            return 0;
        }

        public static int GetDayNumber(this DayOfWeek dayOfWeek)
        {
            int number = 0;
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday: number = 2; break;
                case DayOfWeek.Tuesday: number = 3; break;
                case DayOfWeek.Wednesday: number = 4; break;
                case DayOfWeek.Thursday: number = 5; break;
                case DayOfWeek.Friday: number = 6; break;
                case DayOfWeek.Saturday: number = 7; break;
                case DayOfWeek.Sunday: number = 8; break;
            }
            return number;
        }

    }
}
