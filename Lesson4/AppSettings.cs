using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Buildings
{

    public class AppSettings
    {

        public int BuildingCounter { get; set; }
        public double FloorsHeight { get; set; }
        public int AppPerFloor { get; set; }
        public AppSettings(int mode)//- любой инт будет читать, иначе - пустой конструктор
        {
            ReadSettings();
        }

        public AppSettings()
        {
            
        }

        public void SaveSettings(int lastBid)
        {
            var path = GetUserSettings();
            this.BuildingCounter = lastBid;
            string jsonsettings = JsonSerializer.Serialize(this);
            File.WriteAllText(path, jsonsettings);
        }
        private void ReadSettings()
        {

            string usersettings = GetUserSettings();

            if (File.Exists(usersettings))
            {

                var reader = File.ReadAllText(usersettings);
                var currentsettings = JsonSerializer.Deserialize<AppSettings>(reader);
                this.FloorsHeight = currentsettings.FloorsHeight;
                this.BuildingCounter = currentsettings.BuildingCounter;
                this.AppPerFloor = currentsettings.AppPerFloor;

            }
            else
            {
                var newsettings = File.Create(usersettings);

                this.BuildingCounter = 0;
                this.FloorsHeight = 2.5;
                this.AppPerFloor = 4;
                newsettings.Dispose();
                string jsonsettings = JsonSerializer.Serialize(this);
                File.WriteAllText(usersettings, jsonsettings);
               
            }
        }

        private string GetUserSettings()
        {
            var userfolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();//ой не надо это только у себя запускать, а то придется чистить это говно
            string usersettings = Path.Combine(userfolder, "appsettings.json");
            return usersettings;

        }


    }
}
