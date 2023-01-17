using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using StudentManagementSystem.Data;

namespace StudentManagementSystem.Logic
{
    public class ManagerFactory
    {
        public Manager GetManager()
        {
            var config = new ConfigurationBuilder().AddJsonFile
            ("appsettings.json", true, true).Build();

            string mode = config.GetSection("Settings:DataMode").Value;
            GetDataSource(mode);

            var dataSource = GetDataSource(mode);

            return new Manager(dataSource);

        }

        private IDataSource GetDataSource(string mode)
        {
            switch (mode)
            {
                case "TestData":
                    return new TestDataSource();

                case "TxtData":
                    return new TxtDataSource();

                default:
                    throw new Exception("Data mode could not be configured");
            }
        }
    }
}
