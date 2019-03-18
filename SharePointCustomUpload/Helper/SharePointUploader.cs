using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using System.Web;
using static SharePointCustomUpload.Models.Models.FormDigestInfo;
using Microsoft.SharePoint.Client;
using System.Net.Http.Headers;

namespace SharePointCustomUpload.Helper
{
    public static class SharePointUploader
    {
        private static async Task<Rootobject> GetFormDigest(HttpClientHandler handler, string webUrl)
        {
            //Creating REST url to get Form Digest
            const string RESTURL = "{0}/_api/contextinfo";
            string restUrl = string.Format(RESTURL, webUrl);

            //Adding headers
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json;odata=nometadata");

            //Perform call
            HttpResponseMessage response = await client.PostAsync(restUrl, null).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            //Reading string data
            string jsonData = await response.Content.ReadAsStringAsync();

            //Creating FormDigest object
            Rootobject res = JsonConvert.DeserializeObject<Rootobject>(jsonData);
            return res;
        }

        public static async Task UploadDocumentAsync(string webUrl, string bearerToken, System.IO.Stream document, 
            string folderServerRelativeUrl, string fileName)
        {
            try
            {
                const string SharePointUploadRestApi = 
                    "{0}/_api/web/GetFolderByServerRelativeUrl('{1}')/Files/add(url='{2}',overwrite=true)";
                string SharePointUploadUrl = string.Format(SharePointUploadRestApi, webUrl, folderServerRelativeUrl, fileName);

                using (var handler = new HttpClientHandler())
                {
                    using (var client = new HttpClient(handler))
                    {
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Add("Accept", "application/json;odata=nometadata");
                        client.DefaultRequestHeaders.Add("binaryStringRequestBody", "true");
                        client.MaxResponseContentBufferSize = 2147483647;
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                        //Creating Content
                        var destination = new System.IO.MemoryStream();
                        if (document.GetType() != typeof(System.IO.MemoryStream))
                        {
                            document.CopyTo(destination);
                        }
                        else
                        {
                            destination = document as System.IO.MemoryStream;
                        }
                        ByteArrayContent content = new ByteArrayContent(destination?.ToArray());

                        //Perform post
                        HttpResponseMessage response = await client.PostAsync(SharePointUploadUrl, content).ConfigureAwait(false);

                        //Ensure 200 (Ok)
                        response.EnsureSuccessStatusCode();

                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error uploading document {fileName} call on folder {folderServerRelativeUrl}. {ex.Message}", ex);
            }
            finally
            {
                document?.Dispose();
            }
        }
    }

}
