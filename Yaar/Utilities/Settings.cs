using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Yaar.Utilities
{
    public class Settings
    {
        private JToken _json;

        public Settings()
        {
            _json = JToken.Parse(File.ReadAllText(Constants.SettingsFile.FullName));
            Twitters = _json["Twitters"].Select(o => o.ToString()).ToList();
            Videos = _json["Videos"].Select(o => new DirectoryInfo(o.ToString())).ToList();

            //Emails
            Accounts = new List<EmailAccount>();
            foreach(var account in _json["Emails"])
                Accounts.Add(new EmailAccount(account["Email"].ToString(), account["Password"].ToString()));

            //Alarms
            Wake = DateTime.ParseExact(_json["Wake"].ToString(), "HH:mm", CultureInfo.CurrentCulture).TimeOfDay;
            Sleep = DateTime.ParseExact(_json["Sleep"].ToString(), "HH:mm", CultureInfo.CurrentCulture).TimeOfDay;
        }

        public List<string> Twitters { get; private set; }
        public List<DirectoryInfo> Videos { get; private set; }
        public List<EmailAccount> Accounts { get; private set; }
        public TimeSpan Wake { get; private set; }
        public TimeSpan Sleep { get; private set; }

        public class EmailAccount
        {
            public string Email { get; private set; }
            public string Password { get; private set; }

            public EmailAccount(string email, string password)
            {
                Email = email;
                Password = password;
            }
        }
    }
}
