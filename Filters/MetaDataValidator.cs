using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Utility.Constants;

namespace WebApp.Filters
{
    public class MetaDataValidator : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.Any())
            {
                context.Result = new BadRequestObjectResult(LogMessageConstants.NoFileSelected);
                return;
            }

            if (context.ActionArguments.ContainsKey(CommonConstants.File) &&
                context.ActionArguments[CommonConstants.File] is FormFile file)
            {
                var allowedMaxFileSize   = Config.Configuration.GetValue<int>("MaxFileSizeInMb");
                var totalFileSizeInBytes = allowedMaxFileSize * CommonConstants.BytesFor1Mb;
                var fileExtension        = Path.GetExtension(file.FileName);

                if (fileExtension != CommonConstants.Csv && fileExtension != CommonConstants.Xml)
                {
                    context.Result = new BadRequestObjectResult(LogMessageConstants.UnknownFileFormat);
                    return;
                }

                if (file.Length == 0)
                {
                    context.Result = new BadRequestObjectResult(LogMessageConstants.EmptyFile);
                    return;
                }

                if (file.Length > totalFileSizeInBytes)
                {
                    context.Result = new BadRequestObjectResult($"{LogMessageConstants.FileSizeExceeds}{allowedMaxFileSize}Mb");
                    return;
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
