using Newtonsoft.Json;
using NotficationApp.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotficationApp.Models
{
    #region GetUserDataModel
    public class ResponseDataModel<TEntity> where TEntity : class
    {
        ///   public string Odata_Contex { get; set; }  
        ///    
        public List<TEntity> Value { get; set; }
    }
    public class Value
    {
        public string Domainname { get; set; }
        public string Fullname { get; set; }
        public int Identityid { get; set; }
        public string Systemuserid { get; set; }
        public string Ownerid { get; set; }
    }
    #endregion

    #region TaskDataModel
    public class TasksDataModel
    {
        public string subject { get; set; }
        public int prioritycode { get; set; }
        public int new_task_status { get; set; }
        public int new_task_type { get; set; }
        public string _ownerid_value { get; set; }
        public int new_remained_time_hour { get; set; }
        public int new_remaining_days { get; set; }
        public string activityid { get; set; }
    }
    #endregion
}
