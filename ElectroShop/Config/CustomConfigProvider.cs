using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ElectroShop.Config
{
    public static class CustomConfigProviderExtensions
    {
        public static IConfigurationBuilder AddEncryptedProvider(this IConfigurationBuilder builder)
        {
            return builder.Add(new CustomConfigProvider());
        }
    }

    public class CustomConfigProvider : ConfigurationProvider, IConfigurationSource
    {
        public CustomConfigProvider()
        {

        }

        public override void Load()
        {
            Data = UnencryptMyConfiguration();
        }

        private IDictionary<string, string> UnencryptMyConfiguration()
        {
            // do whatever you need to do here, for example load the file and unencrypt key by key
            //Like:
            var configValues = new Dictionary<string, string>
           {
                //{"amir", @"data source=172.22.59.65\SQL2016;initial catalog=LeeoeDB;User Id=sa;Password=123;"},
                //{"amir", @"data source=192.168.43.57\sql2014;initial catalog=LeeoeDB;User Id=leeoeAdminSqlUser;Password=Far.Za#%^dT11@#;"},
                //{"amir", @"data source=.\SQL2014;initial catalog=LeeoeDB;User Id=sa;Password=123;"},
                {"sqlstr", @"data source=.;initial catalog=ElectroShopDB;User Id=sa;Password=123;"},
                //{"sqlstr", @"data source=Leeoe.com\SQL2017;initial catalog=LeeoeDB;User Id=leeoeAdminSqlUser;Password=Far.Za#%^dT11@#;"},
                {"key2", "unencryptedValue2"}
           };
            return configValues;
        }

        //    USE[master]
        //GO
        //CREATE LOGIN leeoeAdminSqlUser WITH PASSWORD=N'Far.Za#%^dT11@#', 
        //DEFAULT_DATABASE=[LeeoeDB]
        //    GO

        //USE[LeeoeDB]
        //GO
        //CREATE USER[leeoeUserInAdminLogin] FOR LOGIN[leeoeAdminSqlUser]
        //GO

        private IDictionary<string, string> CreateAndSaveDefaultValues(IDictionary<string, string> defaultDictionary)
        {
            var configValues = new Dictionary<string, string>
            {
                {"key1", "encryptedValue1"},
                {"key2", "encryptedValue2"}
            };
            return configValues;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new CustomConfigProvider();
        }
    }
}
