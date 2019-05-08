using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Core.Repository.Interface;
using Slobkoll.HRM.Web.Providers.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using Ionic.Zip;
using System.Linq;
using Ionic.Zlib;

namespace Slobkoll.HRM.Web.Providers.Implementation
{
    public class JobProvider : IJobProvider
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ISubTaskRepository _subTaskRepository;
        public JobProvider(ITaskRepository taskRepository, ISubTaskRepository subTaskRepository)
        {
            _taskRepository = taskRepository;
            _subTaskRepository = subTaskRepository;
        }

        public void JobTaskCheak()
        {
            IList<Task> ListTask = _taskRepository.LoadTaskAllAct();
            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<MyHub>();

            if (ListTask != null)
            {
                foreach (var item in ListTask)
                {
                    if (item.DateEnd <= DateTime.Now)
                    {
                        bool done = true;
                        foreach (var itemsub in item.SubTask)
                        {
                            if (itemsub.Status != "Выполнено")
                            {
                                done = false;
                            }
                        }
                        if (done)
                        {
                            item.Status = "Выполнено";
                            item.Change = false;
                            _taskRepository.TaskUpdate(item);
                            foreach (var item1 in item.SubTask)
                            {
                                foreach (var connectionId in MyHub._connections.GetConnections(item1.Performer.Login))
                                {
                                    context.Clients.Client(connectionId).Message("Задача №" + item.Id + " закончилась");
                                }
                            }
                        }
                        else
                        {
                            item.Status = "Не выполнено";
                            item.Change = false;
                            _taskRepository.TaskUpdate(item);
                            foreach (var item1 in item.SubTask)
                            {
                                foreach (var connectionId in MyHub._connections.GetConnections(item1.Performer.Login))
                                {
                                    context.Clients.Client(connectionId).Message("Задача №" + item.Id + " закончилась");
                                }
                            }
                        }
                    }
                }
            }
        }
        public void JobBackup()
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/PathBackup.txt");
            IEnumerable<string> result = File.ReadLines(path).Skip(0).Take(1);
            foreach (string str in result)
            {
                path = str;
            }
            BackupDB(path + @"\Slobkoll.bak");
            СompressDirectory(path + @"\Slobkoll.zip");
        }
        private void BackupDB(string OutputFilePath)
        {
            if (File.Exists(OutputFilePath))
            {
                File.Delete(OutputFilePath);
            }
            string sSQL = @"BACKUP DATABASE Slobkoll TO DISK = '" + OutputFilePath + "';";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SedDbConnString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(sSQL, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        private void СompressDirectory(string OutputFilePath)
        {
            if (File.Exists(OutputFilePath))
            {
                File.Delete(OutputFilePath);
            }
            using (ZipFile zip = new ZipFile())
            {
                zip.ProvisionalAlternateEncoding = System.Text.Encoding.GetEncoding(866);
                zip.AddDirectory(System.Web.Hosting.HostingEnvironment.MapPath(@"~\Home"));
                zip.Save(OutputFilePath);
            }
        }
    }
}