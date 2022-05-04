namespace Service.Common
{
    public static class ResourceUtilities
    {
#nullable enable
        /// <summary>
        ///     Get file from resource folder
        /// </summary>
        public static byte[]? GetFileFromResources(string _filePath)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", _filePath);
            if (!File.Exists(filePath))
            {
                return null;
            }

            var fileContent = File.ReadAllBytes(filePath);
            return fileContent;
        }
#nullable disable

        /// <summary>
        ///     Get logo
        /// </summary>
        public static byte[] GetLogoFromResources()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Images/logo.png");
            var fileContent = File.ReadAllBytes(filePath);

            return fileContent;
        }
    }
}
