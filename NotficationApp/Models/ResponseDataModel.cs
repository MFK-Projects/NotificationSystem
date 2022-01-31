using Newtonsoft.Json;
using NotficationApp.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotficationApp.Models
{
    #region GetUserDataModel
    public class ResponseDataModel
    {
        ///   public string Odata_Contex { get; set; }  
        ///    
        public List<Value> Value { get; set; }
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
        public int NewRemainedTimeHour { get; set; }
        public int NewRemainingDays { get; set; }
        public TaskType NewTaskType { get; set; }
        public TaskStatus NewTaskStatus { get; set; }
        public string Subject { get; set; }
    }
    #endregion
}
