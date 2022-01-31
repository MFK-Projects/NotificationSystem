using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Diagnostics;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using System.Net.Http;
using NotficationApp.HttpRequest;
using System.Threading.Tasks;
using System.Threading;
using NotficationApp.Models;
using System.Net;
using System.IO;
using System.DirectoryServices.AccountManagement;

namespace NotficationApp
{
    internal class Program
    {
        private readonly static string[] selectedItemForCurentUser = { "domainname", "fullname", "identityid" };
        private readonly static string[] selectedItemForUserTasks = { "subject", "prioritycode", "new_task_status", "new_task_type", "_ownerid_value", "new_remained_time_hour", "new_remaining_days" };
        private static string UserName;

        static void Main(string[] args)
        {

            try
            {

                Task.Factory.StartNew(() =>
                {
                    UserName = UserPrincipal.Current.Name;
                });

                var servies = new ServiceCollection()
                    .AddScoped<IMFKIANRequest, MFKIANRequest>()
                    .BuildServiceProvider();

                var request = servies.GetService<IMFKIANRequest>();


                while (UserName == null) { Thread.Sleep(1000); }


                var curentUser = request.GetCurentUserDetails(new DataRequestModel { EntitiyName = "systemusers", Count = 3, SelectItem = selectedItemForCurentUser, SystemUserName = UserName });


                while (true)
                {
                    #region Ensure That Variable are assgined

                    if (string.IsNullOrEmpty(curentUser.username) || string.IsNullOrEmpty(curentUser.userid))
                        curentUser = request.GetCurentUserDetails(new DataRequestModel { EntitiyName = "systemusers", Count = 3, SelectItem = selectedItemForCurentUser, SystemUserName = UserName });

                    if (servies == null)
                        servies = new ServiceCollection()
                      .AddScoped<IMFKIANRequest, MFKIANRequest>()
                      .BuildServiceProvider();

                    if (request == null)
                        request = servies.GetService<IMFKIANRequest>();
                    #endregion

                    var usreTasks = request.GetTaskData(new DataRequestModel { EntitiyName = "tasks", SelectItem = selectedItemForUserTasks, SystemUserName = UserName });


                    foreach (var task in usreTasks)
                        SendToastNotification(task.Subject, task.NewTaskType.ToString(), "http://google.com");



                    Thread.Sleep(TimeSpan.FromSeconds(10));
                }
            }
            catch (Exception)
            {
                throw;
            }


            //  using var client = new WebClient();
            //  client.Credentials = new NetworkCredential("a.moradi", "r", "KIAN");
            //  client.Headers.Add("OData-Version", "4.0");
            ////  client.OpenReadAsync(new Uri("http://80.210.26.4:8585/MFKIAN/api/data/v9.0/systemusers?$select=domainname,fullname,identityid"));
            //var data =  client.DownloadString("http://80.210.26.4:8585/MFKIAN/api/data/v9.0/systemusers?$select=domainname,fullname,identityid");



            //var crendintalcash = new CredentialCache();
            //crendintalcash.Add(new Uri("http://http://80.210.26.4:8585/"), "NTLM", new NetworkCredential { Domain = "KIAN", Password = "r", UserName = "a.moradi" });

            //using var handler = new HttpClientHandler { Credentials = crendintalcash };
            //using var httpclient = new HttpClient(handler);

            //var test = httpclient.GetStringAsync("http://80.210.26.4:8585/MFKIAN/api/data/v9.0/systemusers?$select=domainname,fullname,identityid").Result;


            //systemusers ?$select = domainname,identityid &$filter = domainname eq 'KIAN"+@"\"+"R.Khaleghi




            //var tests =  httpclient.GetStringAsync(url).Result;


            //if (requestModel.Headers.Count > 0)
            //    foreach (var item in requestModel.Headers)
            //        httpclient.DefaultRequestHeaders.Add(item.Key, item.Value);

            //var reposnemessage = httpclient.GetStringAsync(requestModel.BaseUrl + requestModel.Url).Result;





            //var services = new ServiceCollection()
            //               .AddSingleton<IHttpClientFactory, HttpClientFactory>()
            //               .AddScoped<IMFKIANRequest,MFKIANRequest>()
            //               .BuildServiceProvider();




            //try
            //{
            //    string[] testselect = { "domainname", "fullname", "identityid" };
            //    var testrequest = services.GetService<IMFKIANRequest>();
            //    var ok = testrequest.GetCurentUserDetails(new DataRequestModel { EntitiyName= "systemusers", SelectItem =testselect,UserId=40});
            //}
            //catch (Exception)
            //{

            //    throw;
            //}


            //do
            //{

            //    Thread.Sleep(TimeSpan.FromSeconds(10));

            //} while (true);
        }


        private static void SendToastNotification(string text, string titiel, string url)
        {

            var toast = new ToastContentBuilder();
            toast.AddHeader("", titiel, "");
            toast.AddText(text);
            toast.SetProtocolActivation(new Uri(url));
            toast.SetToastDuration(ToastDuration.Short);
            toast.SetToastScenario(ToastScenario.Reminder);
            toast.Show();
        }
    }
}
