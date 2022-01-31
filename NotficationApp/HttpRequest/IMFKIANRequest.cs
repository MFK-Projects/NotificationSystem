using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NotficationApp.Models;
using Newtonsoft.Json;

namespace NotficationApp.HttpRequest
{
    public interface IMFKIANRequest
    {
        (string username, string userid) GetCurentUserDetails(DataRequestModel model);

        Task GetTaskData(DataRequestModel model);
    }

    public class MFKIANRequest : IMFKIANRequest
    {

        private const string baseUrl = @"http://80.210.26.4:8585/MFKIAN/api/data/v9.0/";


        public (string username, string userid) GetCurentUserDetails(DataRequestModel model)
        {
            try
            {

                var url = UrlBuilder(new UrlBuilderModel { BaseUrl = baseUrl, EntityName = model.EntitiyName, SelectedItem = model.SelectItem });

                var headers = new List<HeaderModel>() {

                    new HeaderModel{Key ="OData-Version",Value ="4.0"},
                    new HeaderModel{Key="Prefer",Value =$"odata.maxpagesize={model.Count}"}

                };

                var _stringdata = SendHtppRequest(new HtppRequestModel { Headers = headers,Url= url });


                var jsonConverter = JsonConvert.DeserializeObject<ResponseDataModel>(_stringdata);

                return ();


            }
            catch (Exception)
            {

                throw new Exception("error while geting data from the server");
            }
        }

        public async Task GetTaskData(DataRequestModel model)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }




        private string SendHtppRequest(HtppRequestModel requestModel)
        {

          var _client = new WebClient();

            try
            {
                _client.Credentials = new NetworkCredential(requestModel.UserName, requestModel.Password, requestModel.Domain);
                _client.Headers.Add("OData-Version", "4.0");

                var data = _client.DownloadString(new Uri(requestModel.Url));

                return data;
            }
            catch
            {
                throw new Exception("error on WebClient method");
            }
            finally
            { 
                _client?.Dispose();
            }

    
        }
        private string UrlBuilder(UrlBuilderModel model)
        {
            if (model == null)
                return string.Empty;


            var url = baseUrl + model.EntityName;


            if (model.SelectedItem.Length > 0)
            {
                url += @"?$select=";
                for (int i = 0; i < model.SelectedItem.Length; i++)
                    if (i == model.SelectedItem.Length - 1)
                        url += model.SelectedItem[i];
                    else
                        url += model.SelectedItem[i] + ",";
            }

            return url;


            //if (model.Filters.Length > 0)
            //{
            //    url+= @"&$filter="
            //    for (int i = 0; i < model.Filters.Length; i++)
            //}


        }
    }
}
