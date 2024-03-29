﻿using System.IO;
using WebApp.Utility.Constants;

namespace WebApp.Handlers
{
    public class FileHandlerFactory
    {
        public static IFileHandler GetFileHandler(string path)
        {
            IFileHandler fileHandler = null;
            string fileExtension     = Path.GetExtension(path);

            switch (fileExtension)
            {
                case CommonConstants.Csv:
                    fileHandler = new CsvFileHandler();
                    break;

                case CommonConstants.Xml:
                    fileHandler = new XmlFileHandler();
                    break;
            }

            return fileHandler;
        }
    }
}
