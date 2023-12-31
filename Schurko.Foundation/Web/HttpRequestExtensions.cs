﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Schurko.Foundation.Web
{
    public static class HttpRequestExtensions
    {
        static string WebRootPath { get; set; }

        /// <summary>
        /// Retrieve the raw body as a string from the Request.Body stream
        /// </summary>
        /// <param name="request">Request instance to apply to</param>
        /// <param name="encoding">Optional - Encoding, defaults to UTF8</param>
        /// <param name="inputStream">Optional - Pass in the stream to retrieve from. Other Request.Body</param>
        /// <returns></returns>
        public static async Task<string> GetRawBodyStringAsync(this HttpRequest request, Encoding encoding = null, Stream inputStream = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            if (inputStream == null)
                inputStream = request.Body;

            using (StreamReader reader = new StreamReader(inputStream, encoding))
                return await reader.ReadToEndAsync();
        }

        /// <summary>
        /// Retrieves the raw body as a byte array from the Request.Body stream
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetRawBodyBytesAsync(this HttpRequest request, Stream inputStream = null)
        {
            if (inputStream == null)
                inputStream = request.Body;

            using (var ms = new MemoryStream(2048))
            {
                await inputStream.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Maps a virtual or relative path to a physical path in a Web site,
        /// using the WebRootPath as the base path (ie. the `wwwroot` folder)
        /// </summary>
        /// <param name="request">HttpRequest instance</param>
        /// <param name="relativePath">Site relative path using either `~/` or `/` as indicating root</param>
        /// <param name="host">Optional - IHostingEnvironment instance. If not passed retrieved from RequestServices DI</param>
        /// <param name="basePath">Optional - Optional physical base path. By default host.WebRootPath</param>
        /// <param name="useAppBasePath">Optional - if true returns the launch folder rather than the wwwroot folder</param>
        /// <returns>physical path of the relative path</returns>
        [Obsolete("Please use HttpContextExtensions.MapPath() instead.")]
        public static string MapPath(this HttpRequest request, string relativePath = null, IWebHostEnvironment host = null, string basePath = null, bool useAppBasePath = false)
        {
            if (string.IsNullOrEmpty(relativePath))
                relativePath = "/";

            // Ignore absolute paths
            if (relativePath.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
                relativePath.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                return relativePath;

            if (useAppBasePath || basePath == null)
            {
                basePath = WebRootPath;
                if (useAppBasePath || string.IsNullOrEmpty(basePath))
                {
                    host ??= request.HttpContext.RequestServices.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;
                    WebRootPath = host.WebRootPath;
                    basePath = useAppBasePath ? host.ContentRootPath.TrimEnd('/', '\\') : WebRootPath;
                }
            }
            else
            {
                basePath = WebRootPath;
            }

            relativePath = relativePath.TrimStart('~', '/', '\\');

            string path = Path.Combine(basePath, relativePath);

            string slash = Path.DirectorySeparatorChar.ToString();
            return path
                .Replace("/", slash)
                .Replace("\\", slash)
                .Replace(slash + slash, slash);
        }


        /// <summary>
        /// Returns the absolute Url of the current request as a string.
        /// </summary>
        /// <param name="request"></param>
        public static string GetUrl(this HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
        }


        /// <summary>
        /// Returns a value based on a key against the Form, Query and Session collections.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <param name="noSession">If true Session object is not check</param>
        /// <returns></returns>
        public static string Params(this HttpRequest request, string key)
        {
            string value = request.Form[key].FirstOrDefault();
            if (string.IsNullOrEmpty(value))
                value = request.Query[key].FirstOrDefault();

            return value;
        }

        /// <summary>
        /// Determines if the request is a local request where the local and remote IP addresses match
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static bool IsLocal(this HttpRequest req)
        {
            var connection = req.HttpContext.Connection;
            if (connection.RemoteIpAddress != null)
            {
                if (connection.LocalIpAddress != null)
                    return connection.RemoteIpAddress.Equals(connection.LocalIpAddress);

                return IPAddress.IsLoopback(connection.RemoteIpAddress);
            }

            // for in memory TestServer or when dealing with default connection info
            if (connection.RemoteIpAddress == null && connection.LocalIpAddress == null)
                return true;
            //if (req.Host.HasValue && req.Host.Value.StartsWith("localhost:") )
            //    return true;

            return false;
        }

        /// <summary>
        /// Checks to see if a given form variable exists in the request form collection.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="formVarName"></param>
        /// <returns></returns>
        public static bool IsFormVar(this HttpRequest req, string formVarName)
        {
            if (!req.HasFormContentType) return false;

            return req.Form[formVarName].Count > 0;
        }

        /// <summary>
        /// Determines whether request is a postback operation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsPostback(this HttpRequest request)
        {
            return request.Method.Equals("POST", StringComparison.InvariantCultureIgnoreCase) ||
                   request.Method.Equals("PUT", StringComparison.InvariantCultureIgnoreCase);
        }

     
    }
}
