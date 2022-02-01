using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NotficationApp.Models;
using Newtonsoft.Json;
using NotficationApp.Enums;

namespace NotficationApp.HttpRequest
{
    public interface IMFKIANRequest
    {
        (string username, string userid) GetCurentUserDetails(DataRequestModel model);

        List<TasksDataModel> GetTaskData(DataRequestModel model);
    }

    public class MFKIANRequest : IMFKIANRequest
    {

        private const string baseUrl = @"http://80.210.26.4:8585/MFKIAN/api/data/v9.0/";


        public (string username, string userid) GetCurentUserDetails(DataRequestModel model)
        {
            try
            {
                //var filter = new List<FilterItem> { new FilterItem { Name = "domainname", Value = model.SystemUserName, FilterKey = "eq" } };
                var filter = new List<FilterItem> { new FilterItem { Name = "domainname", Value = "KIAN\\R.Khaleghi", FilterKey = "eq" ,FilterType = FilterType.String } };

                var url = UrlBuilder(new UrlBuilderModel { BaseUrl = baseUrl, EntityName = model.EntitiyName, SelectedItem = model.SelectItem, FilterItem = filter });

                var headers = new List<HeaderModel>() {

                    new HeaderModel{Key ="OData-Version",Value ="4.0"},
                    new HeaderModel{Key="Prefer",Value =$"odata.maxpagesize={model.Count}"}

                };

                var _stringdata = SendHtppRequest(new HtppRequestModel { Headers = headers, Url = url });


                var jsonConverter = JsonConvert.DeserializeObject<ResponseDataModel<Value>>(_stringdata);
                Value tempdata = new Value();

                if (jsonConverter.Value.Count == 1)

                {
                    foreach (var item in jsonConverter.Value)
                    {
                        tempdata.Domainname = item.Domainname;
                        tempdata.Ownerid = item.Ownerid;
                    }

                    return (tempdata.Domainname, tempdata.Ownerid);
                }


                return (string.Empty, string.Empty);


            }
            catch (Exception)
            {

                throw new Exception("error while geting data from the server");
            }
        }

        public List<TasksDataModel> GetTaskData(DataRequestModel model)
        {
            try
            {
                //  var filter = new List<FilterItem> { new FilterItem { Name = "domainname", Value = model.SystemUserName, FilterKey = "eq" } };
                var filter = new List<FilterItem> { new FilterItem { Name = "_ownerid_value", Value = "fed89561-53ec-eb11-9129-000c2995bbbf", FilterKey = "eq" ,FilterType = FilterType.Int} };

                var url = UrlBuilder(new UrlBuilderModel { BaseUrl = baseUrl, EntityName = model.EntitiyName, SelectedItem = model.SelectItem, FilterItem = filter });

                var headers = new List<HeaderModel>() {

                    new HeaderModel{Key ="OData-Version",Value ="4.0"},
                };


                var _stringData = SendHtppRequest(new HtppRequestModel { Headers = headers, Url = url });

                var formatedData = JsonConvert.DeserializeObject<ResponseDataModel<TasksDataModel>>(_stringData);

                return formatedData.Value;
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

            if (model.FilterItem.Count > 0)
            {
                url += @"&$filter=";
                foreach (var item in model.FilterItem)
                    if (item.FilterType == FilterType.String)
                        url += "  " + item.Name + " " + item.FilterKey + " '" + item.Value + "'";
                    else if (item.FilterType == FilterType.Int)
                        url += "  " + item.Name + " " + item.FilterKey + " " + item.Value;
            }

            return url;

        }
    }
}
