﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageAPI.Application.Features.Constants;

internal static class FileStorageErrorMessages
{
    internal const string UserNotFound = "User not found.";
    internal const string UserNotAuthorized = "User not authorized.";
    internal const string FileAlreadyExists = "File already exists.";
    internal const string FileSizeExceeded = "File size exceeded.";

}
