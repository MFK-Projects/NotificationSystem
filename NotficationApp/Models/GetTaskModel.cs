using System;
using System.Collections.Generic;
using System.Text;

namespace NotficationApp.Models
{
    public class DataRequestModel
    {
        public string SystemUserName { get; set; }
        public string EntitiyName { get; set; }
        public string[] SelectItem { get; set; }
        public int Count { get; set; } = 3;
    }

    public class HeaderModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class HtppRequestModel
    {
        public string BaseUrl { get => @"http://80.210.26.4:8585/MFKIAN/api/data/v9.0/"; }
        public string Url { get; set; }
        public List<HeaderModel> Headers { get; set; }
        public string UserName { get; set; } = "a.moradi";
        public string Password { get; set; } = "r";
        public string Domain { get; set; } = "KIAN";
    }

    public class UrlBuilderModel
    {
        public string EntityName { get; set; }
        public string BaseUrl { get; set; }
        public string[] SelectedItem { get; set; }
        public List<FilterItem> FilterItem { get; set; }

    }

    public class FilterItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string FilterKey { get;set; }
    }
}
